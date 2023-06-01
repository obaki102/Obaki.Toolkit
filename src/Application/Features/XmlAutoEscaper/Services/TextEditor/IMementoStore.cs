namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

public interface IMementoStore<T>
{
    void SetRevertValue(ITextEditor<T> textEditor, IMemento<T> memento);
    void SetReplayValue(ITextEditor<T> textEditor, IMemento<T> memento);
    IMemento<T> RevertValue(ITextEditor<T> textEditor);
    IMemento<T> ReplayValue(ITextEditor<T> textEditor);
    IMemento<T> PeekRevertValue(ITextEditor<T> textEditor);
    IMemento<T> PeekReplayValue(ITextEditor<T> textEditor);
    void Destroy(ITextEditor<T> textEditor);
    bool IsRevertStackEmpty(ITextEditor<T> textEditor);
    bool IsReplayStackEmpty(ITextEditor<T> textEditor);
}