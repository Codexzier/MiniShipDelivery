using System.Collections.Generic;
using System.Threading.Tasks;
using LLama;
using LLama.Common;
using LLama.Sampling;

namespace MiniShipDelivery.Components.Dialog.Chatbots;

public class ChatbotLlama
{
    private readonly string _assistantName;
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
            MaxTokens = 40, // maximum number of tokens to generate
            AntiPrompts = new List<string> { "User:" }, // avoid generating prompts that start with "Bob:"
        };
    }
    
    public void LoadChatHistory(string systemRoleDescription = "")
    {
        this._chatHistory = new ChatHistory();
        this._chatHistory.AddMessage(AuthorRole.System, systemRoleDescription);
            
        this._session = new ChatSession(this._executor, this._chatHistory);
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
    
    public delegate void ChatAnswerEventHandler(string message);
    public event ChatAnswerEventHandler ChatAnswerEvent;

    public void Dispose()
    {
        this._model.Dispose();
        this._context.Dispose();
    }
}