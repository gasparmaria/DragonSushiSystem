using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushiSystem.Models
{
    public class Comanda
    {
        public int ComandaID { get; set; }

        public int FK_Mesa { get; set; }

        public int FK_Cliente { get; set; }

        public bool ComandaStatus { get; set; }
    }
}