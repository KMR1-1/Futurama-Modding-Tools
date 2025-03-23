using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    public class TexAlphaMode
    {
        public string alphaMode {get; set;}
        public TexAlphaMode(NiNode niNode, NiAVObject mesh)
        {
            alphaMode = "MASK";
            foreach (var ppt in niNode.Properties)
            {
                if (ppt.Object is NiAlphaProperty)
                {
                    alphaMode = "BLEND";
                }
            }
            foreach (var prop1 in mesh.Properties)
            {
                if (prop1.Object is NiAlphaProperty)
                {
                    alphaMode = "BLEND";
                }
            }
        }
    }
}