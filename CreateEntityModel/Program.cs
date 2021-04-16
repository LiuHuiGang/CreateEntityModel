using CreateEntityModel.AddDatabase.MySql.BLL;
using CreateEntityModel.AddDatabase.SqlServer.BLL;
using CreateEntityModel.CreateEntityModelBuilder;
using System;
using System.Collections.Generic;
using System.IO;

namespace CreateEntityModel
{
    class Program
    {
        static void Main(string[] args)
        {
            //数据库连接语句
            string connectionString = "";
            //文件创建保存地址
            string path = @"C:\Users\Administrator\Desktop\log\model\";
            //创建默认构建器>添加数据库>实体类构建>创建文件
            new CreateDefaultBuilder().AddMySql(connectionString)
                .EntityBuild(o=> 
                {
                    o.NamespaceName = "Moeel";
                    o.Using = new List<string>
                    {
                        "System.IO",
                        "CreateEntityModel.AddDatabase.MySql.BLL"
                    };
                })
                .Create(path);

            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}
