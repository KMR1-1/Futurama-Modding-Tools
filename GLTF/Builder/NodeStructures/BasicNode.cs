using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public class BasicNode : ChildNode
    {
        public BasicNode(ref Gltf gltf, NiNode niNode): base(ref gltf, niNode)
        {
            foreach (var child in niNode.Children)
                if (child.Object is NiNode childnode)
                    Children.Add(new BasicNode(ref gltf, childnode));
        }
    }
}