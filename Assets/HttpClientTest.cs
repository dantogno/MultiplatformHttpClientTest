using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class HttpClientTest : MonoBehaviour 
{

    void Start()
    {
        //IgnoreSslErrors();

        Debug.Log("Start time: " + Time.time);

		RunTest1();

        Test2();
    }

	private static void IgnoreSslErrors()
	{
		// Without this code, when run in the editor, Test 1 will give the following error:
		// "TlsException: Invalid certificate received from server. Error code: 0x3"
		// On Windows I can update the mono security store with mozroots to correct this.
		// On Mac it doesn't seem to fix the issue.
		System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
	}

    private void RunTest1()
    {
        // This works on Windows and, if you ignore SSL errors, this works in the editor in Mac.
        // However, in a Mac build, no matter what I get this error:
        /*
        * NullReferenceException: Object reference not set to an instance of an object
        at System.Net.ServicePointManager.FindServicePoint (System.Uri address, System.Net.IWebProxy proxy) [0x00140] in <f044fe2c9e7e4b8e91984b41f0cf0b04>:0 
        at System.Net.HttpWebRequest.GetServicePoint () [0x00027] in <f044fe2c9e7e4b8e91984b41f0cf0b04>:0 at System.Net.HttpWebRequest.set_Proxy (System.Net.IWebProxy value) [0x00014] in <f044fe2c9e7e4b8e91984b41f0cf0b04>:0 
        at System.Net.Http.HttpClientHandler.CreateWebRequest (System.Net.Http.HttpRequestMessage request) [0x0011e] in <cbcaef35d3364f8bb9ae7449eac280e9>:0 
        at System.Net.Http.HttpClientHandler+<SendAsync>c__async0.MoveNext () [0x0006a] in <cbcaef35d3364f8bb9ae7449eac280e9>:0 
        */
        // I don't think this is the same SSL error. Perhaps it is related to:
        // https://forums.xamarin.com/discussion/93855/why-isnt-httpclient-working

        var message = Test1();
        Debug.Log(message.Result.Content.Headers.ToString());
    }

    private Task<HttpResponseMessage> Test1()
    {
        var httpClient = new HttpClient();

        var t = httpClient.GetAsync("http://stackoverflow.com", HttpCompletionOption.ResponseHeadersRead);

        return t;
    }

    private void Test2()
    {
        // This seems to work.
        var httpClient = new HttpClient();

        httpClient.GetStringAsync("http://www.microsoft.com").ContinueWith(t => Debug.Log(t));
    }
}
