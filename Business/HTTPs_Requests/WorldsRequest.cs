using BusinessWorldsInfo.Model;
using Flurl;
using Flurl.Http;
using static BusinessWorldsInfo.Model.WorldsListModel;

namespace BusinessWorldsInfo.Business.HTTPs_Requests
{
    public class WorldsRequest
    {
        public WorldsListModel.Root GetWorldsFromTibiaData(string url)
        {
            var result = url.GetJsonAsync<WorldsListModel.Root>().Result;

            return result;
        }

        internal List<WorldModel.Root> GetEachWorldInfo(string url, List<RegularWorld> regular_worlds)
        {
            var WorldModelList = new List<WorldModel.Root>();

            foreach (var world in regular_worlds)
            {
                var request = url + world.name;

                var result = request.GetJsonAsync<WorldModel.Root>().Result;

                WorldModelList.Add(result);
            }

            return WorldModelList;
        }
    }
}
