using NetworkCommsDotNet.DPSBase;
using ProtoBuf;
using System.IO;

namespace WLMData.Data.Packets
{
    [ProtoContract]
    public class WritingStatus : Serializer, IExplicitlySerialize
    {
        [ProtoMember(1)]
        public string id { get; private set; }

        [ProtoMember(2)]
        public bool isWriting { get; private set; }

        private WritingStatus() { }

        public WritingStatus(string id, bool isWriting)
        {
            this.id = id;
            this.isWriting = isWriting;
        }

        public void Serialize(Stream outputStream)
        {
            SerializeData(outputStream, id);
            SerializeData(outputStream, isWriting);
        }

        public void Deserialize(Stream inputStream)
        {
            id = DeserializeString(inputStream);
            isWriting = DeserializeBool(inputStream);
        }

        public static void Deserialize(Stream inputStream, out WritingStatus message)
        {
            message = new WritingStatus();
            message.Deserialize(inputStream);
        }
    }
}