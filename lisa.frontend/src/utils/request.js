import axios from 'axios'
import { getToken,clearToken } from '../utils/auth'

const instance = axios.create({
    baseURL: 'http://localhost:5000/',
    timeout: 5000
  });

instance.interceptors.request.use((config)=>{
  let token = getToken()
  if(token){
    config.headers['Authorization'] = 'Bearer' + token
  }
  return config
},(err)=>{
    return Promise.reject(err)
  }
)

instance.interceptors.request.use(
  (response)=>{
    if(response.status===200){
      return response.data
    }else if(response.status===415){
      clearToken()
    }
    return response
  },(err)=>{
    return Promise.reject(err)
  }
)

  export default instance