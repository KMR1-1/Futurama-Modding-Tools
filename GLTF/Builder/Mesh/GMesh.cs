
using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    public class GMesh
    {
        public string name { get; set; }
        public Dictionary<string, object> mdict { get; set; }
        public GMesh(NiNode niNode, NiTriStrips mesh, ref Gltf gltf, string name)
        {
            mdict = new Dictionary<string, object>();
            var primitives = new Dictionary<string, object>();

            if (mesh.Data.IsValid && mesh.Data.Object is NiTriStripsData data && data != null)
            {
                var geometry = new ProcessGeometry(name, ref gltf, data);
                primitives = geometry.primitives;
            }
            if (HasTexture(niNode, mesh))
            {
                var texture = new ProcessTexture(name, mesh, ref gltf, niNode);
                primitives["material"] = texture.mcount;
            }
            mdict["name"] = name;
            mdict["primitives"] = new List<Dictionary<string, object>> { primitives };
            gltf.structure.meshes.Add(mdict);
        }
        public bool HasTexture(NiNode node, NiTriStrips mesh)
        {
            bool pr = false;
            foreach (var prop in node.Properties)
            {
                if (prop.Object is NiTexturingProperty)
                    pr = true;
            }
            foreach (var prop in mesh.Properties)
            {
                if (prop.Object is NiTexturingProperty)
                    pr = true;
            }
            return pr;
        }
    }
}