//引用模块，这里可以是../ 或者 @建议使用 @
import Layout from '../components/Layout'

let routes = [
    {
        // 父类
        path: '/',
        component: Layout,
        // meta对象/控制title和icon 死的
        redirect:{
            path:"/home"
        },
        meta:{
            title: "首页",
            icon:'el-icon-s-promotion',
            hidden:true
        },
        children: [
            {
                path: 'home',
                meta:{
                    title:'仪表盘',
                    icon:'el-icon-position'
                },
                component: () => import('../components/Views/Home')
            },
        ]
    },
    {
        // 父类
        path: '/system',
        meta:{
            title: "管理",
            icon:'el-icon-s-tools'
        },
        // 子类路由
        component: Layout,
        children: [
            {
                path: 'user',
                meta:{
                    title: '用户管理',
                    icon:'el-icon-crop'
                },
                component: () => import('../components/Views/Users'),
            },
            {
                path:'role', 
                meta:{
                    title:'角色管理',
                    icon:'el-icon-map-location'
                },
                component:()=>import('../components/Views/Role'),
            },
            {
                path: 'permission',
                meta:{
                    title: '权限管理',
                    icon:'el-icon-position'
                },
                component: () => import('../components/Views/Permission'),
            }
        ]
    },
    {
        // 父类
        path: '/commodity',
        meta:{
            title: "实验室",
            icon:'el-icon-coin'
        },
        // 子类路由
        component:Layout,
        children: [
            {
                path: "product",
                meta:{
                    title: "产品管理", 
                    icon:'el-icon-odometer'
                },
                component: () => import('../components/Views/404'),
            },
            {
                path: "category",
                meta:{
                    title: "品类管理",
                    icon:'el-icon-position'
                },
                component: () => import('../views/userConter/login'),
            },
            {
                path: "activity",
                meta:{ 
                    title: "没啥用活动", 
                    icon:'el-icon-bangzhu'
                },
                component: () => import('../views/Users'),
            },
            {
                path: "dome",
                meta:{ 
                    title: "dome测试", 
                    icon:'el-icon-bangzhu'
                },
                component: () => import('../views/dome'),
            },
        ]
    },
    {
        // 父类
        path: '/website',
        component: Layout,
        meta:{
            title: "网站",
            icon:'el-icon-s-promotion'
        },
        children: [
            {
                path: 'work',
                meta:{
                    title:'袜子',
                    icon:'el-icon-position'
                },
                component:()=>import('../views/website/work')
            },
            {
                path: 'blue',
                meta:{
                    title:'模组',
                    icon:'el-icon-position'
                },
                component:()=>import('../views/website/blue')
            },
        ]
    },
    {
        path: "/login",
        meta: {
            title: '登录',
            icon:'',
            hidden:true
        },
        component:()=>import('../views/userConter/login'),
    },
    {
        path: "/register",
        meta: {
            title: '注册',
            icon:'',
            hidden:true
        },
        component:()=>import('../views/userConter/register'),
    },
    {
        path: "/checknewPwd",
        meta: {
            title: '注册',
            icon:'',
            hidden:true
        },
        component:()=>import('../views/userConter/checknewPwd'),
    },
]

// 暴露出去
export default routes



