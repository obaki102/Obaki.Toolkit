namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
public interface ITextEditor<T>
{
        void SetValue(T state);
        void RevertValue();
        void ReplayValue();
        T GetCurrentValue();
        T GetPreviousText();
        void Destroy();
        bool IsUndoStackEmpty();
        bool IsRedoStackEmpty();
}