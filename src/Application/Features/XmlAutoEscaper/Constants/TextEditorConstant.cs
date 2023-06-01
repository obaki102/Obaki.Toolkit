namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
public static class TextEditorConstants
{
    public const string XmlRegexPattern =@"(?<=<(\w+)>)(?<value>.*?)(?=</\1>)|(?<=\s*=\s*['""])(?<value>.*?)(?=(?:['""]\s*/>|['""]\s*\w+\s*=\s*))";
}