using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHospital
{
    class WhatsApp
    {
        public static void Send(string msg)
        {
            var client = new RestClient("https://app.messageautosender.com/message/new");
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("username", "marwarisoftware@gmail.com");
            request.AddParameter("password", "Marwari@#123");
            request.AddParameter("receiverMobileNo", "9045192768");
            //request.AddParameter("receiverMobileNo", "9557194664");
            request.AddParameter("message", msg);

            IRestResponse response = client.Execute(request);
        }

    }
}