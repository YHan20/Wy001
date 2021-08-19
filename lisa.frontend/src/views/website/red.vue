<template>
<el-form :model="ruleForm" :rules="rules" ref="ruleForm" class="demo-ruleForm loginFrom">
 <h1 style="font-size: 1.5rem;color: #fff;font-weight: bold;padding: 1rem 0;">登陆</h1>
 <el-form-item prop="userName">
   <el-input placeholder="账号"  v-model.trim="ruleForm.userName"></el-input>
 </el-form-item>
 <el-form-item prop="password">
   <el-input  type="password" v-model.trim="ruleForm.password" placeholder="密码"></el-input>
 </el-form-item>
 <div style="padding: 1rem 0 2rem 0;" class="clear">
   <div class="rt">
     <el-checkbox v-model="checked" style="color:#a0a0a0;">记住账户</el-checkbox>
     <span @click="clearCookie" style="cursor: pointer;color: #f19149;font-size: 0.75rem;margin-left: 5px;">忘记密码？</span>
   </div>
  </div>
  <el-button type="primary" @click="submitForm('ruleForm')" style="width: 100%;">登陆</el-button>
</el-form>
</template>

<script>
export default {
  data() {
    return {
      ruleForm: {
        userName: "", //用户名
        password: "" //密码
      },
      checked:true,
      rules2: {
        userName: [
          { required: true, message: "请输入手机号", trigger: "blur" }
        ],
        password: [{ required: true, message: "请输入密码", trigger: "blur" }]
      },
    };
  },
  methods: {
    //点击登录调用方法
    submitForm(formName) {
      //保存的账号
      var name = this.ruleForm.userName;
      //保存的密码
      var pass = this.ruleForm.password;
      //判断复选框是否被勾选 勾选则调用配置cookie方法
      if (this.checked) {
        //传入账号名，密码，和保存天数3个参数
        this.setCookie(name, pass, 7);
      }
      return formName();
      //接口
      // var url = "myserver/user/login";
      // this.$http.post(url, this.ruleForm, { emulateJSON: true }).then(res => {
      //   if (res.body == "fail") {
      //     alert("用户名或密码错误,请重新输入");
      //     this.ruleForm.userName = "";
      //     this.ruleForm.password = "";
      //     return;
      //   } else {
      //     // alert（"登陆成功！"）
      //     this.$router.push("/index");
      //   }
      // });
    },
    //设置cookie
    setCookie(c_name, c_pwd, exdays) {
      var exdate = new Date(); //获取时间
      exdate.setTime(exdate.getTime() + 24 * 60 * 60 * 1000 * exdays); //保存的天数
      //字符串拼接cookie
      window.document.cookie =
        "userName" + "=" + c_name + ";path=/;expires=" + exdate.toGMTString();
      window.document.cookie =
        "userPwd" + "=" + c_pwd + ";path=/;expires=" + exdate.toGMTString();
    },
    //读取cookie
    getCookie: function() {
      console.log(document.cookie)
      if (document.cookie.length > 0) {
        var arr = document.cookie.split("; "); //这里显示的格式需要切割一下自己可输出看下
        for (var i = 0; i < arr.length; i++) {
          var arr2 = arr[i].split("="); //再次切割
          //判断查找相对应的值
          if (arr2[0] == "userName") {
            this.ruleForm.userName = arr2[1]; //保存到保存数据的地方
          } else if (arr2[0] == "userPwd") {
            this.ruleForm.password = arr2[1];
          }
        }
      }
    },
    //清除cookie
    clearCookie: function() {
      this.setCookie("", "", -1); //修改2值都为空，天数为负1天就好了
    }
  },
  //页面加载调用获取cookie值
  mounted() {
    this.getCookie();
  }
};
</script>