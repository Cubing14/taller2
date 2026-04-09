using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutosCliente.Services
{
    public class LocalDateTimeConverter : JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-ddTHH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (string.IsNullOrEmpty(str)) return DateTime.MinValue;
            // Java puede devolver array [2025,7,14,15,30,0] o string ISO
            return DateTime.TryParse(str, out var dt) ? dt : DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
