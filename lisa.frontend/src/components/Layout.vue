<template>
  <div>
    <el-container>
      <!-- 左侧导航栏 aside-->
      <el-aside :style="{ width: isCollapse ? 'auto' : '200px' }">
        <navbar :menus="menus" :collapse="isCollapse"></navbar>
      </el-aside>
      <!-- 右侧内容框 -->
      <el-container class="container">
        <!-- 右侧导航栏 header -->
        <el-header>
          <el-row type="flex" justify="center">
            <el-col :span="4">
              <div class="grid-content bg-purple">
                <i
                  class="el-collapse el-icon-s-fold"
                  :class="isCollapse ? 'collapseTransform' : ''"
                  @click="handleCollapse"
                ></i>
              </div>
            </el-col>
            <!-- ll -->
            <el-col :span="4">
              <div class="grid-content bg-purple-light"></div>
            </el-col>
            <el-col :span="4">
              <div class="grid-content bg-purple"></div>
            </el-col>
            <el-col :span="4">
              <div class="grid-content bg-purple-light"></div>
            </el-col>
            <el-col :span="4"
              ><div class="grid-content bg-purple">
                <el-button
                  style="margin-top: 0.4rem; margin-left: 8rem"
                  icon="el-icon-rank"
                  @click="screen"
                ></el-button>
              </div>
            </el-col>
            <el-col :span="4"
              ><div class="grid-content bg-purple-light">
                <el-dropdown trigger="click">
                  <template>
                    <div class="demo-type">
                      <div>
                        <el-avatar
                          src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png"
                        ></el-avatar>
                        <i class="el-icon-arrow-down el-icon-caret-bottom"></i>
                      </div>
                    </div>
                  </template>
                  <el-dropdown-menu slot="dropdown">
                    <el-dropdown-item icon="el-icon-edit-outline"
                      >主 页</el-dropdown-item
                    >
                    <el-dropdown-item icon="el-icon-setting"
                      >设 置</el-dropdown-item
                    >
                    <el-dropdown-item icon="el-icon-info"
                      >帮 助</el-dropdown-item
                    >
                    <el-dropdown-item confirm>
                      <el-popconfirm
                        confirm-button-text="注销"
                        @confirm="handleConfirmLogout"
                        cancel-button-text="不用"
                        icon="el-icon-info"
                        icon-color="red"
                        title="确定注销！！！"
                      >
                        <el-button
                          style="border: none; margin-left: -1.27rem"
                          icon="el-icon-s-promotion"
                          slot="reference"
                          >退 出
                        </el-button>
                      </el-popconfirm>
                    </el-dropdown-item>
                  </el-dropdown-menu>
                </el-dropdown>
              </div></el-col
            >
          </el-row>
        </el-header>

        <!-- 右侧内容框 main -->
        <el-main>
          <router-view></router-view>
        </el-main>

        <!-- 右侧底栏 footer -->
        <el-footer>Copyright © 2021 没啥用科技 All Rights Reserved</el-footer>
      </el-container>
    </el-container>
  </div>
</template>

<script>
// 引入需要的模块
import routes from "@/router/routes";
import Navbar from "@/components/Navbar";

export default {
  components: {
    Navbar,
  },
  data() {
    return {
      // 左侧导航栏隐藏按钮
      isCollapse: false,
      // 下拉注销按钮控件
      dialogVisible: false,
      //满屏空键
      fullscreen: false,
    };
  },
  methods: {
    resolve(arr, parentPath = "") {
      let newArr = [];

      //遍历数组中的每一个元素
      arr.forEach((route) => {
        let obj = Object.assign({}, route);

        parentPath =
          parentPath && parentPath.lastIndexOf("/") !== parentPath.length - 1
            ? parentPath + "/"
            : parentPath;

        // 总是给加上父级路径
        obj.path = parentPath + obj.path;
        // console.log(obj.path);
        // 判断有没有children属性 如有，那么需要再处理，如果没有则直接push到一个新数组中
        if (obj.children && obj.children.length > 0) {
          obj.children = this.resolve(obj.children, obj.path);
        }
        // 如果当前的路由原信息hidden信息，那么就将其节点提升上来
        if (obj.meta && obj.meta.hidden) {
          if (obj.children && obj.children.length > 0) {
            obj.children.forEach((item) => {
              let tmp = Object.assign({}, item);
              newArr.push(tmp);
            });
          }
        } else {
          newArr.push(obj);
        }
      });
      // console.log(newArr);
      return newArr;
    },
    //折叠图标
    handleCollapse() {
      this.isCollapse = !this.isCollapse;
    },
    //注销按钮
    handleLogout() {
      this.dialogVisible = true;
    },
    //注销下拉框
    handleConfirmLogout() {
      this.dialogVisible = false;
      // 在这里把用户名和token保存起来
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
      this.$router.push("/login").then(() => {
        //添加注销成功提示功能
        this.$message({
          type: "success",
          message: "注销成功!",
        });
      });
    },
    //点击按钮实现全屏功能
    screen() {
      let element = document.documentElement;
      if (this.fullscreen) {
        if (document.exitFullscreen) {
          document.exitFullscreen();
        } else if (document.webkitCancelFullScreen) {
          document.webkitCancelFullScreen();
        } else if (document.mozCancelFullScreen) {
          document.mozCancelFullScreen();
        } else if (document.msExitFullscreen) {
          document.msExitFullscreen();
        }
      } else {
        if (element.requestFullscreen) {
          element.requestFullscreen();
        } else if (element.webkitRequestFullScreen) {
          element.webkitRequestFullScreen();
        } else if (element.mozRequestFullScreen) {
          element.mozRequestFullScreen();
        } else if (element.msRequestFullscreen) {
          element.msRequestFullscreen();
        }
      }
      //判断当前是否是全屏
      this.fullscreen = !this.fullscreen;
    },
  },
  computed: {
    menus() {
      let arr = this.resolve(routes);
      return arr;
    },
  },
};
</script>


<style>
.el-aside {
  width: 200px;
}
.el-main {
  background-color: azure;
}
.el-footer {
  background-color: rgb(198, 238, 238);
  display: flex;
  align-items: center;
  justify-content: center;
}
.el-collapse {
  font-size: 1.6rem;
  margin-top: 1rem;
}
.container {
  height: 100vh;
}
/* 收起图标旋转180度 */
.collapseTransform {transform: rotate(-180deg);}
/* 头像 / 用户名样式 */
.demo-type {margin-top: 0.4rem;margin-left: 8rem;}
/* header分栏高度 */
.grid-content {min-height: 3.6rem;}

/* header分隔颜色样式 */
/* .bg-purple-dark {background: #99a9bf;}
.bg-purple {background: #d3dce6;}
.bg-purple-light {background: #e5e9f2;} 
.row-bg {padding: 10px 0;background-color: #f9fafc;}
*/
</style>