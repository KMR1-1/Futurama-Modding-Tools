
namespace FuturamaLib.GLTF.Init
{
    public class Gltf
    {
        public Variables variables { get; set; }
        public GltfCounter counter {get; set;} = new GltfCounter();
        public GltfStructure structure {get; set;} = new GltfStructure();
        public int nodetex;

        public Gltf(string nifDir, string projDir)
        {
            variables = new Variables(nifDir, projDir);
            Kill();
        }
        public void ToDict()
        {
            var name = Path.GetFileName(variables.folderManager.roomPath);
            var json = structure.ToDict();
            var outPath = Path.Combine(variables.folderManager.roomPath, $"{name}.gltf");
            File.WriteAllText(outPath, json);
            var converter = new ConvertToGbl(outPath);
            converter.Convert(outPath.Replace(".gltf", ".gbl"));

            var offsetsPath = Path.Combine(variables.folderManager.roomPath,"offsets.json");
            var offsets = variables.offsets.ToDict();
            File.WriteAllText(offsetsPath, offsets);

            Kill();
        }
        public void Kill()
        {
            structure.Kill();
            counter.Kill();
        }
    }
}