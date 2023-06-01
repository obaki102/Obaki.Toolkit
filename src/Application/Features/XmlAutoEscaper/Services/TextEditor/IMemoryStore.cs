namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

public interface IMemoryStore<T>
{
    void SetRevertValue(ITextEditor<T> textEditor, IStateKeeper<T> stateKeeper);
    void SetReplayValue(ITextEditor<T> textEditor, IStateKeeper<T> stateKeeper);
    IStateKeeper<T> RevertValue(ITextEditor<T> textEditor);
    IStateKeeper<T> ReplayValue(ITextEditor<T> textEditor);
    IStateKeeper<T> PeekRevertValue(ITextEditor<T> textEditor);
    IStateKeeper<T> PeekReplayValue(ITextEditor<T> textEditor);
    void Destroy(ITextEditor<T> textEditor);
    bool IsRevertStackEmpty(ITextEditor<T> textEditor);
    bool IsReplayStackEmpty(ITextEditor<T> textEditor);
}