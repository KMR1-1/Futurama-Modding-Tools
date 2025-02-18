using Newtonsoft.Json;

namespace FuturamaLib.GLTF.Init
{
    class DebugStructure<T>
    {
        public DebugStructure(List<T> list, Gltf gltf, string name)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            string output = Path.Combine(gltf.variables.folderManager.outPutPath, "debug", $"{name}.json");
            File.WriteAllText(output, json);
        }
    }
}