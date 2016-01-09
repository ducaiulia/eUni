using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    /// <summary>
    /// An object that encapsulates the result of an action.
    /// </summary>
    public class ResultActionDTO
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
