using System;
using System.Collections.Generic;
using EcsCore;
using Leopotam.Ecs;

namespace FlyAdventure
{
    public abstract class EcsModuleWithDependencies : EcsModule
    {
        protected Dictionary<Type, object> Dependencies { get; }
        protected EcsWorld World => EcsWorldContainer.World;

        public EcsModuleWithDependencies()
        {
            Dependencies = new Dictionary<Type, object>();
        }

        protected override Dictionary<Type, object> GetDependencies()
        {
            return Dependencies;
        }
    }
}