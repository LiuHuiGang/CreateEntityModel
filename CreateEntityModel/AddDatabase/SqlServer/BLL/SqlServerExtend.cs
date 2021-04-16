using CreateEntityModel.CreateEntityModelBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateEntityModel.AddDatabase.SqlServer.BLL
{
    public static class SqlServerExtend
    {
        public static IEntityBuild AddSqlServer(this CreateDefaultBuilder builder, string connectionString)
        {
            var database = new DAL.SqlServer(connectionString);
            var tableInfos = database.GetTableInfo();
            return new SqlServerEntityBuild(tableInfos);
        }
    }
}
