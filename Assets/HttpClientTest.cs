using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class HttpClientTest : MonoBehaviour 
{

    void Start()
    {
        // System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };

        Debug.Log("Start time: " + Time.time);
        Test2();
        //var message = Test1();
        //Debug.Log(message.Result.Content.Headers.ToString());

    }

    private Task<HttpResponseMessage> Test1()
    {
        var httpClient = new HttpClient();

        var t = httpClient.GetAsync("http://stackoverflow.com", HttpCompletionOption.ResponseHeadersRead);

        return t;
    }

    private void Test2()
    {
        var httpClient = new HttpClient();

        httpClient.GetStringAsync("http://www.microsoft.com").ContinueWith(t => Debug.Log(t));
    }
}
