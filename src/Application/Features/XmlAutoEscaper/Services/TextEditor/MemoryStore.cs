namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

internal sealed class MemoryStore<T> : IMemoryStore<T>
{
    private readonly Dictionary<ITextEditor<T>, Stack<IMemory<T>>> _revertMemoryStore =
      new Dictionary<ITextEditor<T>, Stack<IMemory<T>>>();
    private readonly Dictionary<ITextEditor<T>, Stack<IMemory<T>>> _replayMemoryStore =
        new Dictionary<ITextEditor<T>, Stack<IMemory<T>>>();
    public bool IsRevertStackEmpty(ITextEditor<T> textEditor)
    {
        if (!_revertMemoryStore.TryGetValue(textEditor, out var revertStack))
        {
            return true;
        }

        return revertStack.Count == 0;
    }

    public bool IsReplayStackEmpty(ITextEditor<T> textEditor)
    {
        if (!_replayMemoryStore.TryGetValue(textEditor, out var replayStack))
        {
            return true;
        }

        return replayStack.Count == 0;
    }

    public IMemory<T> PeekReplayValue(ITextEditor<T> textEditor)
    {
        if (!_replayMemoryStore.TryGetValue(textEditor, out var replayStack))
        {
            throw new InvalidOperationException("No states found for the textEditor");
        }

        return replayStack.Peek();
    }

    public IMemory<T> PeekRevertValue(ITextEditor<T> textEditor)
    {
        if (!_revertMemoryStore.TryGetValue(textEditor, out var reverStack))
        {
            throw new InvalidOperationException("No states found for the textEditor");
        }

        return reverStack.Peek();
    }

    public IMemory<T> ReplayValue(ITextEditor<T> textEditor)
    {
        if (!_replayMemoryStore.TryGetValue(textEditor, out var replayStack))
        {
            throw new InvalidOperationException("No replay state keeper found for the textEditor");
        }

        if (replayStack.Count == 0)
        {
            _replayMemoryStore.Remove(textEditor);
            throw new InvalidOperationException("Stack is empty");
        }

        return replayStack.Pop();
    }

    public IMemory<T> RevertValue(ITextEditor<T> textEditor)
    {
        if (!_revertMemoryStore.TryGetValue(textEditor, out var revertStack))
        {
            throw new InvalidOperationException("No revert state keeper found for the textEditor");
        }

        if (revertStack.Count == 0)
        {
            _revertMemoryStore.Remove(textEditor);
            throw new InvalidOperationException("Stack is empty");
        }

        return revertStack.Pop();
    }

    public void SetReplayValue(ITextEditor<T> textEditor, IMemory<T> memory)
    {
        if (!_replayMemoryStore.TryGetValue(textEditor, out var replayStack))
        {
            replayStack = new Stack<IMemory<T>>();
            _replayMemoryStore.Add(textEditor, replayStack);
        }

        replayStack.Push(memory);
    }

    public void SetRevertValue(ITextEditor<T> textEditor, IMemory<T> memory)
    {
        if (!_revertMemoryStore.TryGetValue(textEditor, out var revertStack))
        {
            revertStack = new Stack<IMemory<T>>();
            _replayMemoryStore.Add(textEditor, revertStack);
        }

        revertStack.Push(memory);
    }

    public void Destroy(ITextEditor<T> textEditor)
    {
         if (_revertMemoryStore.ContainsKey(textEditor))
            {
                _revertMemoryStore[textEditor].Clear();
                _revertMemoryStore.Remove(textEditor);
            }

            if (_replayMemoryStore.ContainsKey(textEditor))
            {
                _replayMemoryStore[textEditor].Clear();
                _replayMemoryStore.Remove(textEditor);
            }
    }
}