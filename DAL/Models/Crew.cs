﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Crew:Entity
    {
        public Pilot Pilot { get; set; }
        public List<Stewadress> Stewadresses { get; set; }
    }
}
