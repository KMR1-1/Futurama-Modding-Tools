using FuturamaLib.NIF.Structures;
using System.Numerics;

namespace FuturamaLib.GLTF.Calculs
{
    class CalculateNorm
    {
        public Vector3 normal { get; set; }
        public CalculateNorm(NiTriStripsData data, int nindices)
        {
            normal = CalculateNormalAverage(data, nindices);
            //Futurama uses Vertex Normals, so one normal for each vertex
            //A Vertex normal is a vector that represents the orientation of the vertex, the vector has a lenght of 1 unit
            //The game also uses Strips to make triangles, so a Vertex can be part of 3 triangles
            //ex of triangles formed by triangle strip [0,1,2,3,4] => tri1 = [0,1,2], tri2 = [2,1,3], tri3 = [2,3,4], ....
            //the vertex number 2 is in the 3 triangles
            //so we calculate the normal vector of each of these 3 triangles, make an average of the 3 tri and normalize the result
            //in futurama there are normals with lenght of 0 (corrupt data), so we have to recalculate them here to make the 3D models work
        }
        public static Vector3 CalculateNormalAverage(NiTriStripsData data, int nindices)
        {
            var normallist = new List<Vector3>();
            foreach (var points in data.Points)
                for (var i = 2; i < points.Length; i++)
                    if (points[i - 2] == nindices || points[i - 1] == nindices || points[i] == nindices)
                        if (i % 2 == 0)
                            normallist.Add(CalculateNormal(data, points[i - 2], points[i - 1], points[i]));
                        else
                            normallist.Add(CalculateNormal(data, points[i - 1], points[i - 2], points[i]));

            var sum = Vector3.Zero;
            foreach (var vector in normallist)
                sum += vector;

            return normallist.Count > 0 ? Vector3.Normalize(sum / normallist.Count) : Vector3.Zero;
        }

        public static Vector3 CalculateNormal(NiTriStripsData data, int P1, int P2, int P3)
        {
            var V1 = data.Vertices[P1];
            var V2 = data.Vertices[P2];
            var V3 = data.Vertices[P3];

            return Vector3.Cross(V2 - V1, V3 - V1);
        }
    }
}