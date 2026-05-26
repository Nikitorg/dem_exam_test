using demExam.dbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demExam.Classes
{
    internal class Context
    {
        public static demExamEntities dbContext = new demExamEntities();
    }
}
