using System.Text.Json.Serialization;
namespace Obaki.Toolkit.Application.Features.D4Timer;

public class D4Boss
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public int Time { get; set; }
}