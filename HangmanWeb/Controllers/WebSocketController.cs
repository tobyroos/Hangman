using HangmanWeb.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangmanWeb.Controllers
{
    #region snippet
    public class WebSocketsController : ControllerBase
    {
        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await
                                   HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
        #endregion

        private async Task Echo(HttpContext httpContext, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            HangmanGame game = new HangmanGame();

            char[] guessLetters;
            int lives;
            game.SetWord();
            

            while (!result.CloseStatus.HasValue)
            {
                
                //string str = System.Text.Encoding.Default.GetString(buffer);
                //str = str.Trim('\0');

                char g = (char)buffer[0];

                char[] userWord = game.CheckLetter(g.ToString()); //try without null character "\0" first. if error add null to end of 
                lives = game.GetLives();

                Console.WriteLine(userWord);

                for (int i=0; i<userWord.Length; i++)
                {
                    buffer[i] = (byte)userWord[i];
                }

                char[] gameWinMsg = { 'Y', 'O', 'U', ' ', 'W', 'I', 'N' };

                if (game.CheckResult())
                {
                    Array.Clear(buffer, 0, buffer.Length);
                    for (int i = 0; i < gameWinMsg.Length; i++)
                    {
                        buffer[i] = (byte)gameWinMsg[i];
                    }
                    SessionVar.Score = game.GetLives();
                }

                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
               
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            Response.Redirect("https://localhost:7249/Scores");
        }
    }
}