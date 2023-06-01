
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
namespace Obaki.Toolkit.Test;
public class MementoStoreTest
{

    [Fact]
    public void IsRevertStackEmpty_WhenRevertStackIsEmpty_ShouldReturnTrue()
    {
        // Arrange
        var textEditor = new TextEditor<string>();
        var mementoStore = new MementoStore<string>();

        // Act
        var isRevertStackEmpty = mementoStore.IsRevertStackEmpty(textEditor);

        // Assert
        Assert.True(isRevertStackEmpty);
    }

    [Fact]
    public void IsRevertStackEmpty_WhenRevertStackIsNotEmpty_ShouldReturnFalse()
    {
        // Arrange
        var textEditor = new TextEditor<string>();
        var mementoStore = new MementoStore<string>();
        var memento = new Memento<string>(string.Empty);

        mementoStore.SetRevertValue(textEditor, memento);

        // Act
        var isRevertStackEmpty = mementoStore.IsRevertStackEmpty(textEditor);

        // Assert
        Assert.False(isRevertStackEmpty);
    }

    [Fact]
    public void IsReplayStackEmpty_WhenReplayStackIsEmpty_ShouldReturnTrue()
    {
        // Arrange
        var textEditor = new TextEditor<string>();
        var mementoStore = new MementoStore<string>();

        // Act
        var isReplayStackEmpty = mementoStore.IsReplayStackEmpty(textEditor);

        // Assert
        Assert.True(isReplayStackEmpty);
    }

    [Fact]
    public void IsReplayStackEmpty_WhenReplayStackIsNotEmpty_ShouldReturnFalse()
    {
        // Arrange
        var textEditor = new TextEditor<string>();
        var mementoStore = new MementoStore<string>();
        var memento = new Memento<string>(string.Empty);

        mementoStore.SetReplayValue(textEditor, memento);

        // Act
        var isReplayStackEmpty = mementoStore.IsReplayStackEmpty(textEditor);

        // Assert
        Assert.False(isReplayStackEmpty);
    }

    [Fact]
    public void PeekReplayValue_WhenReplayStackIsEmpty_ShouldThrowException()
    {
        // Arrange
        var textEditor = new TextEditor<string>();
        var mementoStore = new MementoStore<string>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => mementoStore.PeekReplayValue(textEditor));
    }

    [Fact]
    public void PeekReplayValue_WhenReplayStackIsNotEmpty_ShouldReturnTopValue()
    {
        // Arrange
        var textEditor = new TextEditor<string>();
        var mementoStore = new MementoStore<string>();
        var memento1 = new Memento<string>("State 1");
        var memento2 = new Memento<string>("State 2");

        mementoStore.SetReplayValue(textEditor, memento1);
        mementoStore.SetReplayValue(textEditor, memento2);

        // Act
        var peekedValue = mementoStore.PeekReplayValue(textEditor);

        // Assert
        Assert.Equal(memento2, peekedValue);
    }

    [Fact]
    public void PeekRevertValue_WhenRevertStackIsEmpty_ShouldThrowException()
    {
        // Arrange
        var textEditor = new TextEditor<string>();
        var mementoStore = new MementoStore<string>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => mementoStore.PeekRevertValue(textEditor));
    }

    [Fact]
    public void PeekReplayValue_WhenReplayStackIsNotEmpty_ShouldReturnTopMemento()
    {
        // Arrange
        var mementoStore = new MementoStore<int>();
        var textEditor = new TextEditor<int>();
        var memento1 = new Memento<int>(1);
        var memento2 = new Memento<int>(2);

        mementoStore.SetReplayValue(textEditor, memento1);
        mementoStore.SetReplayValue(textEditor, memento2);

        // Act
        var result = mementoStore.PeekReplayValue(textEditor);

        // Assert
        Assert.Equal(memento2, result);
    }

    [Fact]
    public void PeekRevertValue_WhenRevertStackIsNotEmpty_ShouldReturnTopMemento()
    {
        // Arrange
        var mementoStore = new MementoStore<int>();
        var textEditor = new TextEditor<int>();
        var memento1 = new Memento<int>(1);
        var memento2 = new Memento<int>(2);

        mementoStore.SetRevertValue(textEditor, memento1);
        mementoStore.SetRevertValue(textEditor, memento2);

        // Act
        var result = mementoStore.PeekRevertValue(textEditor);

        // Assert
        Assert.Equal(memento2, result);
    }

    [Fact]
    public void ReplayValue_WhenReplayStackIsNotEmpty_ShouldReturnAndRemoveTopMemento()
    {
        // Arrange
        var mementoStore = new MementoStore<int>();
        var textEditor = new TextEditor<int>();
        var memento1 = new Memento<int>(1);
        var memento2 = new Memento<int>(2);

        mementoStore.SetReplayValue(textEditor, memento1);
        mementoStore.SetReplayValue(textEditor, memento2);

        // Act
        var result = mementoStore.ReplayValue(textEditor);

        // Assert
        Assert.Equal(memento2, result);
        Assert.Equal(memento1, mementoStore.PeekReplayValue(textEditor));
    }

    [Fact]
    public void RevertValue_WhenRevertStackIsNotEmpty_ShouldReturnAndRemoveTopMemento()
    {
        // Arrange
        var mementoStore = new MementoStore<int>();
        var textEditor = new TextEditor<int>();
        var memento1 = new Memento<int>(1);
        var memento2 = new Memento<int>(2);

        mementoStore.SetRevertValue(textEditor, memento1);
        mementoStore.SetRevertValue(textEditor, memento2);

        // Act
        var result = mementoStore.RevertValue(textEditor);

        // Assert
        Assert.Equal(memento2, result);
        Assert.Equal(memento1, mementoStore.PeekRevertValue(textEditor));
    }

    [Fact]
    public void Destroy_WhenTextEditorExists_ShouldClearRevertStackAndReplayStack()
    {
        // Arrange
        var mementoStore = new MementoStore<int>();
        var textEditor = new TextEditor<int>();
        var memento1 = new Memento<int>(1);
        var memento2 = new Memento<int>(2);

        mementoStore.SetRevertValue(textEditor, memento1);
        mementoStore.SetReplayValue(textEditor, memento2);

        // Act
        mementoStore.Destroy(textEditor);

        // Assert
        Assert.True(mementoStore.IsRevertStackEmpty(textEditor));
        Assert.True(mementoStore.IsReplayStackEmpty(textEditor));
    }

    [Fact]
    public void Destroy_WhenTextEditorDoesNotExist_ShouldNotThrowException()
    {
        // Arrange
        var mementoStore = new MementoStore<int>();
        var textEditor = new TextEditor<int>();

        // Act 
        var exception = Record.Exception(() => mementoStore.Destroy(textEditor));

        //Assert
        Assert.Null(exception);
    }


}

