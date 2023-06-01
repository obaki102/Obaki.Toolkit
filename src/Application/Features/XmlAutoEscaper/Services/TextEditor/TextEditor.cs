
using Obaki.XmlSpecialCharacterEscaper;
namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
internal class TextEditor<T> : ITextEditor<T>, IDisposable
{
    private readonly IMementoStore<T> _mementoStore;
    private T? _state;
    internal TextEditor()
    {
        _mementoStore = new MementoStore<T>();

    }
    public T? GetValue() => _state;

    public bool IsReplayStackEmpty() => _mementoStore.IsReplayStackEmpty(this);

    public bool IsRevertStackEmpty() => _mementoStore.IsRevertStackEmpty(this);

    public void SetValue(T state)
    {
        _mementoStore.SetRevertValue(this, new Memento<T>(state));
        _state = state;
    }

    public void ReplayValue()
    {
        var replayMemory = _mementoStore.ReplayValue(this);
        SetValue(replayMemory.State);
    }

    public void RevertValue()
    {
        var currentValue = _mementoStore.RevertValue(this);
        _mementoStore.SetReplayValue(this, currentValue);

        var previousValue = _mementoStore.PeekRevertValue(this);
        SetValue(previousValue.State);
    }

    public string  EscapeXmlString(string xmlInput) => xmlInput.Escape(TextEditorConstants.XmlRegexPattern);
  
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _mementoStore.Destroy(this);
        }
    }
}