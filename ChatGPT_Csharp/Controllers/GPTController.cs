using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Completions;
using System.Data;

namespace ChatGPT_Csharp.Controllers
{
    [ApiController]
    public class GPTController : ControllerBase
    {
        [HttpGet]
        [Route("UseChatGPT")]
        public async Task<IActionResult> UseChatGPT(string query)
        {
            string OutPutResult = "";
            var openai = new OpenAIAPI("api-key");

            List<ChatMessage> messages = new List<ChatMessage>();
            messages.Add( new ChatMessage{ Role = ChatMessageRole.User, Content = query});


            ChatRequest chatRequest = new ChatRequest();
            chatRequest.Messages= messages;
            chatRequest.Model = OpenAI_API.Models.Model.ChatGPTTurbo;

            var completions = openai.Chat.CreateChatCompletionAsync(chatRequest);

            foreach (var completion in completions.Result.Choices)
            {
                OutPutResult += completion.Message.TextContent; 
            }

            return Ok(OutPutResult);
        }
    }
}
