using System;
using System.Collections.Generic;
using EcsCore;
using Leopotam.Ecs;

namespace TheTalesOfTwo
{
    public abstract class EcsModuleWithDependencies : EcsModule
    {
        protected Dictionary<Type, object> Dependencies { get; }
        protected EcsWorld World => EcsWorldContainer.World;

        public EcsModuleWithDependencies()
        {
            Dependencies = new Dictionary<Type, object>();
        }

        public override Dictionary<Type, object> GetDependencies()
        {
            return Dependencies;
        }
    }
}