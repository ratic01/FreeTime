using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Agencija
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parse ISO 8601 format
            return DateTime.Parse(reader.GetString(), null, DateTimeStyles.RoundtripKind);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Formatiraj kao ISO 8601
            writer.WriteStringValue(value.ToString("o", CultureInfo.InvariantCulture)); // "o" je za round-trip
        }
    }
}
