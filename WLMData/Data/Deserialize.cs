using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
