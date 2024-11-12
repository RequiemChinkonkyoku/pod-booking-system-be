﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UpdateMembershipRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Discount { get; set; }

        public int PointsRequirement { get; set; }
    }
}
