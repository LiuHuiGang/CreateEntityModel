using System;
using System.Collections.Generic;
using System.Text;

namespace CreateEntityModel.AddDatabase
{
    public class EntityBuildModel
    {
        public string NamespaceName { get; set; } = "Models";
        public List<string> Using { get; set; }
    }
}
