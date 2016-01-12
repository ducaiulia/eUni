using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUni.DataAccess.Domain
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public virtual Module Module { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public virtual List<Test> Tests { get; set; }


    }
}
