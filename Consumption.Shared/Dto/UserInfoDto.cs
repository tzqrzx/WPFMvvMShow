using Consumption.Shared.DataModel;
using System.Collections.Generic;

namespace Consumption.Shared.Dto
{
    public class UserInfoDto
    {
        public User User { get; set; }

        public List<Menu> Menus { get; set; }
    }
}
