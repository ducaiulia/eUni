using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    /// <summary>
    /// An object that encapsulates the result of an action.
    /// </summary>
    public class ResultViewModel
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}