using System;
using Ayehu.Sdk.ActivityCreation.Interfaces;
using Ayehu.Sdk.ActivityCreation.Extension;
using Ayehu.Sdk.ActivityCreation.Helpers;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;

namespace Ayehu.Sdk.ActivityCreation
{
    public class CustomActivity_ZM_Update_a_Meeting : IActivityAsync
    {


    
    public string Jsonkeypath = "";
    
    public string apikey = "";
    
    public string password1 = "";
    
    public string meetingId = "";
    
    public string occurrence_id = "";
    
    public string schedule_for = "";
    
    public string topic = "";
    
    public string type = "";
    
    public string start_time = "";
    
    public string duration = "";
    
    public string timezone = "";
    
    public string password = "";
    
    public string agenda = "";
    
    public string tracking_fields__ = "";
    
    public string field = "";
    
    public string value = "";
    
    public string recurrence_type = "";
    
    public string repeat_interval = "";
    
    public string weekly_days = "";
    
    public string monthly_day = "";
    
    public string monthly_week = "";
    
    public string monthly_week_day = "";
    
    public string end_times = "";
    
    public string end_date_time = "";
    
    private bool omitJsonEmptyorNull = true;
    
    private string contentType = "application/json";
    
    private string endPoint = "https://api.zoom.us";
    
    private string httpMethod = "PATCH";
    
    private string uriBuilderPath {
        get {
            return string.Format("v2/meetings/{0}",meetingId);
        }
    }
    
    private string postData {
        get {
            return string.Format("{{   \"schedule_for\": \"{0}\",   \"topic\": \"{1}\",   \"type\": \"{2}\",   \"start_time\": \"{3}\",   \"duration\": \"{4}\",   \"timezone\": \"{5}\",   \"password\": \"{6}\",   \"agenda\": \"{7}\",   \"tracking_fields\": [     {{       \"field\": \"{8}\",       \"value\": \"{9}\"     }}   ],   \"recurrence\": {{     \"type\": \"{10}\",     \"repeat_interval\": \"{11}\",     \"weekly_days\": \"{12}\",     \"monthly_day\": \"{13}\",     \"monthly_week\": \"{14}\",     \"monthly_week_day\": \"{15}\",     \"end_times\": \"{16}\",     \"end_date_time\": \"{17}\"   }} }}",schedule_for,topic,type,start_time,duration,timezone,password,agenda,field,value,recurrence_type,repeat_interval,weekly_days,monthly_day,monthly_week,monthly_week_day,end_times,end_date_time);
        }
    }
    
    private System.Collections.Generic.Dictionary<string, string> headers {
        get {
            return new Dictionary<string, string>() {{"authorization","Bearer " + AyehuHelper.JWTToken(apikey,password1,"HS256","JWT", 120)}};
        }
    }
    
    private System.Collections.Generic.Dictionary<string, string> queryStringArray {
        get {
            return new Dictionary<string, string>() {};
        }
    }


        public async System.Threading.Tasks.Task<ICustomActivityResult> Execute()
        {

            HttpClient client = new HttpClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            UriBuilder UriBuilder = new UriBuilder(endPoint); 
            UriBuilder.Path = uriBuilderPath;
            UriBuilder.Query = AyehuHelper.queryStringBuilder(queryStringArray);
            HttpRequestMessage myHttpRequestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), UriBuilder.ToString());
           
            if (contentType == "application/x-www-form-urlencoded")
                myHttpRequestMessage.Content = AyehuHelper.formUrlEncodedContent(postData);
            else
              if (string.IsNullOrEmpty(postData) == false)
                if (omitJsonEmptyorNull)
                    myHttpRequestMessage.Content = new StringContent(AyehuHelper.omitJsonEmptyorNull(postData), Encoding.UTF8, "application/json");
                else
                    myHttpRequestMessage.Content = new StringContent(postData, Encoding.UTF8, "application/json");


            foreach (KeyValuePair<string, string> headeritem in headers)
                client.DefaultRequestHeaders.Add(headeritem.Key, headeritem.Value);

            HttpResponseMessage response = client.SendAsync(myHttpRequestMessage).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.OK:
                    {
                        if (string.IsNullOrEmpty(response.Content.ReadAsStringAsync().Result) == false)
                            return this.GenerateActivityResult(response.Content.ReadAsStringAsync().Result, Jsonkeypath);
                        else
                            return this.GenerateActivityResult("Success");
                    }
                default:
                    {
                        if (string.IsNullOrEmpty(response.Content.ReadAsStringAsync().Result) == false)
                            throw new Exception(response.Content.ReadAsStringAsync().Result);
                        else if (string.IsNullOrEmpty(response.ReasonPhrase) == false)
                            throw new Exception(response.ReasonPhrase);
                        else
                            throw new Exception(response.StatusCode.ToString());
                    }
            }
        }

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}