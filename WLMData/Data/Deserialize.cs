using System.Text.Json;

namespace WLMData.Data
{
    public static class Deserialize
    {
        public static T FromBytes<T>(this byte[] inputSource)
        {
            return JsonSerializer.Deserialize<T>(inputSource);
        }
    }
}
