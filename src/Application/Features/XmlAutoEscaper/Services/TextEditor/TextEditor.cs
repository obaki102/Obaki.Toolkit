namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

internal class TextEditor<T> : ITextEditor<T>, IDisposable
{
    private readonly IMemoryStore<T> _memoryStore;
    public TextEditor()
    {
        _memoryStore = new MemoryStore<T>();

    }
    public T GetValue() => _memoryStore.PeekRevertValue(this).State;

    public bool IsReplayStackEmpty() => _memoryStore.IsReplayStackEmpty(this);

    public bool IsRevertStackEmpty() => _memoryStore.IsRevertStackEmpty(this);

    public void SetValue(T state) => _memoryStore.SetRevertValue(this, new Memory<T>(state));

    public void ReplayValue()
    {
        var replayMemory = _memoryStore.ReplayValue(this);
        SetValue(replayMemory.State);
    }

    public void RevertValue()
    {
        var currentMemory = _memoryStore.RevertValue(this);
        _memoryStore.SetReplayValue(this, currentMemory);

        var previousMemory = _memoryStore.RevertValue(this);
        SetValue(previousMemory.State);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _memoryStore.Destroy(this);
        }
    }
}