using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;


namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public class MeshList
    {
        public List<MeshNode> childlist { get; set; }
        public MeshList(ref Gltf gltf, NiNode node)
        {
            var meshlist = GetMeshList(node); //list of mesh objects
            childlist = new List<MeshNode>();
            foreach (var mesh in meshlist)
                childlist.Add(new MeshNode(ref gltf, mesh, node));
            
        }
        public static List<NiTriStrips> GetMeshList(NiNode node)
        {
            var meshlist = new List<NiTriStrips>();
            foreach (var childmesh in node.Children)
                if (childmesh.Object is NiTriStrips niTriStrips)
                    meshlist.Add(niTriStrips);
                
            return meshlist;
        }
    }
}