using System.Collections.Generic;
using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface IMessageProvider
    {
        void CreateMessage(MessageDTO messageDto);
        List<DomainUserDTO> GetAllUsers(int userId);
    }
}
