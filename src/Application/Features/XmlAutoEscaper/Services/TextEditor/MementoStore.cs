namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

internal sealed class MementoStore<T> : IMementoStore<T>
{
    private readonly Dictionary<ITextEditor<T>, Stack<IMemento<T>>> _revertMementoStore;
    private readonly Dictionary<ITextEditor<T>, Stack<IMemento<T>>> _replayMementoStore;
    public MementoStore()
    {
        _revertMementoStore = new Dictionary<ITextEditor<T>, Stack<IMemento<T>>>();
        _replayMementoStore =  new Dictionary<ITextEditor<T>, Stack<IMemento<T>>>();
    }

    public bool IsRevertStackEmpty(ITextEditor<T> textEditor)
    {
        if (!_revertMementoStore.TryGetValue(textEditor, out var revertStack))
        {
            return true;
        }

        return revertStack.Count == 0;
    }

    public bool IsReplayStackEmpty(ITextEditor<T> textEditor)
    {
        if (!_replayMementoStore.TryGetValue(textEditor, out var replayStack))
        {
            return true;
        }

        return replayStack.Count == 0;
    }

    public IMemento<T> PeekReplayValue(ITextEditor<T> textEditor)
    {
        if (!_replayMementoStore.TryGetValue(textEditor, out var replayStack))
        {
            throw new InvalidOperationException("No states found for the textEditor");
        }

        return replayStack.Peek();
    }

    public IMemento<T> PeekRevertValue(ITextEditor<T> textEditor)
    {
        if (!_revertMementoStore.TryGetValue(textEditor, out var reverStack))
        {
            throw new InvalidOperationException("No states found for the textEditor");
        }

        return reverStack.Peek();
    }

    public IMemento<T> ReplayValue(ITextEditor<T> textEditor)
    {
        if (!_replayMementoStore.TryGetValue(textEditor, out var replayStack))
        {
            throw new InvalidOperationException("No replay state keeper found for the textEditor");
        }

        if (replayStack.Count == 0)
        {
            _replayMementoStore.Remove(textEditor);
            throw new InvalidOperationException("Stack is empty");
        }

        return replayStack.Pop();
    }

    public IMemento<T> RevertValue(ITextEditor<T> textEditor)
    {
        if (!_revertMementoStore.TryGetValue(textEditor, out var revertStack))
        {
            throw new InvalidOperationException("No revert state keeper found for the textEditor");
        }

        if (revertStack.Count == 0)
        {
            _revertMementoStore.Remove(textEditor);
            throw new InvalidOperationException("Stack is empty");
        }

        return revertStack.Pop();
    }

    public void SetReplayValue(ITextEditor<T> textEditor, IMemento<T> memento)
    {
        if (!_replayMementoStore.TryGetValue(textEditor, out var replayStack))
        {
            replayStack = new Stack<IMemento<T>>();
            _replayMementoStore.Add(textEditor, replayStack);
        }

        replayStack.Push(memento);
    }

    public void SetRevertValue(ITextEditor<T> textEditor, IMemento<T> memento)
    {
        if (!_revertMementoStore.TryGetValue(textEditor, out var revertStack))
        {
            revertStack = new Stack<IMemento<T>>();
            _revertMementoStore.Add(textEditor, revertStack);
        }

        revertStack.Push(memento);
    }

    public void Destroy(ITextEditor<T> textEditor)
    {
        if (_revertMementoStore.ContainsKey(textEditor))
        {
            _revertMementoStore[textEditor].Clear();
            _revertMementoStore.Remove(textEditor);
        }

        if (_replayMementoStore.ContainsKey(textEditor))
        {
            _replayMementoStore[textEditor].Clear();
            _replayMementoStore.Remove(textEditor);
        }
    }
}