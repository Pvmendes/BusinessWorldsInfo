
namespace BusinessWorldsInfo.Model
{
    public class WorldModel
    {
        public class Root
        {
            public Information information { get; set; }
            public Worlds worlds { get; set; }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Information
        {
            public int api_version { get; set; }
            public string timestamp { get; set; }
        }

        public class OnlinePlayer
        {
            public int level { get; set; }
            public string name { get; set; }
            public string vocation { get; set; }
        }

        public class World
        {
            public string battleye_date { get; set; }
            public bool battleye_protected { get; set; }
            public string creation_date { get; set; }
            public string game_world_type { get; set; }
            public string location { get; set; }
            public string name { get; set; }
            public List<OnlinePlayer> online_players { get; set; }
            public int players_online { get; set; }
            public bool premium_only { get; set; }
            public string pvp_type { get; set; }
            public string record_date { get; set; }
            public int record_players { get; set; }
            public string status { get; set; }
            public string tournament_world_type { get; set; }
            public string transfer_type { get; set; }
            public List<string> world_quest_titles { get; set; }
        }

        public class Worlds
        {
            public World world { get; set; }
        }
    }
}
