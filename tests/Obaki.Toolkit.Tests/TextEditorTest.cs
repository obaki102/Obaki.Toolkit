
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
namespace Obaki.Toolkit.Test;

public class TextEditorTest : IDisposable
{
    private readonly TextEditor<int> _textEditor;

    public TextEditorTest()
    {
        _textEditor = new TextEditor<int>();
    }

    public void Dispose()
    {
        _textEditor.Dispose();
    }

    [Fact]
    public void GetValue_ShouldReturnCurrentValue()
    {
        // Arrange
        _textEditor.SetValue(42);

        // Act
        var result = _textEditor.GetValue();

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void IsReplayStackEmpty_WhenReplayStackIsEmpty_ShouldReturnTrue()
    {
        // Act
        var result = _textEditor.IsReplayStackEmpty();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsRevertStackEmpty_WhenRevertStackIsEmpty_ShouldReturnTrue()
    {
        // Act
        var result = _textEditor.IsRevertStackEmpty();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void SetValue_ShouldSetNewValue()
    {
        // Act
        _textEditor.SetValue(42);

        // Assert
        Assert.Equal(42, _textEditor.GetValue());
    }

    [Fact]
    public void ReplayValue_ShouldSetCurrentValueToReplayValue()
    {
        // Arrange
        _textEditor.SetValue(100);
        _textEditor.SetValue(42);
        _textEditor.RevertValue();

        // Act
        _textEditor.ReplayValue();

        // Assert
        Assert.Equal(42, _textEditor.GetValue());
    }

    [Fact]
    public void RevertValue_ShouldSetCurrentValueToPreviousRevertValue()
    {
        // Arrange
        _textEditor.SetValue(42);
        _textEditor.SetValue(24);
        _textEditor.SetValue(100);

        // Act
        _textEditor.RevertValue();

        // Assert
        Assert.Equal(24, _textEditor.GetValue());
    }
}

