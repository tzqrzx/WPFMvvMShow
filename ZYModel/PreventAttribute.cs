using System;

namespace ZYModel
{
    /// <summary>
    /// 标记不序列化
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PreventAttribute : Attribute
    {
    }
}
