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
    }
}
