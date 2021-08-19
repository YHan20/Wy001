// 定义token等的常量键名
const TokenKey = 'accessToken'
const RefreshTokenKey = 'refreshToken'

// 设置保存token和refreshToken
export function setToken(token, refreshToken) {
    localStorage.setItem(TokenKey, token)
    localStorage.setItem(RefreshTokenKey, refreshToken)
}

// 获取token
export function getToken() {
    return localStorage.getItem(TokenKey)
}

// 获取refreshToken
export function getRefreshToken() {
    return localStorage.getItem(RefreshTokenKey)
}

// 清除token和refreshToken
export function clearToken() {
    localStorage.removeItem(TokenKey)
    localStorage.removeItem(RefreshTokenKey)
}

// 判断有无登录
export function isAuth() {
    let token = getToken()
    if (token) {
        return true
    } else {
        return false
    }
}

