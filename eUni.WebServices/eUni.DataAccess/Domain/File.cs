using System.ComponentModel.DataAnnotations.Schema;
using eUni.DataAccess.Enums;

namespace eUni.DataAccess.Domain
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public virtual Module Module { get; set; }
        public virtual StudentHomework StudentHomework { get; set; }
    }
}
