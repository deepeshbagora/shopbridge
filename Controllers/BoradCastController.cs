using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge.Data;
using Shopbridge.Domain.Models;
using Shopbridge.Domain.Models.Inventory;
using Shopbridge.Domain.Services.Interfaces;

namespace Shopbridge.Controllers;


public class WebSocketController : ControllerBase,IBroadCast
{

    private static List<WebSocket> wsObject;

    public WebSocketController()
    {
        if(wsObject == null)
        wsObject = new List<WebSocket>();
    }

    [HttpGet("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            wsObject.Add(webSocket);
            await Echo(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    private static async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);
        
        while (!receiveResult.CloseStatus.HasValue)
        {


            await webSocket.SendAsync(
            new ArraySegment<byte>(buffer, 0, receiveResult.Count),
            receiveResult.MessageType,
            receiveResult.EndOfMessage,
            CancellationToken.None);



            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }

    [HttpPost("BroadCast")]
    public void Broadcast([FromBodyAttribute]BroadCastModel Data){

        Parallel.ForEach(wsObject,async ws => {
           
            byte[] bytes = Encoding.ASCII.GetBytes(System.Text.Json.JsonSerializer.Serialize(Data, typeof (BroadCastModel)));
            await ws.SendAsync(bytes,WebSocketMessageType.Text,WebSocketMessageFlags.EndOfMessage,CancellationToken.None);
        });
    }
}