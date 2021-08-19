<template>
  <div class="login_container">
    <div class="login_body">
      <div class="login-form">
        <div class="login-logo">
          <!-- <img class="logo" src="assets/img/img.png"> -->
          <h2>用户登录</h2>
        </div>
        <el-form :model="formData" status-icon :rules="rules" @keyup.enter.native="submitForm('ruleForm')" ref="ruleForm" label-width="0" class="demo-ruleForm">
          <el-form-item prop="username">
            <el-input type="text" v-model="formData.username" prefix-icon="el-icon-user" clearable placeholder="用户名 / 手机 / 邮箱"></el-input>
          </el-form-item>

          <el-form-item prop="password">
            <el-input type="password" v-model="formData.password" prefix-icon="el-icon-lock" clearable show-password placeholder="请输入密码"></el-input>
          </el-form-item>

          <el-form-item style="margin-bottom: 10px;">
            <el-row>
              <el-col :span="12">
                <el-checkbox v-model="checked" style="color:#a0a0a0;" label="记住账户"></el-checkbox>
              </el-col>
              <el-col :span="12" style="text-align: right;">
                <el-button @click="clearCookie" type="text">忘记密码？</el-button>
              </el-col>
            </el-row>
          </el-form-item>

          <el-form-item style="height: 2rem;">
            <el-button type="primary" style="width: 45%;margin-left: 1rem;" @click="submitForm('ruleForm')" round>登 录</el-button>
            <el-button type="success" style="width: 45%;" @click="resetForm('ruleForm')" round>重 置</el-button>
          </el-form-item>
        </el-form>

        <el-divider> 其他登录方式 </el-divider>
        <div class="login-oauth">
          <el-button size="medium" type="primary" icon="el-icon-platform-eleme" circle></el-button>
          <el-button size="medium" type="success" icon="el-icon-s-goods" circle></el-button>
          <el-button size="medium" type="info" icon="el-icon-s-promotion" circle></el-button>
          <el-button size="medium" type="warning" icon="el-icon-menu" circle></el-button>
          <el-link size="medium" type="primary" icon="el-icon-edit-outline" href="/register">注 册</el-link>
        </div>
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
import { login } from "../../api/users";
export default {
  data() {
    return {
      formData: {
        username: "",
        password: ""
      },
      rules: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" }
        ],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" }
        ]
      },
      isRemember: false,
      checked: true,
      // clearCookie:"",
    };
  },
  methods: {
    //忘记密码页面跳转
    clearCookie(){
      this.$router.push("/checknewPwd");
    },
    submitForm(formName) {
      //保存的账号
      var name = this.formData.username;
      //保存的密码
      var pass = this.formData.password;
      //判断复选框是否被勾选 勾选则调用配置cookie方法
      if (this.checked) {
        //传入账号名，密码，和保存天数3个参数
        this.setCookie(name, pass, 7);
      }
      this.$refs[formName].validate(valid => {
        if (valid) {
          login(this.formData)
            .then(({ data }) => {
              let res = data;
              console.log(res);
              if (res.code === 1000) {
                localStorage.setItem("accessToken", res.data.token);
                localStorage.setItem("refreshToken", res.data.refreshToken);
                this.$router.push("/");
                //开启欢迎词
                this.$message.success("欢迎登陆 / 登录成功")
              } else {
                this.$message({ type: "error", message: res.msg });
              }
            })
            .catch(err => {
              console.log(err);
            });
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
    //设置cookie
    setCookie(c_name, c_pwd, exdays) {
      var exdate = new Date(); //获取时间
      exdate.setTime(exdate.getTime() + 24 * 60 * 60 * 1000 * exdays); //保存的天数
      //字符串拼接cookie
      window.document.cookie =
        "username" + "=" + c_name + ";path=/;expires=" + exdate.toGMTString();
      window.document.cookie =
        "password" + "=" + c_pwd + ";path=/;expires=" + exdate.toGMTString();
    },
    //读取cookie
    getCookie: function() {
      console.log(document.cookie)
      if (document.cookie.length > 0) {
        var arr = document.cookie.split("; "); //这里显示的格式需要切割一下自己可输出看下
        for (var i = 0; i < arr.length; i++) {
          var arr2 = arr[i].split("="); //再次切割
          //判断查找相对应的值
          if (arr2[0] == "username") {
            this.formData.username = arr2[1]; //保存到保存数据的地方
          } else if (arr2[0] == "password") {
            this.formData.password = arr2[1];
          }
        }
      }
    },
    //清除cookie
    // clearCookie: function() {
    //   this.setCookie("", "", -1); //修改2值都为空，天数为负1天就好了
    // }
  },
  //页面加载调用获取cookie值
  mounted() {
    this.getCookie();
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