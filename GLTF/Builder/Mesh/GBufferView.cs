
using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    class GBufferView
    {
        public GBufferView(string type, uint count, ref Gltf gltf)
        {
            int btlen;
            switch(type)
            {
                case "VEC3":
                    btlen = (int)(count*12);
                    break;
                case "VEC2":
                    btlen = (int)(count*8);
                    break;
                case "VEC4":
                    btlen = (int)(count*16);
                    break;
                case "SCALAR":
                    btlen = (int)(count*2);
                    break;
                default:
                    throw new ArgumentException($"Type '{type}' non reconnu.");
            }

            var bufferView = new Dictionary<string, object>
            {
                {"buffer", gltf.counter.mesh},
                {"byteOffset", gltf.counter.bufferOffset},
                {"byteLength", btlen},
                {"target", 34962},
            };
            if(type == "SCALAR")
                bufferView["target"] = 34963;

            gltf.structure.bufferViews.Add(bufferView);
            gltf.counter.bufferOffset += btlen;
        }
    }
}