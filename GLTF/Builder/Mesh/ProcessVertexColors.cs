using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    public class ProcessVertexColors
    {
        public ProcessVertexColors(BinaryWriter writer, NiTriStripsData data, ref Gltf gltf)
        {
            foreach (var vc in data.VertexColors)
            {
                writer.Write(vc.R);
                writer.Write(vc.G);
                writer.Write(vc.B);
                writer.Write(vc.A);
            }
            new GAccessor(data.NumVertices, "VEC4", ref gltf);
        }
    }
}