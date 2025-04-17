using System.Text.Json;
using System.Text.Json.Serialization;

namespace NasaBrowser.Api.Converters;

public sealed class StringToIntConverter : JsonConverter<int>
{
    public override int Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        // Проверяем тип токена
        if (reader.TokenType == JsonTokenType.String)
        {
            // Пытаемся преобразовать строку в int
            if (int.TryParse(reader.GetString(), out int result))
            {
                return result;
            }
            else
            {
                throw new JsonException($"Не удалось преобразовать строку '{reader.GetString()}' в int.");
            }
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            // Если значение уже число, возвращаем его
            return reader.GetInt32();
        }

        throw new JsonException($"Ожидалась строка или число, получен {reader.TokenType}.");
    }

    public override void Write(
        Utf8JsonWriter writer,
        int value,
        JsonSerializerOptions options)
    {
        // Записываем значение как число (можно изменить на строку, если нужно)
        writer.WriteNumberValue(value);
    }
}