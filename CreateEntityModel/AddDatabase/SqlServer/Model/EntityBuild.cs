using System;
using System.Collections.Generic;
using System.Text;

namespace CreateEntityModel.AddDatabase.SqlServer.Model
{
    public class EntityBuild
    {
        public string NamespaceName { get; set; } = "Models";
        public List<string> Using { get; set; }
    }
}
