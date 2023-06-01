
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
namespace Obaki.Toolkit.Test;
public class MementoTest
{
    [Fact]
    public void Memento_ObjectsWithSameState_AreEqual()
    {
        // Arrange
        var state = "Hello";
        var memory1 = new Memento<string>(state);
        var memory2 = new Memento<string>(state);

        // Assert
        Assert.Equal(memory1.State, memory2.State);
    }

    [Fact]
    public void Memento_ObjectsWithDifferentState_AreNotEqual()
    {
        // Arrange
        var memory1 = new Memento<string>("Hello");
        var memory2 = new Memento<string>("World");

        // Assert
        Assert.NotEqual(memory1.State, memory2.State);
    }
}