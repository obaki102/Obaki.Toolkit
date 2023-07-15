using System.Text.RegularExpressions;
namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor.Extensions;
public static partial class AssetIdFormatterExtension
{
    public static string TrimScrippsAssetId(this string input)
    {
       return  ScrippsRegexPattern().Replace(input, match =>
        {
            string trimmedValue = match.Value.Substring(11, 8);
            if (trimmedValue.StartsWith("0"))
            {
                trimmedValue = trimmedValue[1..];
            }
            return trimmedValue;
        });
    }

    public static string TrimDiscoveryAssetId(this string input)
        => DiscoveryRegexPattern().Replace(input, match => match.Value.Substring(5, 14));

    public static string TrimAeAssetId(this string input)
       => AeRegexPattern().Replace(input, match => match.Value.Substring(9, 6));

    [GeneratedRegex("H[A-Z0-9]{19}")]
    private static partial Regex ScrippsRegexPattern();

    [GeneratedRegex("(TLC|IDD?|OWN|DSC|APL?|SCI|DAM|DES|DSF|AHC|DFC|DLF)[A-Z0-9]{17}")]
    private static partial Regex DiscoveryRegexPattern();

    [GeneratedRegex("[A-Z]{3}_[A-Z]{4}_\\d{6}")]
    private static partial Regex AeRegexPattern();
}