using BestHTTP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AsepStudios.API
{
    internal class ServiceHelper
    {
        public static HTTPRequest GetHTTPRequest(string body, string address, HTTPMethods hTTPMethod)
        {
            HTTPRequest request = GetHTTPRequest(address, hTTPMethod);

            request.RawData = System.Text.Encoding.UTF8.GetBytes(body);

            return request;
        }

        public static HTTPRequest GetHTTPRequest(string address, HTTPMethods hTTPMethod)
        {
            HTTPRequest request = new HTTPRequest(new Uri(address), hTTPMethod);

            request.SetHeader("Content-Type", "application/json; charset=UTF-8");

            return request;
        }

        public async static Task<HTTPResponse> GetHTTPResponse(HTTPRequest request)
        {
            HTTPResponse response;

            try
            {
                response = await request.GetHTTPResponseAsync();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                response = null;
            }

            return response;
        }

        public static ActionResult GetActionResult(HTTPResponse response)
        {
            Debug.Log(response.StatusCode + " " + response.Message);
            return new ActionResult
            {
                StatusCode = response.StatusCode,
                Message = response.Message,
            };
        }

        public static ActionResult<T> GetActionResult<T>(HTTPResponse response)
        {
            Debug.Log(response.StatusCode + " " + response.Message);

            T responseDTO;

            try
            {
                responseDTO = JsonConvert.DeserializeObject<T>(response.DataAsText);
            }
            catch (Exception ex)
            {
                responseDTO = default;
                Debug.Log(ex);
            }
            return new ActionResult<T>
            {
                Data = responseDTO,
                StatusCode = response.StatusCode,
                Message = response.Message,
            };
        }

        public static ActionResult<List<T>> GetActionResultList<T>(HTTPResponse response)
        {
            Debug.Log(response.StatusCode + " " + response.Message);

            List<T> responseDTO;
            try
            {
                responseDTO = JsonConvert.DeserializeObject<List<T>>(response.DataAsText);
            }
            catch (Exception ex)
            {
                responseDTO = new List<T>();
                Debug.Log(ex);

            }

            return new ActionResult<List<T>>
            {
                Data = responseDTO,
                StatusCode = response.StatusCode,
                Message = response.Message,
            };
        }
    }
}
