using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    public class ProcessVertex
    {
        public ProcessVertex(BinaryWriter writer, NiTriStripsData data, ref Gltf gltf)
        {
            var v = data.Vertices;
            var min = new List<float> { v[0].X, v[0].Y, v[0].Z };
            var max = new List<float> { v[0].X, v[0].Y, v[0].Z };

            foreach (var vx in v)
            {
                writer.Write(vx.X);
                writer.Write(vx.Y);
                writer.Write(vx.Z);
                min[0] = Math.Min(min[0], vx.X);
                min[1] = Math.Min(min[1], vx.Y);
                min[2] = Math.Min(min[2], vx.Z);

                max[0] = Math.Max(max[0], vx.X);
                max[1] = Math.Max(max[1], vx.Y);
                max[2] = Math.Max(max[2], vx.Z);
            }
            new GAccessor(data.NumVertices, "VEC3", ref gltf, min, max);
        }
    }
}