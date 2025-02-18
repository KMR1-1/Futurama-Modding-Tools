using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public class ChildNode : Node
    {
        public override List<INode> Children { get; set; }
        public ChildNode(ref Gltf gltf, NiNode niNode) : base(ref gltf, niNode)
        {
            Children = new List<INode>();
            Mesh = null;
            var meshlist = new MeshList(ref gltf, niNode);

            if (meshlist != null && meshlist.childlist != null)
                foreach (var child in meshlist.childlist)
                    Children.Add(child);
  
            
        }
    }
}