﻿using System;

namespace ZYModel.Entity
{
    [Serializable]
    public class User : BaseEntity
    {
        public string Account { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime LastTime { get; set; }

        public DateTime EndTime { get; set; }

        public int IsLocked { get; set; }

        public DateTime CreateTime { get; set; }

        public string FlagAdmin { get; set; }

        public string FlagOnline { get; set; }

        public int LoginCounter { get; set; }
    }
}
