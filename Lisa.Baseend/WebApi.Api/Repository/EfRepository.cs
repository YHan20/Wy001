using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Api.Entity;
using WebApi.Api.Db;

namespace WebApi.Api.Repository
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 数据库上下文的变量，此处是使用常规手段，直接初始化一个数据库上下文的实例对象
        /// </summary>
        /// <returns></returns>
        private ShowDb _db;

        public EfRepository(ShowDb db)
        {
            _db=db;
        }

        /// <summary>
        /// 一个字段成员，用于内部Entity属性
        /// </summary>
        private DbSet<T> _entity;


        /// <summary>
        /// 一个访问受限的属性，只有访问器，总是返回一个代表T类型的表对象
        /// </summary>
        /// <value></value>
        protected DbSet<T> Entity
        {
            get
            {
                // 如果 _entity为空（没有指向一个对象），则从数据库上下文重新获得一个
                if (_entity == null)
                {
                    _entity = _db.Set<T>();
                }
                return _entity;
            }
        }

        /// <summary>
        /// 代表一个可以用于查询T类型的表
        /// </summary>
        /// <value></value>
        public IQueryable<T> Table
        {
            get
            {
                // return Entity.AsQueryable<T>();
                // 只查询存在的数据
                return Entity.AsQueryable<T>().Where(x=>x.IsActived==true);
            }
        }

         public IQueryable<T> DeleteTable
        {
            get
            {
                // 只查询删除的数据
                return Entity.AsQueryable<T>().Where(x=>x.IsDeleted==true);
            }
        }


        /// <summary>
        /// 删除(指定T类型，在数据库当中代表指定的表)指定Id的记录
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var t = Entity.Where(x => x.Id == id).FirstOrDefault();
            Entity.Remove(t);
            _db.SaveChanges();
        }

        public T GetById(int id)
        {
            var t = Entity.Where(x => x.Id == id).FirstOrDefault();
            return t;
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new System.NullReferenceException();
            }

            entity.IsActived = true;
            entity.IsDeleted = false;
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entity.DisplayOrder = 0;

            Entity.Add(entity);
            _db.SaveChanges();
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.NullReferenceException();
            }

            entity.IsActived = true;
            entity.IsDeleted = false;
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entity.DisplayOrder = 0;

            await Entity.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public void InsertBulk(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsActived = true;
                entity.IsDeleted = false;
                entity.CreatedTime = DateTime.Now;
                entity.UpdatedTime = DateTime.Now;
                entity.DisplayOrder = 0;
            }
            Entity.AddRange(entities);
            _db.SaveChanges();
        }

        public async Task InsertBulkAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsActived = true;
                entity.IsDeleted = false;
                entity.CreatedTime = DateTime.Now;
                entity.UpdatedTime = DateTime.Now;
                entity.DisplayOrder = 0;
            }
            await Entity.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new System.NullReferenceException();
            }

            entity.UpdatedTime = DateTime.Now;
            _db.SaveChanges();
        }

        public void UpdateBulk(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedTime = DateTime.Now;
            }


            _db.SaveChanges();
        }
    }
}