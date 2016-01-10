using eUni.BusinessLogic.IProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class LogProvider : ILogProvider
    {
        private ILogsRepository _logRepo;

        public LogProvider(ILogsRepository logRepo)
        {
            _logRepo = logRepo;
        }
        public List<LogDTO> GetAllLogs(bool isPaginated, PaginationFilter filter)
        {
            IQueryable<Log> logs;
            if (isPaginated)
            {
                logs = _logRepo.GetAll().Where(l => l.Level == "INFO")
                    .OrderBy(x => x.Date)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
            }
            else
            {
                logs = _logRepo.GetAll().Where(l => l.Level == "INFO");
            }

            List<LogDTO> res = new List<LogDTO>();
            foreach (var item in logs)
            {
                var pairs = item.Message.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var date = pairs[2].Substring(pairs[2].IndexOf(':') + 1);

                res.Add(new LogDTO
                {
                    User = pairs[0].Split(':')[1],
                    Action = pairs[1].Split(':')[1],
                    Date = date
                });
            }
            return res;
        }
    }
}
