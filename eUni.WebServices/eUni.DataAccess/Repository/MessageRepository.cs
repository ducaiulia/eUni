using System;
using System.Collections.Generic;
using System.Linq;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;

namespace eUni.DataAccess.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<DomainUser> GetToUsersByFromUserId(int fromUserId)
        {
            var result = Context.Messages
                .Where(x => x.From.DomainUserId == fromUserId)
                .Select(x => x.To)
                .GroupBy(x => x.DomainUserId)
                .Select(x => x.FirstOrDefault())
                .ToList();

            return result;
        }
    }
}
