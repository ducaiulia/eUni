using System.Collections.Generic;
using eUni.DataAccess.Domain;

namespace eUni.DataAccess.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        List<DomainUser> GetToUsersByFromUserId(int fromUserId);
    }
}
