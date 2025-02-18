using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;
using FuturamaLib.GLTF.Calculs;
using Newtonsoft.Json.Serialization;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public class InstanceNode : ChildNode
    {
        public InstanceNode(ref Gltf gltf, NiNode niNode) : base(ref gltf, niNode)
        {
            if (niNode.Name.Value.Contains("::"))
            {
                Name = niNode.Name.Value.Split("::")[1];
            }
            foreach (var prop in niNode.Properties)
            {
                if (prop.Object is NiTexturingProperty tex)
                {
                    if (tex.BaseTexture.Source.Object.InternalTexture.Object is NiPixelData texdata)
                    {
                        string tgapath = Path.Combine(gltf.variables.folderManager.outPutPath,"external",$"{gltf.counter.node}_{Name}.tga");
                        new TGAGenerator(texdata, tgapath);
                        TgaToPng.ConvertirImageTgaEnPng(tgapath);
                    }
                }
            }
            foreach (var child in niNode.Children)
                if (child.Object is NiNode childnode)
                    Children.Add(new InstanceNode(ref gltf, childnode));
        }
    }
}
//TODO export pickup picked up icons (nodes with textures, without mesh) 
//TODO if obj is CharDef (external), instance is in /char/*.ucf (a nif btw) with SkinInstance anim data
//like fry or nibbler
//TODO something that supports NiSwitchNode for FireEstinguisher_Window break
//"window node" switches to "glass shards node"
//same for NiLODNode ex: complex Bender in Toilet model gets lowpoly when far distance, SwitchNode Load levels