using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Data;
using PropertyManagement.API.Dtos;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Controllers
{
    [Authorize]
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IPropertyManagementRepository _repo;
        private readonly IMapper _mapper;
        public MessagesController(IPropertyManagementRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            var messageFromRepo = await _repo.GetMessage(id);

            if (messageFromRepo == null)
                return NotFound();

            return Ok(messageFromRepo);
        }

        [HttpGet("users/{userId}/sent")]
        public async Task<IActionResult> GetSentMessages(int userId)
        {
            var messagesFromRepo = await _repo.GetMessagesForUser(true, userId);
            
            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messageThread);
        }

        [HttpGet("users/{userId}/received")]
        public async Task<IActionResult> GetReceivedMessages(int userId)
        {
            var messagesFromRepo = await _repo.GetMessagesForUser(false, userId);

            var joinedPropertiesId = await _repo.GetUserRents(userId);
            
            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messageThread);
        }

        [HttpGet("users/{userId}/unread")]
        public async Task<IActionResult> GetUnreadMessages(int userId)
        {
            var messagesFromRepo = await _repo.GetMessagesForUser(null, userId);

            messagesFromRepo = messagesFromRepo.Where(m => m.IsRead);
            
            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messageThread);
        }

        [HttpGet("thread/{propertyId}")]
        public async Task<IActionResult> GetMessageThread(int propertyId)
        {
            var messagesFromRepo = await _repo.GetMessageThread(propertyId);
            
            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messageThread);
        }

        [HttpPost("{propertyId}")]
        public async Task<IActionResult> CreateMessage(int propertyId, MessageForCreationDto messageForCreationDto)
        {
            messageForCreationDto.PropertyId = propertyId;

            var sender = await _repo.GetUser(messageForCreationDto.SenderId);
            
            if (sender == null)
                return BadRequest("Cound not find sender");

            var property = await _repo.GetProperty(messageForCreationDto.PropertyId);

            if (property == null)
                return BadRequest("Cound not find property");

            var message = _mapper.Map<Message>(messageForCreationDto);
            message.Sender = sender;

            _repo.Add(message);

            if (await _repo.SaveAll())
            {
                var messageToRetun = _mapper.Map<MessageToReturnDto>(message);
                return CreatedAtRoute("GetMessage", new {propertyId, id = message.Id}, messageToRetun);
            }

            throw new Exception("Creation the message failed on save");
        }

        [HttpPost("{Id}/delete")]
        public async Task<IActionResult> DeleteMessage(int Id)
        {
            var messageFromRepo = await _repo.GetMessage(Id);

            messageFromRepo.SenderDeleted = true;

            if (messageFromRepo.SenderDeleted)
                _repo.Delete(messageFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Error deleting the message");
        }
    }
}