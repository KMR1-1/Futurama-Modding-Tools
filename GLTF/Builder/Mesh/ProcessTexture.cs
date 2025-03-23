
using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;
using FuturamaLib.GLTF.Builder.Mesh;

namespace FuturamaLib.GLTF.Builder
{
    public class ProcessTexture
    {
        public Dictionary<string, object> pbrMetallicRoughness {get; set;} = new Dictionary<string, object>();
        public Dictionary<string, object> materialdict {get; set;}
        public string name {get; set;}
        public int mcount;
        public ProcessTexture(string name, NiTriStrips mesh, ref Gltf gltf, NiNode niNode)
        {
            this.name = name;
            materialdict = new Dictionary<string, object>
            {
                {"name", name}
            };
            foreach (var prop in mesh.Properties)
            {
                if (prop.Object is NiTexturingProperty tex)
                {
                    ProcessTex(ref gltf, tex);
                }
            }
            foreach (var prop in niNode.Properties)
            {
                if (prop.Object is NiTexturingProperty tex)
                {
                    ProcessTex(ref gltf, tex);
                }
            }

            var alpha = new TexAlphaMode(niNode, mesh);
            materialdict["alphaMode"] = alpha.alphaMode;
            materialdict["pbrMetallicRoughness"] = pbrMetallicRoughness;
            gltf.structure.materials.Add(materialdict);
            

        }

        public void ProcessTex(ref Gltf gltf, NiTexturingProperty tex)
        {
            var image = new GImage(tex, ref gltf, name);
            var texref = image.texref;

            var texture = new GTexture(ref gltf, tex, gltf.counter.refIdToCounter[texref]);
            pbrMetallicRoughness = texture.pbrMetallicRoughness;
            mcount = gltf.counter.texture++;
        }
        public static void CreateMaterial(NiMaterialProperty mat)
        {
            //var d = mat.DiffuseColor;
            //var am = mat.AmbientColor;
            //var s = mat.SpecularColor;
            //var e = mat.EmissiveColor;
            //var a = mat.Alpha;

            //pbrMetallicRoughness["baseColorFactor"] = new List<float> { d.R, d.G, d.B, a };
            //materialdict["emissiveFactor"] = new List<float> { e.R*d.R/255, e.G*d.G/255, e.G*d.G/255};
            //pbrMetallicRoughness["roughnessFactor"] = 1 - mat.Glossiness;
            //if(mat.Glossiness > 1)
            //{
            //    pbrMetallicRoughness["roughnessFactor"] = 0;
            //}

            //TODO handle mat.SpecularColor (default is vertexColor gradient of the mesh)
        }
    }
}