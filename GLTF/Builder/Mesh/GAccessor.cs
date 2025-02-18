using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    class GAccessor
    {
        public GAccessor(uint count, string type, ref Gltf gltf, List<float>? min = null, List<float>? max = null)
        {
            min ??= new List<float>();
            max ??= new List<float>();
            var accessor =  new Dictionary<string, object>
            {
                {"bufferView", gltf.counter.accessor},
                {"componentType", 5126},
                {"count", count},
                {"type", type},
            };

            if(type == "SCALAR")
                accessor["componentType"] = 5123;

            if(min.Count != 0 && max.Count != 0)
            {
                accessor["min"] = min;
                accessor["max"] = max;
            }

            gltf.structure.accessors.Add(accessor);
            new GBufferView(type, count, ref gltf);

            gltf.counter.accessor++;    
        }
    }
}