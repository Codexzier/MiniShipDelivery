using Microsoft.Xna.Framework.Input;
using MiniShipDelivery.Components.Input;

namespace CodexzierGameEngine.Test;

[TestClass]
public sealed class InputTextControllerTest
{
    [TestMethod]
    public void SetEnterAnLetterToPost_Success()
    {
        // Arrange
        var inputTextController = new InputTextController();
        
        // Act
        // start enter text
        inputTextController.StartTextInput();
        inputTextController.UpdateV2();
        
        // input single letters
        inputTextController.InputText("H");
        inputTextController.UpdateV2();
        
        // Assert
        Assert.AreEqual("H", inputTextController.OutputText);
    }
    
    [TestMethod]
    public void SetEnterAnLetterToPost_NotReleasedSuccess()
    {
        // Arrange
        var inputTextController = new InputTextController();
        
        // Act
        // start enter text
        inputTextController.StartTextInput();
        inputTextController.UpdateV2();
        
        // input single letters
        inputTextController.InputText("H");
        
        // Assert
        Assert.AreEqual("", inputTextController.OutputText);
    }
    
    [TestMethod]
    public void SetEnterAnLetterToPost_DoublePressedMustGetOnlyTheFirst()
    {
        // Arrange
        var inputTextController = new InputTextController();
        
        // Act
        // start enter text
        inputTextController.StartTextInput();
        inputTextController.UpdateV2();
        
        // input single letters
        inputTextController.InputText("H");
        inputTextController.InputText("e");
        inputTextController.UpdateV2();
        
        // Assert
        Assert.AreEqual("H", inputTextController.OutputText);
    }
    
    [TestMethod]
    public void SetEnterAnLetterToPost_SayHello()
    {
        // Arrange
        var inputTextController = new InputTextController();
        
        // Act
        // start enter text
        inputTextController.StartTextInput();
        inputTextController.UpdateV2();
        
        // input single letters
        inputTextController.InputText("H");
        inputTextController.UpdateV2();
        inputTextController.InputText("e");
        inputTextController.UpdateV2();
        inputTextController.InputText("l");
        inputTextController.UpdateV2();
        inputTextController.InputText("l");
        inputTextController.UpdateV2();
        inputTextController.InputText("o");
        inputTextController.UpdateV2();
        
        // Assert
        Assert.AreEqual("Hello", inputTextController.OutputText);
    }
}

[TestClass]
public class KeyboardControllerTest
{
    [TestMethod]
    public void KeyboardHasPressedTheEnterKey()
    {
        // arrange
        var instance = new KeyboardController();
        var keys = new KeyboardState(Keys.H);
        
        
        // act
        instance.Update(new KeyboardState(Keys.Enter));
        
        // assert
    }
}