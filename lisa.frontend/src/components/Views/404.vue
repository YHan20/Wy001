<template>
	<div style="text-align: center;width: 100%;">
		<el-empty description=" ">

			<h2 data-text="404">404</h2>
			<h4>啊呀~页面不存在!</h4>
			<p>您可以先检查网址，然后重新输入或给我们反馈问题。</p>
			<el-button type="primary" plain round @click="gohome">返回首页</el-button>
			<el-button type="primary" round @click="goback">返回上一页</el-button>
		</el-empty>
	</div> 
</template>

<script>
	export default {
		methods: {
			// 返回首页界面
			gohome(){
				location.href="#/"
			},
			// 返回上一层界面
			goback(){
				this.$router.go(-1);
			}
		}
	}
</script>

<style>

</style>