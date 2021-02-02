﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToPlan.Models
{
    public class LeisureType
    {
        public int LeisureTypeId { get; set; }
        public string Name { get; set; }
        public List<Event> Events { get; set; }

        public LeisureType(string n)
        {
            this.Name = n;
        }

        public LeisureType() { }
    }
}