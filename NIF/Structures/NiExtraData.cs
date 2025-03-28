using System.IO;

namespace FuturamaLib.NIF.Structures
{
    public class NiExtraData : NiObject
    {
        public byte[] ExtraData;

        public NiRef<NiExtraData> NextExtraData;

        public NiExtraData(NIFReader file, BinaryReader reader) : base(file, reader)
        {
            NextExtraData = new NiRef<NiExtraData>(reader.ReadUInt32());

            if (GetType() == typeof(NiExtraData))
            {
                ExtraData = reader.ReadBytes(reader.ReadInt32());
            }
        }
        public virtual Dictionary<string, object> GetDetails()
        {
            return new Dictionary<string, object>
            {
                {"ExtraData", ExtraData},
                {"NextExtraData", NextExtraData.IsValid ? NextExtraData.Object.GetType().Name : null},
            };
        }
    }
}