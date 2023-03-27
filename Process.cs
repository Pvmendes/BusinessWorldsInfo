using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Flurl;
using Flurl.Http;
using BusinessWorldsInfo.Model;
using static BusinessWorldsInfo.Model.WorldModel;
using BusinessWorldsInfo.Model.Enum;
using System.Numerics;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;

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

        public Process(string url_tibiadata_domain, string url_tibiadata_path_worlds, string url_tibiadata_path_world_name) 
        {
            URL_tibiadata_Domain = url_tibiadata_domain;
            URL_tibiadata_path_worlds = url_tibiadata_path_worlds;
            URL_tibiadata_path_world_name = url_tibiadata_path_world_name;

            WorldModelList = new List<WorldModel.Root>();
            

            this.GetWorldsFromTibiaData();
            this.GetEachWorldInfo();
        }

        public void ActionStart()
        {
            Console.WriteLine("Hello, Process List " + WorldModelList.Count);

            do
            {
                this.processingEachWorld();

                foreach (var item in ResumeWorldList)
                {
                    Console.WriteLine($"{item.NameWorld} RegisterDate:{item.RegisterDate}");
                }

                Console.WriteLine("1 hour ");
                Thread.Sleep(3600000);

                this.GetWorldsFromTibiaData();
                this.GetEachWorldInfo();
            } while (true);
        }

        private void processingEachWorld()
        {
            ResumeWorldList = new List<ResumeWorld>();

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

                    var vocation = this.vocation(player);

                    switch (vocation)
                    {
                        case VocationEnum.None:
                            break;
                        case VocationEnum.Druid:
                            ResumeWorld.druid.DruidQt += 1;
                            ResumeWorld.druid.rangelvl = this.rangelvlEnumVocation(range, ResumeWorld.druid.rangelvl);
                            break;
                        case VocationEnum.Knight:
                            ResumeWorld.knight.KnightQt += 1;
                            ResumeWorld.knight.rangelvl = this.rangelvlEnumVocation(range, ResumeWorld.druid.rangelvl);
                            break;
                        case VocationEnum.Paladin:
                            ResumeWorld.paladin.PaladinQt += 1;
                            ResumeWorld.paladin.rangelvl = this.rangelvlEnumVocation(range, ResumeWorld.druid.rangelvl);
                            break;
                        case VocationEnum.Sorcerer:
                            ResumeWorld.sorcecer.SorcecerQt += 1;
                            ResumeWorld.sorcecer.rangelvl = this.rangelvlEnumVocation(range, ResumeWorld.druid.rangelvl);
                            break;
                        default:
                            break;
                    }
                }

                ResumeWorldList.Add(ResumeWorld);
            }

            this.WriteCSVLocal();
        }

        private void WriteCSVLocal()
        {
            var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
            using (var writer = new StreamWriter("C:\\dados\\fileWorlds.csv"))
            using (var csv = new CsvWriter(writer, configPersons))
            {
                csv.WriteRecords(ResumeWorldList);
            }
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

        private VocationEnum vocation(OnlinePlayer player)
        {
            if (player.vocation.Contains("Druid"))
            {
                return VocationEnum.Druid;
            }
            else if (player.vocation.Contains("Knight"))
            {
                return VocationEnum.Knight;
            }
            else if (player.vocation.Contains("Paladin"))
            {
                return VocationEnum.Paladin;
            }
            else if (player.vocation.Contains("Sorcerer"))
            {
                return VocationEnum.Sorcerer;
            }

            return VocationEnum.None;
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

        private void GetWorldsFromTibiaData()
        {
            var url = URL_tibiadata_Domain + URL_tibiadata_path_worlds;
            var result = url
                .GetJsonAsync<WorldsListModel.Root>().Result;

            WorldsModelList = result;
        }

        private void GetEachWorldInfo()
        {
            foreach (var world in WorldsModelList.worlds.regular_worlds)
            {
                var url = URL_tibiadata_Domain + URL_tibiadata_path_world_name + world.name;

                var result = url.GetJsonAsync<WorldModel.Root>().Result;

                WorldModelList.Add(result);
            } 
        }
    }
}
