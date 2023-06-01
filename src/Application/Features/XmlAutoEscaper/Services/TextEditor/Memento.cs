namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

internal sealed class Memento<T> : IMemento<T>
{
    public T State { get; }

    public Memento(T state)
    {
        State = state;
    }
}