using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWorldsInfo.Model
{
    public class Rangelvl
    {
        public Rangelvl(string prefix = "")
        {
            this.Prefix = prefix;
        }
        public string Prefix { get; set; }

        [Index(0)]
        [Name("lvl8to50")]
        public int lvl8to50 { get; set; }

        [Index(1)]
        [Name("lvl51to100")]
        public int lvl51to100{ get; set; }

        [Index(2)]
        [Name("lvl101to150")]
        public int lvl101to150 { get; set; }

        [Index(3)]
        [Name("lvl151to250")]
        public int lvl151to250 { get; set; }

        [Index(4)]
        [Name("lvl251to450")]
        public int lvl251to450{ get; set; }

        [Index(5)]
        [Name("lvl451to650")]
        public int lvl451to650{ get; set; }

        [Index(6)]
        [Name("lvl651to850")]
        public int lvl651to850 { get; set; }

        [Index(7)]
        [Name("lvl851to1000")]
        public int lvl851to1000 { get; set; }

        [Index(8)]
        [Name("lvl1001to1500")]
        public int lvl1001to1500 { get; set; }

        [Index(9)]
        [Name("lvl1501")]
        public int lvl1501 { get; set; }
    }
}
