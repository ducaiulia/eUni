using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class MessageProvider : IMessageProvider
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageProvider(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public void CreateMessage(MessageDTO messageDto)
        {
            var message = Mapper.Map<Message>(messageDto);
            message.From = _userRepository.Get(u => u.DomainUserId == messageDto.From.DomainUserId);
            message.To = _userRepository.Get(u => u.DomainUserId == messageDto.To.DomainUserId);
            _messageRepository.Add(message);
        }

        public List<DomainUserDTO> GetAllUsers(int userId)
        {
            var users = _messageRepository.GetToUsersByFromUserId(userId);
            var userDtos = Mapper.Map<List<DomainUserDTO>>(users);
            return userDtos;
        }

        public List<MessageDTO> GetConversation(int userId1, int userId2, PaginationFilter filter)
        {
            var messages = _messageRepository.GetAll()
                .Where(x =>
                    (x.From.DomainUserId == userId1 && x.To.DomainUserId == userId2)
                    || (x.From.DomainUserId == userId2 && x.To.DomainUserId == userId1))
                    .OrderBy(x => x.DateTime)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);

            var messageDtos = Mapper.Map<List<MessageDTO>>(messages);

            return messageDtos;
        }
    }
}
