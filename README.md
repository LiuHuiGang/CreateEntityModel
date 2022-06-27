# CreateEntityModel
   
   //程序入口
   static void Main(string[] args)
        {
            //数据库连接语句
            string connectionString = "";
            //文件创建保存地址
            string path = @"C:\Users\Administrator\Desktop\log\model\";

             new CreateDefaultBuilder()      //创建默认构建器
              //.AddSqlServer(connectionString) 添加SqlServer数据库
                .AddMySql(connectionString)  //添加MySql数据库
                .EntityBuild(options =>      //实体类构建
                {
                    //命名空间名（必填）
                    options.NamespaceName = "Model";
                    //引用程序集（选填）
                    options.Using = new List<string>
                    {
                        "System.IO",
                        "CreateEntityModel.AddDatabase.MySql.BLL"
                    };
                    //使用委托自定义类名规则（默认类名与表名一致，选填）
                    options.CustomClassName = fileName => fileName+"_Model";
                })
                .Create(path);   //创建文件

            Console.WriteLine("完成");
            Console.ReadLine();
        }
