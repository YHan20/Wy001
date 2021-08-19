// @ 符号表示一个特定路径的别称，使用会import很多模块，或者组件
// import config from "@/config";
import request from '@/utils/request'

// 根据id查询用户
export function getById(id){
    return request.get(`/users/${id}`)
}

// 获取用户列表
export function getList(params){
    return request.get('/users',{params:params})
}
//用户登录
export function login(data){
    return request.post(`/users/token`,data)
}

//用户注册
export function register(data){
    return request.post(`/users/register`,data)
}

//添加用户
export function addUser(data){
    return request.post(`/users`,data)
}

//删除用户
export function deleteUsersUser(id){
    return request.delete(`/users/${id}`)
}
//修改用户
export function modUser(id,data){
    return request.put(`/users/${id}`,data)
}
