using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    public class GTexture
    {
        public Dictionary<string, object> pbrMetallicRoughness {get; set;} = new Dictionary<string, object>();
        public GTexture(ref Gltf gltf, NiTexturingProperty tex, int refid)
        {
            var texture = new Dictionary<string, object>
            {
                {"source", refid},
                {"sampler", gltf.counter.texture},
            };
            gltf.structure.textures.Add(texture);
            new GSampler(tex, ref gltf);

            var baseColorTexture = new Dictionary<string, object>
            {
                {"index", gltf.counter.texture},
                {"texCoord", tex.BaseTexture.UVSetIndex}
            };

            pbrMetallicRoughness["baseColorTexture"] = baseColorTexture;
        }
    }
}