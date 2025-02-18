using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    public class ProcessUV
    {
        public ProcessUV(int i, BinaryWriter writer, NiTriStripsData data, ref Gltf gltf)
        {
            foreach (var u in data.UVSets[i])
            {
                writer.Write(u.X);
                writer.Write(u.Y);
            }
            new GAccessor((uint)data.UVSets[i].Length, "VEC2", ref gltf);
        }
    }
}