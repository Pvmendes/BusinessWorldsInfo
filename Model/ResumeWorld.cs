using CsvHelper.Configuration.Attributes;

namespace BusinessWorldsInfo.Model
{
    public class ResumeWorld
    {
        public ResumeWorld()
        {
            rangelvl = new Rangelvl();
            druid = new Druid();
            sorcecer = new Sorcecer();
            knight = new Knight();
            paladin = new Paladin();
        }

        [Index(0)]
        public DateTime RegisterDate { get; set; }

        [Index(1)]
        public string NameWorld { get; set; }

        [Index(2)]
        public int TotalQtPlayer { get; set; }        

        [Index(3)]
        public Rangelvl rangelvl { get; set; }

        [Index(4)]
        public Druid druid { get; set; }

        [Index(5)]
        public Sorcecer sorcecer { get; set; }

        [Index(6)]
        public Knight knight { get; set; }

        [Index(7)]
        public Paladin paladin { get; set; }
             
        public class Druid
        {
            public Druid()
            {
                Rangelvl = new Rangelvl();
            }

            public int DruidQt { get; set; }
            
            public Rangelvl Rangelvl { get; set; }
        }

        public class Sorcecer
        {
            public Sorcecer()
            {
                Rangelvl = new Rangelvl();
            }

            public int SorcecerQt { get; set; }
            public Rangelvl Rangelvl { get; set; }
        }

        public class Knight
        {
            public Knight()
            {
                Rangelvl = new Rangelvl();
            }

            public int KnightQt { get; set; }
            public Rangelvl Rangelvl { get; set; }
        }

        public class Paladin
        {
            public Paladin()
            {
                Rangelvl = new Rangelvl();
            }
            public int PaladinQt { get; set; }
            public Rangelvl Rangelvl { get; set; }
        }
    }
}
