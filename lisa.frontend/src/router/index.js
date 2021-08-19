//使用会import很多模块，或者组件
import Vue from "vue";
import VueRouter from 'vue-router'
import routes from './routes'
import { isAuth } from '../utils/auth'

// 引入的VueRouter插件
Vue.use(VueRouter)

let router = new VueRouter({
    mode:'history',
    routes
})

router.beforeEach((to,from,next)=>{
    let isLogined = isAuth()

    if(to.path === '/login'){
        if(isLogined){
            next('/')
        }else{
            next()
        }
    }else if(to.path === '/register'){
        if(isLogined){
            next('/')
        }else{
            next()
        }
    }else if(to.path === '/checknewPwd'){
        if(isLogined){
            next('/')
        }else{
            next()
        }
    }
    else{
        if(isLogined){
            next()
        }else{
            next('/login')
        }
    }
})

export default router 