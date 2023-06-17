using BestHTTP;
using BoardsStake.API.Dto;
using System.Threading.Tasks;

namespace BoardsStake.API.Service
{
    public class IPService
    {
        private const string IPIFY_ADDRESS = "https://api.ipify.org?format=json";

        public async static Task<ActionResult<IPDTO>> GetIPAddress()
        {
            HTTPRequest request = ServiceHelper.GetHTTPRequest(IPIFY_ADDRESS, HTTPMethods.Get);

            HTTPResponse response = await ServiceHelper.GetHTTPResponse(request);

            return ServiceHelper.GetActionResult<IPDTO>(response);
        }
    }
}

