using NetworkCommsDotNet.DPSBase;
using ProtoBuf;
using System.IO;

namespace WLMData.Data.Packets
{
    [ProtoContract]
    public class LoginRequest : Serializer, IExplicitlySerialize
    {
        [ProtoMember(1)]
        public string username { get; private set; }

        [ProtoMember(2)]
        public string password { get; private set; }

        [ProtoMember(3)]
        public int status { get; private set; }

        [ProtoMember(4)]
        public string version { get; private set; }

        public LoginRequest() { }

        public LoginRequest(string username, string password, int status, string version)
        {
            this.username = username;
            this.password = password;
            this.status = status;
            this.version = version;
        }

        public void Serialize(Stream outputStream)
        {
            SerializeData(outputStream, username);
            SerializeData(outputStream, password);
            SerializeData(outputStream, status);
            SerializeData(outputStream, version);
        }

        public void Deserialize(Stream inputStream)
        {
            username = DeserializeString(inputStream);
            password = DeserializeString(inputStream);
            status = DeserializeInt(inputStream);
            version = DeserializeString(inputStream);
        }

        public static void Deserialize(Stream inputStream, out LoginRequest loginRequest)
        {
            loginRequest = new LoginRequest();
            loginRequest.Deserialize(inputStream);
        }
    }
}
