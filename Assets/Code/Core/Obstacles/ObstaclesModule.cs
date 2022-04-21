using System;
using System.Collections.Generic;
using EcsCore;
using TheTalesOfTwo.Core.Obstacles.Patterns;

namespace TheTalesOfTwo.Core.Obstacles
{
    public class ObstaclesModule : EcsModule
    {
        protected override Dictionary<Type, int> GetSystemsOrder()
        {
            return new Dictionary<Type, int>
            {
                { typeof(PatternsSystem), -100 }
            };
        }
    }
}