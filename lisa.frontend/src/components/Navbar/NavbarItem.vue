<template>
  <div>
    <!-- 循环菜单数组 -->
    <div v-for="menu in menus" :key="menu.path">
      <!-- 如果有子菜单 -->
      <el-submenu
        v-if="menu.children && menu.children.length"
        :index="menu.path"
        popper-append-to-body
      >
        <template slot="title">
          <i :class="menu.meta.icon"></i>
          <span :class="menuTitleDisplay">{{ menu.meta.title }}</span>
        </template>
        <navbar-item :menus="menu.children"></navbar-item>
      </el-submenu>
      <!-- 如果没有子菜单 -->
      <el-menu-item v-else :index="menu.path">
        
        <i :class="menu.meta.icon"></i>
        <span :class="menuTitleDisplay" slot="title">
          {{menu.meta.title}}
        </span>
      </el-menu-item>
    </div>
  </div>
</template>

<script>
export default {
  name: "NavbarItem",
  data() {
    return {
      menuTitleDisplay: {
        menu_title: this.collapse,
      },
    };
  },
  props: {
    menus: {
      type: Array,
      require: true,
    },
    collapse: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style>
/* 隐藏左则导航栏收起的字体和图标 */

/* v-show 隐藏 是display:'none'
v-if 隐藏是 visibility:hidden; */

.menu_title {display: none;}
/* 小图标隐藏  */
.el-icon-arrow-right {visibility: hidden;}
/* 字体小图标隐藏 */
.el-menu--collapse span{display: none;}
</style>