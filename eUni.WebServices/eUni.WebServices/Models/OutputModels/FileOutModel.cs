using eUni.DataAccess.Enums;

namespace eUni.WebServices.Models.OutputModels
{
    public class FileOutModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        //public int Size { get; set; }
        //public FileType FileType { get; set; }
        public NamedEntityOutModel Module { get; set; }
    }
}