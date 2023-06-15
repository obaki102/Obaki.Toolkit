namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
public static class TextEditorConstants
{
    //The regex captures content between XML tags and attribute values within XML tags.
    public const string XmlRegexPattern = @"(?<=<(\w+)>)(?<value>.*?)(?=</\1>)|(?<=\s*=\s*['""])(?<value>.*?)(?=(?:['""]\s*/>|['""]\s*\w+\s*=\s*))";
    public const string ScrippsRegexPattern = @"H[A-Z0-9]{19}";
    public const string DiscoveryRegexPattern = @"(TLC|IDD?|OWN|DSC|APL?|SCI|DAM|DES|DSF|AHC|DFC|DLF)[A-Z0-9]{17}";
    public const string AeRegexPattern = @"[A-Z]{3}_[A-Z]{4}_\d{6}";
}