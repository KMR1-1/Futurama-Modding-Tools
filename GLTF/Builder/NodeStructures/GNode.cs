using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    class GNode
    {
        public GNode(INode node, ref Gltf gltf)
        {
            var nodedict = new Dictionary<string, object>()
            {
                {"name", node.Name},
                {"translation", node.Translations},
                {"scale", node.Scale},
                {"rotation", node.Rotations}
            };
            var extradict = new Dictionary<string, object>
            {
                {"posOffset", node.PosOffset},
            };

            if (node.Type == "MeshNode")
            {
                nodedict["mesh"] = node.Mesh;
            }
            else
            {
                if (node.Children.Count != 0)
                    nodedict["children"] = GetChildIdList(node);
            }
            if(node is UDSNode uDSNode)
            {
                extradict["file"] = uDSNode.fileName;
            }
            if(node is InstancesNode inode)
            {
                extradict["file"] = inode.fileName;
            }
            if(node is BuildInstance)
            {
                extradict["file"] = "defs";
            }
            nodedict["extras"] = extradict;
            gltf.structure.nodes.Add(nodedict);
        }

        public List<int> GetChildIdList(INode node)
        {
            var childlist = new List<int>();
            foreach (var child in node.Children)
            {
                childlist.Add(child.Id);
            }
            return childlist;
        }
    }
}