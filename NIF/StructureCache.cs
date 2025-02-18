using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FuturamaLib.NIF
{
    internal static class StructureCache
    {
        private readonly static Dictionary<string, Type> Collection;

        static StructureCache()
        {
            var _namespace = typeof(StructureCache).Namespace;

            var assembly = Assembly.GetExecutingAssembly();
            if (assembly != null)
            {
                var typesInNamespace = assembly.GetTypes()
                    .Where(x => x.Namespace != null && x.Namespace.StartsWith(_namespace))
                    .ToList();

                // Log or debug print typesInNamespace to check what types are being found

                Collection = typesInNamespace
                    .ToDictionary(x => x.Name);
            }
            else
            {
                // Log or handle the case where assembly is null
                Collection = new Dictionary<string, Type>();
            }
        }


        public static bool TryGetValue(string name, out Type type) => Collection.TryGetValue(name, out type);
    }
}