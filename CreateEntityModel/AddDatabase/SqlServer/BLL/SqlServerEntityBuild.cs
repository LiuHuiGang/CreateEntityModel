using CreateEntityModel.AddDatabase.SqlServer.Model;
using CreateEntityModel.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateEntityModel.AddDatabase.SqlServer.BLL
{
    public class SqlServerEntityBuild : IEntityBuild
    {
        private List<TableInfo> TableInfo { get; set; }
        public SqlServerEntityBuild(List<TableInfo> tableInfo)
        {
            TableInfo = tableInfo;
        }

        public ICreateEntityFile EntityBuild(Action<EntityBuildModel> options)
        {
            EntityBuildModel model = new EntityBuildModel();
            options(model);
            Dictionary<string, string> fileContent = new Dictionary<string, string>();
            foreach (var item in TableInfo)
            {
                string content = CreatModel(item.TableName, item.ColumnInfos, model.NamespaceName, model.Using);
                fileContent.Add(item.TableName, content);
            }
            return new CreateEntityFile(fileContent);
        }

        private string CreatModel(string tableName, List<ColumnInfo> columnInfos, string namespaceName, List<string> reference = null)
        {
            StringBuilder attribute = new StringBuilder();
            foreach (ColumnInfo item in columnInfos)
            {
                attribute.AppendSpaceLine("/// <summary>", 8);
                attribute.AppendSpaceLine($"/// {item.Description}", 8);
                attribute.AppendSpaceLine("/// </summary>", 8);
                attribute.AppendSpaceLine($"public {TypeCast(item.TypeName)} {item.ColumnName}", 8);
                attribute.AppendLine(" { get; set; }");
            }

            StringBuilder strclass = new StringBuilder();
            strclass.AppendSpaceLine("using System;", 0, 0);
            strclass.AppendSpaceLine("using System.Collections.Generic;");
            strclass.AppendSpaceLine("using System.Text;");

            if (reference != null)
            {
                reference.ForEach(o =>
                {
                    strclass.AppendSpaceLine($"using {o};");
                });
            }
            strclass.AppendSpaceLine($"namespace {namespaceName}", 0, 2);
            strclass.AppendSpaceLine("{");
            strclass.AppendSpaceLine($"public class {tableName}", 4);
            strclass.AppendSpaceLine("{", 4);
            //封装属性
            strclass.AppendSpaceLine(attribute.ToString());
            strclass.AppendSpaceLine("}", 4);
            strclass.AppendSpaceLine("}");
            return strclass.ToString();
        }

        /// <summary>
        /// 数据库字段类型转换为C#属性类型
        /// </summary>
        /// <param name="TypeName">数据库字段类型</param>
        /// <returns></returns>
        private string TypeCast(string TypeName)
        {
            switch (TypeName.ToLower())
            {
                case "bit":
                    TypeName = "Boolean";
                    break;
                case "tinyint":
                    TypeName = "byte";
                    break;
                case "smallint":
                    TypeName = "Int16";
                    break;
                case "int":
                    TypeName = "int";
                    break;
                case "bigint":
                    TypeName = "Int64";
                    break;
                case "smallmoney":
                    TypeName = "decimal";
                    break;
                case "money":
                    TypeName = "decimal";
                    break;
                case "numeric":
                    TypeName = "decimal";
                    break;
                case "decimal":
                    TypeName = "decimal";
                    break;
                case "float":
                    TypeName = "double";
                    break;
                case "real":
                    TypeName = "Single";
                    break;
                case "smalldatetime":
                    TypeName = "DateTime";
                    break;
                case "datetime":
                    TypeName = "DateTime";
                    break;
                case "timestamp":
                    TypeName = "DateTime";
                    break;
                case "char":
                    TypeName = "string";
                    break;
                case "text":
                    TypeName = "string";
                    break;
                case "varchar":
                    TypeName = "string";
                    break;
                case "nchar":
                    TypeName = "string";
                    break;
                case "ntext":
                    TypeName = "string";
                    break;
                case "nvarchar":
                    TypeName = "string";
                    break;
                case "binary":
                    TypeName = "byte[]";
                    break;
                case "varbinary":
                    TypeName = "byte[]";
                    break;
                case "image":
                    TypeName = "byte[]";
                    break;
                case "uniqueidentifier":
                    TypeName = "Guid";
                    break;
                case "Variant":
                    TypeName = "object";
                    break;
                default:
                    TypeName = TypeName.ToLower();
                    break;
            }
            return TypeName;
        }
    }
}
