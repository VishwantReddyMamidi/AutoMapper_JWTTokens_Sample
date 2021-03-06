﻿using Automapper_JWTTokens_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automapper_JWTTokens_Demo.Dtos
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int HitPoints { get; set; }
        public int Strength { get; set; } 
        public int Defense { get; set; }
        public int Intelligence { get; set; } 
        public RpgClass Class { get; set; }
    }
}
