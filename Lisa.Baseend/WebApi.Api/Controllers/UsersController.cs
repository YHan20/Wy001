using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Api.Entity;
using System.Linq;
using Newtonsoft.Json;
using WebApi.Api.ParamModel;
using WebApi.Api.Repository;
using WebApi.Api.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace MyWebApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IRepository<Users> _usersRespository;
        private TokenParameter _tokenParameter;
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration, IRepository<Users> usersRespository)
        {
            _configuration = configuration;
            _usersRespository = usersRespository;
            _tokenParameter = _configuration.GetSection("TokenParameter").Get<TokenParameter>();
        }

        [HttpGet]
        public dynamic Get([FromQuery] Pager pager)
        {
            //get请求默认从uri从获取参数，所以此处应该加上FromQuery的特性才能正确获得参数
            var pageIndex = pager.PageIndex;
            var pageSize = pager.PageSize;

            // 获得所有满足条件的记录
            var users = _usersRespository.Table;
            if(pageIndex == 0 && pageSize == 0){
                var activedTable = users.ToList();
                var deletedTable = _usersRespository.DeleteTable.ToList();
                return new
                {
                    Code = 1000,
                    Data = new {
                        Data = activedTable,Pager = new{
                            pageIndex,pageSize,rowsTotal = users.Count()
                        },
                        DeletedData = deletedTable
                    },
                    Msg = "获取用户列表成功"
                };
            }else{
                // 对获得的记录分页
                // 返回的数据里，带有数据和分页信息
                var activedTable = users.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList();
                return new {
                    Code = 1000,
                    Data = new {
                        Data = activedTable,Pager = new {
                            pageIndex,pageSize,rowsTotal = users.Count()
                        }
                    },
                    Msg = "获取用户列表成功"
                };
            }
        }

        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            var user = _usersRespository.GetById(id);
            // return user;
            if (user == null)
            {
                return new
                {
                    Code = 1000,
                    Data = "",
                    Msg = "指定用户不存在"
                };

            }
            return new
            {
                Code = 1000,
                Data = user,
                Msg = "获取用户列表成功"
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public string Post(NewUser model)
        {
            // JsonSerializerSettings settings = new JsonSerializerSettings();

            // settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var username = model.Username.Trim();
            var password = model.Password.Trim();
            var remarks = model.Remarks.Trim();

            if (string.IsNullOrEmpty(username))
            {
                var tmp = new
                {
                    Code = 1004,
                    Data = "",
                    Msg = "用户名不能为空，请确认后重试"
                };
                return JsonHelper.Serialize(tmp);
            }

            var user = new Users
            {
                Username = username,
                Password = password,
                Remarks = remarks
            };

            _usersRespository.Insert(user);

            var res = new
            {
                Code = 1000,
                Data = user,
                Msg = "创建用户成功"
            };
            return JsonHelper.Serialize(res);
        }

        [AllowAnonymous]
        //修改
        [HttpPut("{id}")]
        public dynamic Put(int id, NewUser model)
        {
            var user = _usersRespository.GetById(id);

            if (user != null)
            {
                user.Username = model.Username;
                // user.Password = model.Password;
                user.Remarks = model.Remarks;
                user.UpdatedTime = DateTime.Now;
                _usersRespository.Update(user);
                return JsonHelper.Serialize(new
                {
                    Code = 1000,
                    Data = user,
                    Msg = string.Format("你修改的用户的id为：{0}，已经修改成功，请注意查收", id)
                });
            }
            else
            {
                return JsonHelper.Serialize(new
                {
                    Code = 104,
                    Data = "",
                    Msg = "指定Id的用户不存在，请确认后重试"
                });
            }
        }

        //删除
        [HttpDelete("{id}")]
        public dynamic Delete(int id)
        {
            var user = _usersRespository.GetById(id);
            if (user != null)
            {
                //伪删除
                user.IsActived = false;
                user.IsDeleted = true;
                _usersRespository.Delete(id);
                return new
                {
                    Code = 1000,
                    Data = user,
                    Msg = string.Format("删除指定id为{0}的用户成功", id)
                };
            }
            else
            {
                return new
                {
                    Code = 1000,
                    Data = "",
                    Msg = "指定id的用户不存在，请确认重试"
                };
            }
        }

        // 用户登录
        [AllowAnonymous]
        [HttpPost, Route("token")]
        public dynamic GetToken(NewUser model)
        {
            var username = model.Username.Trim();
            var password = model.Password.Trim();

            var user = _usersRespository.Table.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

            if (user == null)
            {
                return new
                {
                    Code = 1001,
                    Data = user,
                    Msg = "咋回事 / 用户名或密码都不正确 / 回家核对后再来重试"
                };
            }

            var token = TokenHelper.GenerateToekn(_tokenParameter, user);
            var refreshToken = "123456";

            var res = new
            {
                Code = 1000,
                Data = new { Token = token, RefreshToken = refreshToken },
                Msg = "用户登录成功"
            };
            return res;
        }

        [AllowAnonymous]
        // 用户注册
        [HttpPost, Route("register")]
        public dynamic Register(NewUser model)
        {
            var username = model.Username.Trim();
            var password = model.Password.Trim();
            var remarks = model.Remarks.Trim();

            var userInfo = _usersRespository.Table.Where(x => x.Username == username).FirstOrDefault();
            if (userInfo == null)
            {
                if (string.IsNullOrEmpty(username))
                {
                    var res = new
                    {
                        Code = 1004,
                        Data = "",
                        Msg = "用户名不能为空，请确认后重试"
                    };
                    return JsonHelper.Serialize(res);
                }
                else
                {
                    var user = new Users
                    {
                        Username = username,
                        Password = password,
                        Remarks = remarks
                    };
                    _usersRespository.Insert(user);
                    var res = new
                    {
                        Code = 1000,
                        Data = user,
                        Msg = "创建用户成功"
                    };
                    return JsonHelper.Serialize(res);
                }
            }
            else
            {
                var res = new
                {
                    Code = 1004,
                    Data = "",
                    Msg = "该用户已经注册过 / 就不要来凑热闹啦"
                };
                return res;
            }

        }

        // private IEnumerable<Users> GetUsers()
        // {
        //     var users = new List<Users>{
        //         new Users{
        //             Id=1,
        //             Username="你好",
        //             Password="确实好啊"
        //         },
        //         new Users{
        //             Id=2,
        //             Username="真的吗",
        //             Password="不啊"
        //         },
        //         new Users{
        //             Id=3,
        //             Username="可以吗",
        //             Password="我觉得不行"
        //         },
        //     };
        //     return users;
        // }


    }
}
