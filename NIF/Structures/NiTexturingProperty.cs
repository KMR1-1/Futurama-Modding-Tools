using System.IO;
using System.Numerics;

namespace FuturamaLib.NIF.Structures
{
    public class NiTexturingProperty : NiProperty
    {
        public ushort Flags;
        public uint ApplyMode;
        public uint TextureCount;
        
        public bool HasBaseTexture;
        public TexDesc BaseTexture;

        public bool HasDarkTexture;
        public TexDesc DarkTexture;

        public bool HasDetailTexture;
        public TexDesc DetailTexture;

        public bool HasGlossTexture;
        public TexDesc GlossTexture;

        public bool HasGlowTexture;
        public TexDesc GlowTexture;
        
        public bool HasDecal0Texture;
        public TexDesc Decal0Texture;

        public bool HasBumpMapTexture;
        public TexDesc BumpMapTexture;
        public float BumpMapLumaScale;
        public float BumpMapLumaOffset;
        public Vector3 BumpMapMatrix;

        public NiTexturingProperty(NIFReader file, BinaryReader reader) : base(file, reader)
        {
            Flags = reader.ReadUInt16();
            ApplyMode = reader.ReadUInt32();
            TextureCount = reader.ReadUInt32();
            HasBaseTexture = reader.ReadBoolean();
            if(HasBaseTexture)
            {
                BaseTexture = new TexDesc(reader);
            }
            HasDarkTexture = reader.ReadBoolean();
            if (HasDarkTexture)
            {
                DarkTexture = new TexDesc(reader);
            }
            HasDetailTexture = reader.ReadBoolean();
            if (HasDetailTexture)
            {
                DetailTexture = new TexDesc(reader);
            }
            HasGlossTexture = reader.ReadBoolean();
            if (HasGlossTexture)
            {
                GlossTexture = new TexDesc(reader);
            }
            HasGlowTexture = reader.ReadBoolean();
            if (HasGlowTexture)
            {
                GlowTexture = new TexDesc(reader);
            }
            HasBumpMapTexture = reader.ReadBoolean();
            if (HasBumpMapTexture)
            {
                BumpMapTexture = new TexDesc(reader);
                BumpMapLumaScale = reader.ReadSingle();
                BumpMapLumaOffset = reader.ReadSingle();
                BumpMapMatrix = reader.Read<Vector3>();
                reader.ReadSingle();
            }
            HasDecal0Texture = reader.ReadBoolean();
            if (HasDecal0Texture)
            {
                Decal0Texture = new TexDesc(reader);
            }
        }
    }
}