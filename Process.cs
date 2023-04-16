
using Flurl.Http;
using BusinessWorldsInfo.Model;
using static BusinessWorldsInfo.Model.WorldModel;
using BusinessWorldsInfo.Model.Enum;
using BusinessWorldsInfo.Utils;
using BusinessWorldsInfo.Business.HTTPs_Requests;

namespace BusinessWorldsInfo
{
    public class Process
    {
        public string URL_tibiadata_Domain { get; set; }
        public string URL_tibiadata_path_worlds { get; set; }
        public string URL_tibiadata_path_world_name { get; set; }

        public WorldsListModel.Root WorldsModelList { get; set; }

        public List<WorldModel.Root> WorldModelList { get; set; }

        public List<ResumeWorld> ResumeWorldList { get; set; }

        public string FilePath { get; set; }

        public WorldsRequest WorldsRequest { get; set; }

        public Process() 
        {
            ResumeWorldList = new List<ResumeWorld>();

            WorldsRequest = new WorldsRequest();
        }

        public void ActionStart()
        {
            WorldsModelList = WorldsRequest.GetWorldsFromTibiaData(URL_tibiadata_Domain + URL_tibiadata_path_worlds);

            do
            {
                try
                {
                    WorldModelList = WorldsRequest.GetEachWorldInfo(URL_tibiadata_Domain + URL_tibiadata_path_world_name, WorldsModelList.worlds.regular_worlds);
                    
                    Console.WriteLine("Hello, Process List " + WorldModelList.Count);

                    this.processingEachWorld();

                    foreach (var item in ResumeWorldList)
                    {
                        Console.WriteLine($"{item.NameWorld} RegisterDate:{item.RegisterDate}");
                    }

                    Console.WriteLine($"{ResumeWorldList.Count} RegisterDate:{DateTime.Now}");

                    Thread.Sleep(10000);

                    //Console.WriteLine("1 hour ");
                    //Thread.Sleep(3600000);

                    //Console.WriteLine("30 min ");
                    //Thread.Sleep(1800000);
                }
                catch (Exception)
                {
                }
                
            } while (true);
        }

        private void processingEachWorld()
        {
            foreach (var item in WorldModelList)
            {
                var ResumeWorld = new ResumeWorld();

                ResumeWorld.TotalQtPlayer = item.worlds.world.online_players.Count;

                ResumeWorld.NameWorld = item.worlds.world.name;

                ResumeWorld.RegisterDate = DateTime.Now;

                foreach (var player in item.worlds.world.online_players)
                {
                    var range = this.rangelvlEnum(player);

                    switch (range)
                    {
                        case RangelvlEnum.None:
                            break;
                        case RangelvlEnum.lvl8to50:
                            ResumeWorld.rangelvl.lvl8to50 += 1;
                            break;
                        case RangelvlEnum.lvl51to100:
                            ResumeWorld.rangelvl.lvl51to100 += 1;
                            break;
                        case RangelvlEnum.lvl101to150:
                            ResumeWorld.rangelvl.lvl101to150 += 1;
                            break;
                        case RangelvlEnum.lvl151to250:
                            ResumeWorld.rangelvl.lvl151to250 += 1;
                            break;
                        case RangelvlEnum.lvl251to450:
                            ResumeWorld.rangelvl.lvl251to450 += 1;
                            break;
                        case RangelvlEnum.lvl451to650:
                            ResumeWorld.rangelvl.lvl451to650 += 1;
                            break;
                        case RangelvlEnum.lvl651to850:
                            ResumeWorld.rangelvl.lvl651to850 += 1;
                            break;
                        case RangelvlEnum.lvl851to1000:
                            ResumeWorld.rangelvl.lvl851to1000 += 1;
                            break;
                        case RangelvlEnum.lvl1001to1500:
                            ResumeWorld.rangelvl.lvl1001to1500 += 1;
                            break;
                        case RangelvlEnum.lvl1501:
                            ResumeWorld.rangelvl.lvl1501 += 1;
                            break;
                        default:
                            break;
                    }

                    if (player.vocation.Contains("Druid"))
                    {
                        ResumeWorld.druid.DruidQt += 1;
                        ResumeWorld.druid.Rangelvl = this.rangelvlEnumVocation(range, ResumeWorld.druid.Rangelvl);
                    }
                    else if (player.vocation.Contains("Knight"))
                    {
                        ResumeWorld.knight.KnightQt += 1;
                        ResumeWorld.knight.Rangelvl = this.rangelvlEnumVocation(range, ResumeWorld.druid.Rangelvl);
                    }
                    else if (player.vocation.Contains("Paladin"))
                    {
                        ResumeWorld.paladin.PaladinQt += 1;
                        ResumeWorld.paladin.Rangelvl = this.rangelvlEnumVocation(range, ResumeWorld.druid.Rangelvl);
                    }
                    else if (player.vocation.Contains("Sorcerer"))
                    {
                        ResumeWorld.sorcecer.SorcecerQt += 1;
                        ResumeWorld.sorcecer.Rangelvl = this.rangelvlEnumVocation(range, ResumeWorld.druid.Rangelvl);
                    }
                }

                ResumeWorldList.Add(ResumeWorld);
            }

            JsonFileUtils.JsonWriteLocal(
                $"{FilePath}fileWorlds_{DateTime.Now.ToString("dd-MM-yyyy")}_.json", 
                ResumeWorldList.Distinct());

            CSVFileUtils.CSVWriteLocal(
                filePath: $"{FilePath}fileWorlds_{DateTime.Now.ToString("dd-MM-yyyy")}_.csv",
                ResumeWorldList.Distinct());
        }

        private Rangelvl rangelvlEnumVocation(RangelvlEnum range, Rangelvl rangelvl)
        {
            switch (range)
            {
                case RangelvlEnum.None:
                    break;
                case RangelvlEnum.lvl8to50:
                    rangelvl.lvl8to50 += 1;
                    break;
                case RangelvlEnum.lvl51to100:
                    rangelvl.lvl51to100 += 1;
                    break;
                case RangelvlEnum.lvl101to150:
                    rangelvl.lvl101to150 += 1;
                    break;
                case RangelvlEnum.lvl151to250:
                    rangelvl.lvl151to250 += 1;
                    break;
                case RangelvlEnum.lvl251to450:
                    rangelvl.lvl251to450 += 1;
                    break;
                case RangelvlEnum.lvl451to650:
                    rangelvl.lvl451to650 += 1;
                    break;
                case RangelvlEnum.lvl651to850:
                    rangelvl.lvl651to850 += 1;
                    break;
                case RangelvlEnum.lvl851to1000:
                    rangelvl.lvl851to1000 += 1;
                    break;
                case RangelvlEnum.lvl1001to1500:
                    rangelvl.lvl1001to1500 += 1;
                    break;
                case RangelvlEnum.lvl1501:
                    rangelvl.lvl1501 += 1;
                    break;
                default:
                    break;
            }

            return rangelvl;
        }
              
        private RangelvlEnum rangelvlEnum(OnlinePlayer player)
        {
            if (player.level >= 8 && player.level <= 50)
            {
               return RangelvlEnum.lvl8to50;
            }
            else if (player.level >= 51 && player.level <= 100)
            {
                return RangelvlEnum.lvl51to100;
            }
            else if (player.level >= 101 && player.level <= 150)
            {
                return RangelvlEnum.lvl101to150;
            }
            else if (player.level >= 151 && player.level <= 250)
            {
                return RangelvlEnum.lvl151to250;
            }
            else if (player.level >= 251 && player.level <= 450)
            {
                return RangelvlEnum.lvl251to450;
            }
            else if (player.level >= 451 && player.level <= 650)
            {
                return RangelvlEnum.lvl451to650;
            }
            else if (player.level >= 651 && player.level <= 850)
            {
                return RangelvlEnum.lvl651to850;
            }
            else if (player.level >= 851 && player.level <= 1000)
            {
                return RangelvlEnum.lvl851to1000;
            }
            else if (player.level >= 1001 && player.level <= 1500)
            {
                return RangelvlEnum.lvl1001to1500;
            }
            else if (player.level >= 1501)
            {
                return RangelvlEnum.lvl1501;
            }

            return RangelvlEnum.None;
        }
    }
}
