using eUni.DataAccess.Enums;

namespace eUni.WebServices.Models.OutputModels
{
    public class FileOutModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int ModuleId { get; set; }
    }
}