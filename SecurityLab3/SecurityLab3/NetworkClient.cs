using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SecurityLab3
{
    public class NetworkClient
    {
        public class CasinoAccount
        {
            public String accountId { get; set; }
            public String accountMoney { get; set; }
            public String accountDeletionTime { get; set; }
        }
        
        public class CasinoResponse
        {
            public String responseMessage { get; set; }
            public CasinoAccount responseAccount { get; set; }
            public int responseRealNumber { get; set; }
        }
        private static readonly  HttpClient client = new HttpClient();

        private readonly String casinoURL = "http://95.217.177.249/casino/";
        
        private readonly String createAccParam = "createacc";
        
        private readonly String playParam = "play";
        
        private CasinoAccount account = new ();


        public void InitAccount(String name)
        {
            var values = new Dictionary<string, string>
            {
                { "id", name }
            };
            var content = new FormUrlEncodedContent(values);

            var response = client.GetAsync("http://www.example.com/recepticle.aspx?id={name}");
        }
    }
}