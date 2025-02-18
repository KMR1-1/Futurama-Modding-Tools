using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    class GBuild
    {
        public GBuild(ref Gltf gltf)
        {
            foreach (var node in gltf.structure.nodeStructure)
            {
                BuildGltfNodes(node, ref gltf);
            }
            gltf.ToDict();
        }

        public void BuildGltfNodes(INode node, ref Gltf gltf)
        {
            new GNode(node, ref gltf);
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                    BuildGltfNodes(child, ref gltf);
            }
        }
    }
}