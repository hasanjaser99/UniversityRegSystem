namespace UniversityRegSystem.Shared.DTOS
{
    public class BaseResult
    {
        public bool IsSucsess { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}