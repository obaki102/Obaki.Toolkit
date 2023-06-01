namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

internal sealed class Memory<T> : IMemory<T>
{
    public T State { get; }

    public Memory(T state)
    {
        State = state;
    }
}