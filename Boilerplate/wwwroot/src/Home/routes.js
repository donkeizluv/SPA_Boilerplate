import store from './store'

import hello from './Component/Hello.vue'
import login from './Component/Login.vue'



function requireAuth(to, from, next) {
    if (!store.getters.IsAuthenticated) {
        next({
            path: '/Login',
            query: { redirect: to.path }
        })
    } else {
        next()
    }
}

const routes = [
    //Default
    {
        path: '/',
        redirect: '/Home'
    },
    //Auth
    { path: '/Home', name: 'Home', component: hello, display: 'Trang chính', navbar: true, beforeEnter: requireAuth },
    //No auth
    { path: '/Login', name: 'Login', component: login, display: 'Đăng nhập', navbar: false }];

module.exports = routes;