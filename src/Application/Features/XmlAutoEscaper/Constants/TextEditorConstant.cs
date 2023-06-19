namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
public static class TextEditorConstants
{
    //The regex captures content between XML tags and attribute values within XML tags.
    public const string XmlRegexPattern = @"(?<=<(\w+)>)(?<value>.*?)(?=</\1>)|(?<=\s*=\s*['""])(?<value>.*?)(?=(?:['""]\s*/>|['""]\s*\w+\s*=\s*))";
}