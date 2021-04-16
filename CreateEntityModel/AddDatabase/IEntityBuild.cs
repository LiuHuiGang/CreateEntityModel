using CreateEntityModel.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateEntityModel.AddDatabase
{
    public interface IEntityBuild
    {
        ICreateEntityFile EntityBuild(Action<EntityBuildModel> options);
    }
}
