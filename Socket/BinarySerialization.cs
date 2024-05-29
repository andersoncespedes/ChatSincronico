using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
namespace Sockets;
public class BinarySerialization
{
    public static byte[] Serializate(object toSerialize)
    {
        return JsonSerializer.SerializeToUtf8Bytes(toSerialize);
    }
    public static object Derializate(byte[] data)
    {
        DataContractSerializer serializer = new DataContractSerializer(typeof(User));
        using (MemoryStream stream = new MemoryStream(data))
        {
            User persona = (User)serializer.ReadObject(stream);
            return persona;
        }
    }
}