namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class StudentTestDTO
    {
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public int Grade { get; set; }
        public DomainUserDTO DomainUser { get; set; }
        public TestDTO Homework { get; set; }
    }
}