using BestHTTP;
using System.Threading.Tasks;
using AsepStudios.TableChump.API;
using AsepStudios.TableChump.API.Dto;

namespace AsepStudios.API.Service
{
    public class IPService
    {
        private const string IPIFY_ADDRESS = "https://api.ipify.org?format=json";

        public static async Task<ActionResult<IpDto>> GetIPAddress()
        {
            var request = ServiceHelper.GetHTTPRequest(IPIFY_ADDRESS, HTTPMethods.Get);

            var response = await ServiceHelper.GetHTTPResponse(request);

            return ServiceHelper.GetActionResult<IpDto>(response);
        }
    }
}

