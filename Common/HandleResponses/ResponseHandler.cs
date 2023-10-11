using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.HandleResponses
{
    public static class ResponseHandler
    {
        public static async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseJson);

            return responseObj;
        }
        
    }
}


