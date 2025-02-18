using Newtonsoft.Json;
using FuturamaLib.GLTF.Builder.NodeStructures;

namespace FuturamaLib.GLTF.Init
{
    public class GltfStructure
    {
        public List<Node> nodeStructure { get; set; }
        public List<Node> instStructure { get; set; }
        public List<Node> instList { get; set; }
        public Dictionary<string, object> asset { get; set; }
        public List<int> scenes { get; set; }
        public List<Dictionary<string, object>> nodes { get; set; }
        public List<Dictionary<string, object>> meshes { get; set; }
        public List<Dictionary<string, object>> accessors { get; set; }
        public List<Dictionary<string, object>> bufferViews { get; set; }
        public List<Dictionary<string, object>> buffers { get; set; }
        public List<Dictionary<string, object>> materials { get; set; }
        public List<Dictionary<string, object>> samplers { get; set; }
        public List<Dictionary<string, object>> textures { get; set; }
        public List<Dictionary<string, object>> images { get; set; }
        public List<Dictionary<string, object>> animations { get; set; }
        public GltfStructure()
        {
        }
        public string ToDict()
        {
            var gltfile = new Dictionary<string, object>
                {
                    {"asset", asset},
                    {"scene", 0},
                    {"scenes", new List<Dictionary<string, object>>{{new Dictionary<string, object>{{"nodes", scenes }}}}},
                    {"nodes", nodes},
                    {"meshes", meshes},
                    {"accessors", accessors},
                    {"bufferViews", bufferViews},
                    {"buffers", buffers},
                    {"materials", materials},
                    {"textures", textures},
                    {"samplers", samplers},
                    {"images", images},
                };

            return JsonConvert.SerializeObject(gltfile, Formatting.Indented);            
        }
        public void Kill()
        {
            asset = new Dictionary<string, object>{{ "version", "2.0" }};
            scenes = new List<int>();
            nodes = new List<Dictionary<string, object>>();
            meshes = new List<Dictionary<string, object>>();
            accessors = new List<Dictionary<string, object>>();
            bufferViews = new List<Dictionary<string, object>>();
            buffers = new List<Dictionary<string, object>>();
            materials = new List<Dictionary<string, object>>();
            textures = new List<Dictionary<string, object>>();
            samplers = new List<Dictionary<string, object>>();
            images = new List<Dictionary<string, object>>();
            animations = new List<Dictionary<string, object>>();
        }
    }
}