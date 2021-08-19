<template>
  <div class="login_container">
    <div class="login_body">
      <div class="login-form">
        <div class="login-logo">
          <!-- <img class="logo" src="assets/img/img.png"> -->
          <h2>用户注册</h2>
        </div>
        <el-form :model="formData" :rules="rules" status-icon @keyup.enter.native="submitForm('formData')" ref="formData" label-width="0" class="demo-ruleForm">
          <el-form-item prop="username">
            <el-input type="text" v-model="formData.username" prefix-icon="el-icon-user" placeholder="用户名 / 手机 / 邮箱"></el-input>
          </el-form-item>

          <el-form-item prop="password">
            <el-input type="password" v-model="formData.password" prefix-icon="el-icon-lock" clearable show-password placeholder="请输入密码"></el-input>
          </el-form-item>

          <el-form-item prop="confirmPassword">
            <el-input type="password" v-model="formData.confirmPassword" prefix-icon="el-icon-lock" clearable show-password placeholder="请输入密码"></el-input>
          </el-form-item>

          <el-form-item style="height: 2rem;">
            <el-button type="primary" style="width: 45%;margin-left: 1rem;" @click="submitForm('formData')" round>注 册</el-button>
            <el-button type="success" style="width: 45%;" @click="resetForm('formData')" round>重 置</el-button>
          </el-form-item>
        </el-form>

        <el-divider>
          <div class="login-oauth">
            <el-link size="medium" type="primary" icon="el-icon-edit-outline" href="/login">✔登 录✔</el-link>
          </div>
        </el-divider>
      </div>
      <div class="login-sidebox">
        <!-- <div class="login-sidebox__title">
					<h2>没啥用科技</h2>
					<h4>高性能 / 精致 / 优雅</h4>
					<p>基于Vue + Element</p>
        </div>-->
        <!-- <img src="img/loginbg.svg"/> -->
      </div>
    </div>
  </div>
</template>

<script>
import { register } from "../../api/users";
export default {
  data() {
    //用户名判断语句
    var user = (rule, value , callback)=>{
      if(!value){
        return callback(new Error("用户不能为空"));
      }else if(value.length < 3 || value.length>16){
        return callback(new Error('账号为 3-16 个字符'));
      }else{
        return callback();
      }
    };
    //密码判断语句
    var pwd = (rule ,value ,callback)=>{
      if(/[\u4E00-\u9FA5]/g.test(value)){
        return callback(new Error('密码由数字和英文字母组成'));
      }else if(!value){
        return callback(new Error('密码怎么是空的 / 得写上你的密码'));
      }else if(value.length < 3 || value.length > 16){
        return callback(new Error('密码为 3-16 个字符'));
      }else{
        return callback();
      }
    };
    //确认密码判断语句
    var confirmpwd = (rule,value,callback)=>{
      if(/[\u4E00-\u9FA5]/g.test(value)){
        return callback(new Error('密码由数字和英文字母组成'));
      }else if(!value){
        return callback(new Error('密码怎么是空的 / 得写上你的密码'));
      }else if(value.length < 3 || value.length > 16){
        return callback(new Error('密码为 3-16 个字符'));
      }else if(value != this.formData.password){
        return callback(new Error('密码不一致 / 重新去写吧你'));
      }else{
        return callback();
      }
    };
    return {
      formData: {
        username: "",
        password: "",
        confirmPassword:"",
        remarks:""
      },
      rules: {
        username: [
          { required: true, validator:user, trigger: "blur" }
        ],
        password: [
          { required: true, validator:pwd, trigger: "blur" }
        ],
        confirmPassword:[
          {required:true,validator:confirmpwd,trigger:"blur"}
        ]
      },
    };
  },
  methods: {
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          register(this.formData)
            .then(( data ) => {
              // let res = data;
              console.log(data);
              if (data.data.code === 1000) {
                // localStorage.setItem("accessToken", res.data.token);
                // localStorage.setItem("refreshToken", res.data.refreshToken);
                this.$router.push("/login");
                //开启欢迎词
                this.$message.success("欢迎注册 / 注册成功")
              } else {
                // this.$message({ type: "error", message: res.msg });
                this.$message({
                  message: data.data.msg + "",
                  type: "error",
                });
              }
            })
            .catch(err => {
              console.log(err);
            });
        } else {
          this.$message({
            message: "注册失败",
            type: "error",
          });
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
  }
};
</script>

<style>
.login_container {position: absolute;top:50%;left:50%;width: 1100px;margin: 0 auto;z-index: 1;transform: translate(-50%, -50%);}
.login_body {width: inherit;display: flex;box-shadow: 0px 20px 80px 0px rgba(0,0,0,0.3);}

.login-logo {text-align: center;margin-bottom: 30px;}
.login-logo .logo {width: 70px;height: 70px;vertical-align: bottom;}
.login-logo h2 {font-size: 24px;margin-top: 20px;color: #40485b;}

.login-form {width: 50%;padding: 60px 100px;background: #fff;}
.login-oauth {display: flex;justify-content:space-around;}
.login-form .el-divider {margin-top:40px;}


.login-sidebox {width: 50%;padding: 60px;color: #fff;background:#fff;position: relative;overflow: hidden;}
.login-sidebox__title h2 {font-size: 30px;}
.login-sidebox__title h4 {font-size: 18px;margin-top: 10px;font-weight: normal;}
.login-sidebox__title p {font-size: 14px;margin-top:10px;line-height: 1.8;color: rgba(255,255,255,0.6);}
.login-sidebox img {position: absolute;left:-120px;bottom:-160px;width: 550px;}

@media (max-height: 650px){
.login_container {position: static;transform: none;margin:50px auto;}
.login-footer {margin-bottom: 50px;}
}
@media (max-width: 1200px){
.login_container {width: 900px;}
.login-form {padding:60px;}
}
@media (max-width: 1000px){
.login_container {width: 100%;background: #fff;margin: 0;transform:none;top:0px;bottom:0px;left:0px;right: 0px;}
.login_body {box-shadow: none;}
.login-form {width:100%;padding:60px 40px;}
.login-sidebox {display: none;}
.login-footer {margin-top: 0;}
}
</style>