using eUni.BusinessLogic.Providers.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.IProviders
{
    public interface IStudentHomeworkProvider
    {
        void CreateStudentHomework(StudentHomeworkDTO hw);
    }
}
