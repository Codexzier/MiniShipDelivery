using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LLama;
using LLama.Common;
using LLama.Sampling;

namespace MiniShipDelivery.Components.Dialog.Chatbots;

public class ChatbotLlama
{
    private readonly string _assistantName;
    private readonly string _fileHistorie = $"{Environment.CurrentDirectory}/chatHistory";
    
    private readonly InteractiveExecutor _executor;
    private ChatHistory _chatHistory;
    private readonly InferenceParams _inferenceParams;
    private ChatSession _session;
    private readonly LLamaWeights _model;
    private readonly LLamaContext _context;
    
    public ChatbotLlama(string modelPath, string assistantName)
    {
        this._assistantName = assistantName;
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 1024, // The longest length of chat as memory.
            GpuLayerCount = 5, // How many layers to offload to GPU. Please adjust it according to your GPU memory. 
        };
        
        this._model = LLamaWeights.LoadFromFile(parameters);
        this._context = this._model.CreateContext(parameters);
        this._executor = new InteractiveExecutor(this._context);
        
        this._inferenceParams = new InferenceParams
        {
            SamplingPipeline = new DefaultSamplingPipeline(), // use default sampling pipeline
            MaxTokens = 50, // maximum number of tokens to generate
            AntiPrompts = new List<string> { "User:" }, // avoid generating prompts that start with "Bob:"
        };
    }
    
    private string GetFileName()
    {
        return $"{this._fileHistorie}_{this._assistantName}.json";
    }
    
    public List<ChatItem> LoadChatHistory(bool loadFromFile = true, string systemRoleDescription = "")
    {
        // if (loadFromFile && File.Exists(this.GetFileName()))
        // {
        //     var chatHistory = File.ReadAllText(this.GetFileName());
        //     this._chatHistory = ChatHistory.FromJson(chatHistory) ?? new ChatHistory();
        // }
        // else
        // {
            this._chatHistory = new ChatHistory();
            this._chatHistory.AddMessage(AuthorRole.System, systemRoleDescription);
        //}
            
        this._session = new ChatSession(this._executor, this._chatHistory); 
        
        return this._chatHistory.Messages.Select(s => new ChatItem(s.AuthorRole.ToString(), s.Content)).ToList();
    }
    
    public async Task Chat(string userInput)
    {
        await foreach (var text in this._session
                           .ChatAsync(new ChatHistory
                               .Message(AuthorRole.User, userInput), 
                               this._inferenceParams))
        {
            this.ChatAnswerEvent?.Invoke(text);
        }
    }
    
    public string GetLastAnswer()
    {
        var lastMessage = this._chatHistory.Messages.LastOrDefault();
        return lastMessage?.Content ?? "";
    }
    
    public delegate void ChatAnswerEventHandler(string message);
    public event ChatAnswerEventHandler ChatAnswerEvent;

    public void Dispose()
    {
        this._model.Dispose();
        this._context.Dispose();
    }

    public void SaveChatHistory()
    {
        File.WriteAllText(this.GetFileName(), this._chatHistory.ToJson());
    }
}

public class ChatItem(string role, string message)
{
    public string Role { get; } = role;
    public string Message { get; } = message;
}