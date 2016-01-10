using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Helpers;
using eUni.WebServices.Models;
using eUni.WebServices.Models.OutputModels;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        private IMessageProvider _messageProvider;

        public MessageController(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        [Route("GetAllPersons")]
        public async Task<IHttpActionResult> GetAllPersons(int? userId)
        {
            if (userId == null)
            {
                return BadRequest("User Id Not Found");
            }

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var userDtos = _messageProvider.GetAllUsers(userId.Value);
            var userViewModels = Mapper.Map<List<UserViewModel>>(userDtos);
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Persons For Converstion "));
            return Content(HttpStatusCode.OK, userViewModels);
        }

        [Route("GetConversationBetweenUsers")]
        public async Task<IHttpActionResult> GetConversationBetweenUsers(int? userId1, int? userId2, int? pageNumber, int? pageSize)
        {
            if (userId1 == null || userId2 == null)
            {
                return BadRequest("User Ids Not Found");
            }

            var filter = new PaginationFilter
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            var messageDtos = _messageProvider.GetConversation(userId1.Value, userId2.Value, filter);
            var messageOutModels = Mapper.Map<List<MessageOutModel>>(messageDtos);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Messages Between Two Users "));
            return Content(HttpStatusCode.OK, messageOutModels);
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(MessageViewModel message)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var messageDto = Mapper.Map<MessageDTO>(message);
            messageDto.From = new DomainUserDTO { DomainUserId = message.FromUserId };
            messageDto.To = new DomainUserDTO { DomainUserId = message.ToUserId };
            _messageProvider.CreateMessage(messageDto);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Message created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }
    }
}
