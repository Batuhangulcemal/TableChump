using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestHTTP;
using Newtonsoft.Json;
using UnityEngine;

namespace AsepStudios.TableChump.API
{
    internal class ServiceHelper
    {
        public static HTTPRequest GetHTTPRequest(string body, string address, HTTPMethods hTTPMethod)
        {
            var request = GetHTTPRequest(address, hTTPMethod);

            request.RawData = System.Text.Encoding.UTF8.GetBytes(body);

            return request;
        }

        public static HTTPRequest GetHTTPRequest(string address, HTTPMethods hTTPMethod)
        {
            var request = new HTTPRequest(new Uri(address), hTTPMethod);

            request.SetHeader("Content-Type", "application/json; charset=UTF-8");

            return request;
        }

        public static async Task<HTTPResponse> GetHTTPResponse(HTTPRequest request)
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

            T responseDto;

            try
            {
                responseDto = JsonConvert.DeserializeObject<T>(response.DataAsText);
            }
            catch (Exception ex)
            {
                responseDto = default;
                Debug.Log(ex);
            }
            return new ActionResult<T>
            {
                Data = responseDto,
                StatusCode = response.StatusCode,
                Message = response.Message,
            };
        }

        public static ActionResult<List<T>> GetActionResultList<T>(HTTPResponse response)
        {
            Debug.Log(response.StatusCode + " " + response.Message);

            List<T> responseDto;
            try
            {
                responseDto = JsonConvert.DeserializeObject<List<T>>(response.DataAsText);
            }
            catch (Exception ex)
            {
                responseDto = new List<T>();
                Debug.Log(ex);

            }

            return new ActionResult<List<T>>
            {
                Data = responseDto,
                StatusCode = response.StatusCode,
                Message = response.Message,
            };
        }
    }
}
