
namespace UiTest.Service.ErrorCode
{
    public class ErrorCodeMapperConfig
    {
        public ErrorCodeMapperConfig()
        {
            LocalFilePath = "./Errorcodes.csv";
            LocalNewFilePath = "./NewErrorcodes.csv";
            MaxLength = 6;
        }
        public string LocalFilePath { get; set; }
        public string LocalNewFilePath { get; set; }
        public int MaxLength { get; set; }
    }
}
