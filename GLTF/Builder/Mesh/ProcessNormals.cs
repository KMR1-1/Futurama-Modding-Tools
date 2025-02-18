using FuturamaLib.GLTF.Init;
using FuturamaLib.GLTF.Calculs;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    public class ProcessNormals
    {
        public ProcessNormals(BinaryWriter writer, NiTriStripsData data, ref Gltf gltf)
        {
            var nindice = 0;
            foreach (var n in data.Normals)
            {
                if (IsUnitVector(n.X, n.Y, n.Z)) // bug with some normals are (0,0,0)
                {
                writer.Write(n.X);
                writer.Write(n.Y);
                writer.Write(n.Z);
                }
                else
                {
                    var normal = new CalculateNorm(data, nindice).normal;
                    writer.Write(normal.X);
                    writer.Write(normal.Y);
                    writer.Write(normal.Z);
                }
                nindice++;
            }
            new GAccessor(data.NumVertices, "VEC3", ref gltf);
        }
        static bool IsUnitVector(float x, float y, float z, float tolerance = 0.0001f)
        {
            float magnitude = (float)Math.Sqrt(x * x + y * y + z * z);
            return Math.Abs(magnitude - 1.0f) <= tolerance;
        }
    }
}