using Consumption.Shared.Common.Enums;
using System.Text.RegularExpressions;

namespace Consumption.Shared.Common.Validation
{
    public class Validation
    {
        public static bool IsPhone(string phone)
        {
            string regex = string.Empty;

            regex = GetEnumAttrbute.GetDescription(ValidationType.Phone).Description;

            Regex re = new Regex(regex);

            if (re.IsMatch(phone))
            {
                return true;
            }
            else
                return false;
        }
    }
}
