using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.Mesh
{
    class ProcessGeometry
    {
        public string binpath { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object> primitives;
        public ProcessGeometry(string name, ref Gltf gltf, NiTriStripsData data)
        {
            Name = $"{gltf.counter.mesh}_{name}.bin";
            binpath = Path.Combine(gltf.variables.folderManager.roomPath, "data", Name);
            primitives = new Dictionary<string, object>();
            WriteBinFile(ref gltf, data);
        }
        public void WriteBinFile(ref Gltf gltf, NiTriStripsData data)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(binpath, FileMode.Create)))
            {
                var attributes = new Dictionary<string, object>();

                gltf.counter.bufferOffset = 0;
                if (data.HasVertices)
                {
                    attributes["POSITION"] = gltf.counter.accessor;
                    new ProcessVertex(writer, data, ref gltf);
                }
                if (data.HasNormals)
                {
                    attributes["NORMAL"] = gltf.counter.accessor;
                    new ProcessNormals(writer, data, ref gltf);
                }
                if (data.UVSets.Length != 0)
                {
                    for (int i = 0; i < data.UVSets.Length; i++)
                    {
                        attributes[$"TEXCOORD_{i}"] = gltf.counter.accessor;
                        new ProcessUV(i, writer, data, ref gltf);
                    }
                }
                if (data.HasVertexColors)
                {
                    attributes["COLOR_0"] = gltf.counter.accessor;
                    new ProcessVertexColors(writer, data, ref gltf);
                }
                primitives["attributes"] = attributes;
                primitives["indices"] = gltf.counter.accessor;
                new ProcessIndices(writer, data, ref gltf);
                new GBuffer(Name, ref gltf);
            }
        }
    }
}