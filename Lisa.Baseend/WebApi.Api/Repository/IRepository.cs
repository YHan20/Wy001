using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Api.Repository
{
    
    public interface IRepository<T>
    {
        IQueryable<T> Table { get; }
        IQueryable<T> DeleteTable { get; }

        /// <summary>
        /// 根据Id获取指定实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// 使用实体对象，插入数据
        /// </summary>
        /// <param name="entity">需要插入的对象</param>
        void Insert(T entity);

        /// <summary>
        /// 使用实体对象，插入数据（异步）
        /// </summary>
        /// <param name="entity">需要插入的对象</param>
        /// <returns></returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// 批量插入若干数据
        /// </summary>
        /// <param name="entities">待插入的若干实体数据</param>
        void InsertBulk(IEnumerable<T> entities);

        /// <summary>
        /// 批量插入若干数据（异步）
        /// </summary>
        /// <param name="entities">待插入的若干实体数据</param>
        /// <returns></returns>
        Task InsertBulkAsync(IEnumerable<T> entities);

        /// <summary>
        /// 根据对象更新数据
        /// </summary>
        /// <param name="entity">要更新的对象</param>
        void Update(T entity);


        /// <summary>
        /// 根据对象更新数据（异步）
        /// </summary>
        /// <param name="entities">要更新的若干个实体</param>
        /// <returns></returns>
        void UpdateBulk(IEnumerable<T> entities);

        /// <summary>
        /// 根据Id删除对应的记录
        /// </summary>
        /// <param name="id">主键id</param>
        void Delete(int id);
    }
}