using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NasaBrowser.Api.Converters;

public class StringDateTimeConverter : JsonConverter<DateTime>
{
    // Формат даты (например, "yyyy-MM-dd" или "dd/MM/yyyy")
    private const string DateFormat = "yyyy-MM-dd";

    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        // Проверяем тип токена (строка или число)
        if (reader.TokenType == JsonTokenType.String)
        {
            string? dateString = reader.GetString();
            if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new JsonException($"Неверный формат даты. Ожидается: {DateFormat}.");
            }
        }

        throw new JsonException($"Ожидалась строка, получен {reader.TokenType}.");
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options)
    {
        // Сериализация в строку в указанном формате
        writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
    }
}