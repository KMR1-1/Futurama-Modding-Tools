using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    class GImage
    {
        public uint texref {get; set;}
        public GImage(NiTexturingProperty tex, ref Gltf gltf, string name)
        {
            texref = tex.BaseTexture.Source.Object.InternalTexture.RefId;
            if (tex.BaseTexture.Source.Object.InternalTexture.Object is NiPixelData texdata)
            {
                if (!gltf.counter.refIdToCounter.ContainsKey(texref)) //if image don't exists
                {
                    //TODO handle mipmap formats up to 1px image, current image is highest resolution mipmap
                    gltf.counter.refIdToCounter[texref] = gltf.counter.image++;
                    string tgapath = Path.Combine(gltf.variables.folderManager.roomPath,"texture",$"{name}.tga");
                    new TGAGenerator(texdata, tgapath);
                    TgaToPng.ConvertirImageTgaEnPng(tgapath);
                    string pngpath = Path.Combine("texture", $"{name}.png");
                    gltf.structure.images.Add(new Dictionary<string, object> { { "uri", pngpath } });
                    gltf.variables.offsets.image[name] = texdata.Offsets["PixelData"];
                }
            }
        }
    }
}