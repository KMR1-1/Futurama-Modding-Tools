using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    class InstancesNode : ChildNode
    {
        public string fileName{get;set;}
        public InstancesNode(ref Gltf gltf, NiNode niNode, string fileName): base(ref gltf, niNode)
        {
            this.fileName = fileName;
            foreach (var child in niNode.Children)
            {
                if(child.Object is NiUDSNode niUDSNode)
                {
                    Children.Add(new UDSNode(ref gltf, niUDSNode, fileName));
                }
            }
        }
    }
}