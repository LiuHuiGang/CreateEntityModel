using CreateEntityModel.AddDatabase.MySql.DAL;
using CreateEntityModel.CreateEntityModelBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateEntityModel.AddDatabase.MySql.BLL
{
    public static class MySqlExtend
    {
        public static IEntityBuild AddMySql(this CreateDefaultBuilder builder,string connectionString)
        {
            var database = new DAL.MySql(connectionString);
            var tableInfos = database.GetTableInfo();
            return new MySqlEntityBuild(tableInfos);
        }
    }
}
