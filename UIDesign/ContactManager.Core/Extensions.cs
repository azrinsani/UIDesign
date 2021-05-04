using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using ProtoBuf;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Buffers;

namespace ContactManager.Core
{
    public static class Extensions
    {      
        public static byte[] SerializeToProtoBufBytes<T>(this T obj)
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, obj);
            return stream.GetBuffer();
        }
        public static string SerializeToProtoBufFile<T>(this T obj, string fn)
        {
            using var file = File.Create(fn);
            Serializer.Serialize<T>(file, obj);
            return fn;
        }
        public static T DeserializeFromProtoBufFile<T>(this string fn)
        {
            using var file = File.OpenRead(fn);
            return Serializer.Deserialize<T>(file);
        }
    }

}
