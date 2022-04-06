using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Random = Unity.Mathematics.Random;

namespace TheTalesOfTwo.Core.Obstacles.Patterns
{
    public class PatternsRawContainer
    {
        private readonly string[] _patterns;
        private Random _random;
        public PatternsRawContainer(string patterns)
        {
            using var sr = new StringReader(patterns);
            using var reader = new JsonTextReader(sr);
            var token = JToken.ReadFrom(reader);
            var p = token.Value<JArray>("patterns");
            if (p == null)
                throw new ArgumentException("Can't find patterns. Json: " + patterns);
            _patterns = p.Select(t => t.ToString()).ToArray();
            _random = new Random();
            _random.InitState((uint)DateTime.Now.Ticks);
        }

        /// <summary>
        /// Возвращает json-объект со случайным паттерном
        /// </summary>
        public string GetRandom()
        {
            return _patterns[_random.NextInt(0, _patterns.Length)];
        }
    }
}