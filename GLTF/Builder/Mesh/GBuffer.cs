using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    class GBuffer
    {
        public GBuffer(string name, ref Gltf gltf)
        {
            var buffer = new Dictionary<string, object>
            {
                {"uri", Path.Combine("data", name)},
                {"byteLength", gltf.counter.bufferOffset},
            };
            gltf.structure.buffers.Add(buffer);
        }
    }
}