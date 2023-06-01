namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

public interface IMemoryStore<T>
{
    void SetRevertValue(ITextEditor<T> textEditor, IMemory<T> stateKeeper);
    void SetReplayValue(ITextEditor<T> textEditor, IMemory<T> stateKeeper);
    IMemory<T> RevertValue(ITextEditor<T> textEditor);
    IMemory<T> ReplayValue(ITextEditor<T> textEditor);
    IMemory<T> PeekRevertValue(ITextEditor<T> textEditor);
    IMemory<T> PeekReplayValue(ITextEditor<T> textEditor);
    void Destroy(ITextEditor<T> textEditor);
    bool IsRevertStackEmpty(ITextEditor<T> textEditor);
    bool IsReplayStackEmpty(ITextEditor<T> textEditor);
}