using AsepStudios.API;
using AsepStudios.API.Dto;
using AsepStudios.API.Service;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        GetIP();
    }

    private async void GetIP()
    {
        ActionResult<IPDTO> result = await IPService.GetIPAddress();

        Debug.Log(result.Data.ip);
    }
}
