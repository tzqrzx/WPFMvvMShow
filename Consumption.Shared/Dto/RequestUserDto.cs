namespace Consumption.Shared.Dto
{
    public class RequestUserDto
    {
        public string channel { get; set; }
        public string code { get; set; }
        public string did { get; set; }
        public string inviteCode { get; set; }
        public string phone { get; set; }
        public string platform { get; set; }

    }

    public class GameSearchDto
    {


        public string category { get; set; }
        public string keyword { get; set; }
        public int offset { get; set; }
        public int pagesize { get; set; }
        public string platform { get; set; }

    }
}
