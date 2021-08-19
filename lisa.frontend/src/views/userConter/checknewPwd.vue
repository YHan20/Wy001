<template>
  <div class="login_container">
    <div class="login_body">
      <div class="login-form">
        <el-link size="medium" type="primary" icon="el-icon-s-promotion" href="/login"></el-link>
        <el-steps :active="1" finish-status="success" simple style="margin-top: 20px;margin-bottom: 2rem;">
          <el-step title="步骤 1"></el-step>
          <el-step title="步骤 2"></el-step>
          <el-step title="步骤 3"></el-step>
        </el-steps>

        <el-form
          :model="formData"
          status-icon
          :rules="rules"
          ref="ruleForm"
          label-width="0"
          class="demo-ruleForm"
        >
          <p class="para-style">请在下面输入您的电子邮件地址，我们将给您发送一封带有说明的电子邮件。</p>
					<p class="para-style-2"><strong>需要帮助?</strong>了解更多关于如何</p>
          <el-form-item prop="username">
            <el-input type="email" prefix-icon="el-icon-user" placeholder="邮 箱" required></el-input>
          </el-form-item>

          <el-form-item prop="password">
            <el-input type="password" prefix-icon="el-icon-lock" show-password placeholder="请输入密码"></el-input>
          </el-form-item>

          <el-form-item style="height: 2rem;">
            <el-button type="success" icon="el-icon-check" style="width: 25%;margin-left: 75%;">确 定</el-button>
          </el-form-item>
        </el-form>
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
// import { login } from "../../api/users";
export default {
  
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

body.el-scrollbar__wrap {overflow-x: hidden;}

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