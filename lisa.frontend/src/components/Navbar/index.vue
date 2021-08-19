<template>
  <el-scrollbar>
    <el-menu
      default-active="2"
      class="el-menu-vertical-demo"
      background-color="azure"
      router
      unique-opened
      :collapse="collapse"
      :collapse-transition="false"
    >
      <navbar-item :menus="menus" :collapse="collapse"></navbar-item>
    </el-menu>
  </el-scrollbar>
</template>

<script>
import NavbarItem from "./NavbarItem.vue";
export default {
  components: {
    NavbarItem,
  },
  props: {
    menus: {
      type: Array,
      require: true,
    },
    collapse:{
        type:Boolean,
        default:false
    }
  },
};
</script>

<style scoped>
.el-menu{
  height: 100vh;
}
</style>