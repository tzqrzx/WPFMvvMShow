using System;

namespace ZYModel.RequestModel
{
    public class BasePaging
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }

    public class BaseTime
    {
        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }
    }
}
