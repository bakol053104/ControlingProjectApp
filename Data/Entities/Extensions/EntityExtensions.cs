using System.Text.Json;

namespace ControlingProjectApp.Data.Entities.Extensions;

public static class EntityExtensions
{
    public static T? Copy<T>(this T itemCopy) where T : IEntity
    {
        var json = JsonSerializer.Serialize(itemCopy);
        return JsonSerializer.Deserialize<T>(json);
    }
}
