namespace Consumption.Shared.Dto
{
    public class HeaderDto
    {
        public string XClientInfo { get; set; }
        public string XSid { get; set; }
        public string XDid { get; set; }
        public string XSign { get; set; }
        public string XTraceId { get; set; }
        public string XTs { get; set; }
        public string XUa { get; set; }
        public string XUid { get; set; }
    }

    public class phoneDto
    {
        public string opType { get; set; }
        public string phone { get; set; }

    }
}
