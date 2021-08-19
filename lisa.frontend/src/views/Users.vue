<template>
  <div>
    <el-form :inline="true" class="demo-form-inline">
      <el-form-item>
        <el-input size="small" placeholder="用户名 / 备注"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button size="small" icon="el-icon-search" type="primary" @click="handleQuery">查 询</el-button>
        <el-button size="small" icon="el-icon-edit-outline" type="success" @click="handleAdd">添 加</el-button>
        <el-button size="small" icon="el-icon-refresh" type="primary" @click="handleRefresh">刷新</el-button>
      </el-form-item>
    </el-form>

    <el-table :data="tableData" height="545" @keyup.enter.native="handleSave" style="width: 100%" border>
      <el-table-column label="Id" width="180">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column label="用户名" width="180">
        <template slot-scope="scope">
          <!-- 用户拦弹出框 -->
          <el-popover trigger="hover" placement="top">
            <p>姓 名: {{ scope.row.username }}</p>
            <p>密 码: {{ scope.row.password }}</p>
            <p>备 注: {{ scope.row.remarks }}</p>
            <div slot="reference" class="name-wrapper">
              <el-tag size="medium">{{ scope.row.username }}</el-tag>
            </div>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column label="备 注" prop="remarks"></el-table-column>

      <el-table-column label="操 作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编 辑</el-button>
          <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删 除</el-button>
        </template>

        <template>
          <el-button type="text" @click="handleDelete"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <!-- 模态框，用于用户的增加和修改 -->
    <el-dialog title="用户信息"  @keyup.enter.native="handleSave" :visible.sync="dialogFormVisible">
      <el-form :model="formData">
        <el-form-item label="用户名" :label-width="formLabelWidth">
          <el-input v-model="formData.username" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="备注" :label-width="formLabelWidth">
          <el-input v-model="formData.remarks" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" @click="handleCancel">取 消</el-button>
        <el-button size="small" type="primary" @click="handleSave">确 定</el-button>
      </div>
    </el-dialog>
    <!-- 分页组件 -->
    <div class="block">
      <el-pagination
        @size-change="handlePageSizeChange"
        @current-change="handlePageIndexChange"
        :current-page="pager.pageIndex"
        :page-size="pager.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="pager.rowsTotal"
        background
      >
      </el-pagination>
    </div>
  </div>
</template>

<script>
import { getList, addUser, modUser ,deleteUsersUser} from "../api/users";
export default {
  data() {
    //调用 data 函数 / 返回函数 / 避免了数据污染。
    return {
      tableData: [{}],
      pager: {
        pageIndex: 1,
        pageSize: 10,
        rowsTotal: 30,
      },
      formData: {
        id: 0,
        username: "",
        password: "",
        remarks: "",
      },
      formLabelWidth: "120px",
      dialogFormVisible: false,
      currentIndex: 0,
      filterTableData:[],
      allData:[],
      show: false,
      showText: "查看已删除表",
      deletedData: [],
    };
  },
  mounted() {
    //拉取全部的数据
    this.fetchData(this.pager);
    // 获取全部表数据
    this.getData();
  },
  methods: {
    //添加用户
    handleAdd() {
      this.dialogFormVisible = true;
      this.formData.id = 0;
      this.formData.username = "";
      this.formData.password = "1112";
    },
     handleQuery() {
      this.fetchData();
      this.$message.success("查找数据成功^_^");
    },
    handleRefresh(){
      this.fetchData();
      this.$message.success("刷新数据成功^_^")
    },
    //用户修改
    handleEdit(index, row) {
      this.dialogFormVisible = true;
      this.formData.id = row.id;
      this.formData.username = row.username;
      // this.formData.password = row.password;
      this.formData.remarks = row.remarks;
      this.currentIndex = index;

      console.log(index, row);
    },
    //删除用户
    handleDelete(index,row){
      this.$confirm('此操作将删除该文件, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        //触发删除功能
        deleteUsersUser(row.id).then((data)=>{
        if(data.data.code === 1000){
          this.tableData.splice(index,1);
          this.deletedData.splice(0,0,data.data.data)
          this.$message.success(data.msg);
        }else{
          this.$message.error(data.msg)
        }
        }).catch((err)=>{
          console.log(err);
        });
        this.$message({
          type: 'success',
          message: '删除成功!'
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });          
      });
    },
    //添加修改后，保存信息
    handleSave() {
      console.log(this.formData);
      // 如果表单中的id为0，则认为是添加用户，反之则为修改用户
      if (this.formData.id === 0) {
        addUser(this.formData)
          .then(({ data }) => {
            console.log(data);
            //将新添加的数据push到现有表数据的最后面
            this.tableData.push(data.data);
          })
          .catch((err) => {
            console.log(err);
          });
          this.$message({
            type: 'success',
            message: '添加成功!'
          });
      } else {
        //修改
        modUser(this.formData.id, this.formData)
          .then(({ data }) => {
            console.log(data);
            this.tableData.splice(this.currentIndex, 1, data.data);
          })
          .catch((err) => {
            console.log(err);
          });
          //修改成功提示
          this.$message({
            type: 'success',
            message: '修改成功!'
          });
      }
      this.dialogFormVisible = false;
    },
    //取消
    handleCancel() {
      this.dialogFormVisible = false;
      //取消成功提示
      this.$message({
        type: 'info',
        message: '取消成功'
      }); 
    },
    //页面页码改变时，重新拉取数据
    handlePageIndexChange(val) {
      console.log(val);
      this.pager.pageIndex = val > 0 ? val : 1;
      this.fetchData(this.pager);
    },
    //页面大小改变时，重新拉取数据
    handlePageSizeChange(val) {
      console.log(val);
      this.pager.pageSize = val;
      this.fetchData(this.pager);
    },
    //拉取后台数据 / 获取表操作
    fetchData(pager) {
      getList(pager).then(({ data }) => {
        this.tableData = data.data.data;
        this.pager = data.data.pager;
      });
    },
    // 获取整个表 
    ///////////////////
    getData() {
      getList(null).then((data) => {
        let res = data.data.data;
        console.log(res);
        console.log(res.data);
        return (this.allData = res.data.data), (this.deletedData = res.deletedData);
      });
    },
    // 搜索
    
  },
};
</script>

<style>

</style>