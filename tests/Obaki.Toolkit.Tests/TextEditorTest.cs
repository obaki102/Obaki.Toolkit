
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

    [Fact]
    public void Dispose_ShouldClearRevertAndReplayStacks()
    {
        // Arrange
        _textEditor.SetValue(42);
        _textEditor.SetValue(42);
        _textEditor.RevertValue();
        _textEditor.ReplayValue();

        // Act
        _textEditor.Dispose();

        // Assert
        Assert.True(_textEditor.IsRevertStackEmpty());
        Assert.True(_textEditor.IsReplayStackEmpty());
    }

    [Fact]
    public void RevertValue_WhenRevertStackIsEmpty_ShouldThrowException()
    {
        // Assert
        Assert.Throws<InvalidOperationException>(() => _textEditor.RevertValue());
    }

    [Fact]
    public void ReplayValue_WhenReplayStackIsEmpty_ShouldThrowException()
    {
        // Assert
        Assert.Throws<InvalidOperationException>(() => _textEditor.ReplayValue());
    }


    [Fact]
    public void GetValue_WhenDisposed_ShouldThrowException()
    {
        // Arrange
        _textEditor.Dispose();

        // Assert
        Assert.Throws<System.InvalidOperationException>(() => _textEditor.ReplayValue());
    }

    [Theory]
    [InlineData("<tag Value=\" &gt;\"/>", "<tag Value=\" >\"/>")]
    [InlineData("<tag Value=\" &lt;\"/>", "<tag Value=\" <\"/>")]
    [InlineData("<root value=\"&quot; &amp; &amp; &amp; &amp; &amp; \"/>", "<root value=\"\" & & & & & \"/>")]
    [InlineData("<root value=\"&quot; &amp;      &amp; &amp; &amp;      &amp; \"/>", "<root value=\"\" &      & & &      & \"/>")]
    [InlineData("<tag>&quot;&apos; &amp; &amp; &amp; &apos; &lt; &gt; &lt;&gt; </tag>", "<tag>\"' & & & ' < > <> </tag>")]
    [InlineData("<AMS Value  = \"Glaser&quot;-Focused HD\" Verb=\"\" Asset_ID = \"test  &quot;  \" Asset_Name=\"test_&quot;Glaser&quot;-FocusedT\" Creation_Date=\"2022-02-10\" Description=\"&quot;Glaser&quot;-Focused--title--\" Product=\"MOD\" Provider=\"HD\" Provider_ID=\".com\" Name=\"Title_Brief\" Value=\"&quot;Glaser&quot;-Focused HD\"  />", "<AMS Value  = \"Glaser\"-Focused HD\" Verb=\"\" Asset_ID = \"test  \"  \" Asset_Name=\"test_\"Glaser\"-FocusedT\" Creation_Date=\"2022-02-10\" Description=\"\"Glaser\"-Focused--title--\" Product=\"MOD\" Provider=\"HD\" Provider_ID=\".com\" Name=\"Title_Brief\" Value=\"\"Glaser\"-Focused HD\"  />")]
    public void EscapeXmlString_ValidInput_ShouldEscapeSpecialCharacters(string expected, string input)
    {
        //Arrange
        string test = input;

        //Act
        var result = _textEditor.EscapeXmlString(test);

        //Assert
        Assert.Equal(expected, result);

    }
}

