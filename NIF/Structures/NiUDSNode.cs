using System.IO;

namespace FuturamaLib.NIF.Structures
{
    public class NiUDSNode : NiAVObject
    {
        public NiUDSNode(NIFReader file, BinaryReader reader) : base(file, reader)
        {
        }
        public override Dictionary<string, object> GetDetails()
        {
            var details = base.GetDetails();
            return details;
        }

    }
}