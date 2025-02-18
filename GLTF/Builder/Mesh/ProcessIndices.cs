using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    public class ProcessIndices
    {
        public ProcessIndices(BinaryWriter writer, NiTriStripsData data, ref Gltf gltf)
        {
            int numtriangle = 0;
            foreach (var p in data.Points)
            {
                for (int i = 2; i < p.Length; i++)
                {
                    // Chaque triangle est défini par 3 sommets consécutifs
                    var triangle = new[] { p[i - 2], p[i - 1], p[i] };
                    if (i % 2 == 0)
                    {
                        writer.Write((ushort)triangle[0]);
                        writer.Write((ushort)triangle[1]);
                        writer.Write((ushort)triangle[2]);
                    }
                    else
                    {
                        writer.Write((ushort)triangle[0]);
                        writer.Write((ushort)triangle[2]);
                        writer.Write((ushort)triangle[1]);
                    }
                    numtriangle++;
                }
            }
            new GAccessor((uint)(data.NumTriangles * 3), "SCALAR", ref gltf);
        }
    }
}