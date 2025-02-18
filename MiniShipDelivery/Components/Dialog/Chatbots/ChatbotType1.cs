using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniShipDelivery.Components.Input;

namespace MiniShipDelivery.Components.Dialog.Chatbots;

public class ChatbotType1
{
    private const string ModelPath = "C:/Users/Codexzier/.lmstudio/models/lmstudio-community/Phi-3.1-mini-128k-instruct-GGUF/Phi-3.1-mini-128k-instruct-Q4_K_M.gguf";
    private readonly ChatbotLlama _chatbotLlama;
    private readonly StringBuilder _aiMessage = new();
    
    public ChatbotType1()
    {
        this._chatbotLlama = new ChatbotLlama(ModelPath, "character1");
        
        var rolePromtDescription = new StringBuilder();
        rolePromtDescription.Append("Transcript of a dialog, where the User interacts with an Assistant named Aouda. ");
        rolePromtDescription.Append("Ihre antworten sind kurz und sind maximal mit Leerzeichen 50 Zeichnen lang. ");
        
        // rolePromtDescription.Append("Promt beschreibung für den Charater1. ");
        // rolePromtDescription.Append("Sie ist Weiblich. ");
        // rolePromtDescription.Append("Ihre Antwortsätze sind kurz und sind maximal mit Leerzeichen 40 Zeichnen lang. ");
        rolePromtDescription.Append("Sie weis viel über verschiedene Bücher und Filme. ");
        
        this._chatbotLlama.LoadChatHistory(true, rolePromtDescription.ToString());
        
        this._chatbotLlama.ChatAnswerEvent += this.ChatbotLlama_ChatAnswerEvent;
    }

    private void ChatbotLlama_ChatAnswerEvent(string message)
    {
        Debug.Write(message);
        // // remove unusable chars from the message
        message = message.Replace("character1: ", "").Split('\r', '\n')[0].ToUpper();
        
        var newMessage = new StringBuilder();
        foreach (var chr in message)
        {
            if(AssetOfLetters.Letters.Values.All(a => a != $"{chr}")) continue;
            
            newMessage.Append(chr);
        }
        
        this._aiMessage.Append(newMessage);
        this.ChatAnswerPartEvent?.Invoke(newMessage.ToString());
    }
    
    public delegate void ChatAnswerPartEventHandler(string message);
    public event ChatAnswerPartEventHandler ChatAnswerPartEvent;
    
    public async Task SetUserInput(string outputTextUser)
    {
        this._aiMessage.Clear();
        await this._chatbotLlama.Chat(outputTextUser);
        
        var message = this._aiMessage
            .ToString()
            .Replace("ASSISTANT: ", "")
            .Replace("ASSISTANT", "")
            .Replace("CHARACTER1: ", "")    
            .Replace("USER:", "")
            .Split('\r', '\n')[0].ToUpper();
        
        if(message.Length > 40)
        {
            message = message.Substring(0, 40);
        }
        
        this.ChatAnswerEvent?.Invoke(message);
    }
    
    public delegate void ChatAnswerEventHandler(string message);
    public event ChatAnswerEventHandler ChatAnswerEvent;
}