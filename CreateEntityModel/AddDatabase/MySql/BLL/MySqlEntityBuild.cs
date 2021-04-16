using CreateEntityModel.AddDatabase.MySql.Model;
using CreateEntityModel.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateEntityModel.AddDatabase.MySql.BLL
{
    public class MySqlEntityBuild: IEntityBuild
    {
        private List<TableInfo> TableInfo { get; set; }
        public MySqlEntityBuild(List<TableInfo> tableInfo)
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
              string content = CreatModel(item.TableName,item.ColumnInfos, model.NamespaceName, model.Using);
                fileContent.Add(item.TableName, content);
            }
            return new CreateEntityFile(fileContent);
        }

        private string CreatModel(string tableName, List<ColumnInfo> columnInfos, string namespaceName, List<string> reference = null)
        {
            StringBuilder attribute = new StringBuilder();
            foreach (ColumnInfo item in columnInfos)
            {
                attribute.AppendSpaceLine("/// <summary>",8);
                attribute.AppendSpaceLine($"/// {item.Description}",8);
                attribute.AppendSpaceLine("/// </summary>",8);
                attribute.AppendSpaceLine($"public {TypeCast(item.TypeName)} {item.ColumnName}", 8);
                attribute.AppendLine(" { get; set; }");
            }

            StringBuilder strclass = new StringBuilder();
            strclass.AppendSpaceLine("using System;",0,0);
            strclass.AppendSpaceLine("using System.Collections.Generic;");
            strclass.AppendSpaceLine("using System.Text;");

            if (reference != null)
            {
                reference.ForEach(o=> 
                {
                    strclass.AppendSpaceLine($"using {o};");
                });
            }
            strclass.AppendSpaceLine($"namespace {namespaceName}",0,2);
            strclass.AppendSpaceLine("{");
            strclass.AppendSpaceLine($"public class {tableName}",4);
            strclass.AppendSpaceLine("{",4);
            //封装属性
            strclass.AppendSpaceLine(attribute.ToString());
            strclass.AppendSpaceLine("}",4);
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
                case "bigint":
                    TypeName = "Int64?";
                    break;
                case "binary":
                    TypeName = "byte[]";
                    break;
                case "bit":
                    TypeName = "Boolean?";
                    break;
                case "char":
                    TypeName = "string";
                    break;
                case "date":
                    TypeName = "DateTime?";
                    break;
                case "datetime":
                    TypeName = "DateTime?";
                    break;
                case "decimal":
                    TypeName = "decimal?";
                    break;
                case "double":
                    TypeName = "double?";
                    break;
                case "float":
                    TypeName = "float?";
                    break;
                case "int":
                    TypeName = "int?";
                    break;
                case "text":
                    TypeName = "string";
                    break;
                case "time":
                    TypeName = "DateTime?";
                    break;
                case "tinyint":
                    TypeName = "int?";
                    break;
                case "varbinary":
                    TypeName = "byte[]";
                    break;
                case "varchar":
                    TypeName = "string";
                    break;
                case "smallint":
                    TypeName = "int?";
                    break;
                case "mediumint":
                    TypeName = "int?";
                    break;
                case "longtext":
                    TypeName = "string";
                    break;
                default:
                    TypeName = TypeName.ToLower();
                    break;
            }
            return TypeName;
        }
    }
}
