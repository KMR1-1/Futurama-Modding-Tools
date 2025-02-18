using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;
using FuturamaLib.GLTF.Builder.Mesh;


namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public class MeshNode : Node
    {
        public override int? Mesh {get; set;}
        public MeshNode(ref Gltf gltf, NiTriStrips mesh, NiNode node) : base(ref gltf, mesh)
        {
            if(string.IsNullOrEmpty(mesh.Name.Value))
            {
                Name = node.Name.Value;
                if(string.IsNullOrEmpty(Name))
                {
                    Name = gltf.counter.mesh.ToString();
                }
            }
            new GMesh(node, mesh, ref gltf, Name);
            Mesh = gltf.counter.mesh++;   
        }
    }
}