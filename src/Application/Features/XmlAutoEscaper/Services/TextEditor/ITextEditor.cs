namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
public interface ITextEditor<T>
{
        void SetValue(T state);
        void RevertValue();
        void ReplayValue();
        T? GetValue();
        bool IsRevertStackEmpty();
        bool IsReplayStackEmpty();
        string EscapeXmlString(string input);
}