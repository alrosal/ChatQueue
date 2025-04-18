﻿using ChatQueue.Mocker;
using ChatQueue.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatQueue.Controllers
{
    [ApiController]
    public class ChatSessionController : ControllerBase
    {
        private readonly IChatQueueService _chatQueueService;

        public ChatSessionController(IChatQueueService chatQueueService)
        {
            _chatQueueService = chatQueueService;
        }

        [HttpPost]
        [Route("api/requestagent")]
        public IActionResult RequestAgent(string sessionId)
        {
            var result = _chatQueueService.AddToQueue(sessionId);

            if (result)
            {
                return Ok(new { Message = $"Chat Session is queued for Session Id: {sessionId}" });
            }

            Console.WriteLine($"Session ID: {sessionId} was not in queue.");

            return BadRequest(new { Message = "Chat Session Queue is full." });
        }

    }
}
