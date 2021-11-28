using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SecurityLab3
{        
    public enum GameMode
    {
        Lcg,
        Mt,
        BetterMt
    }
    public class NetworkClient
    {
        public class CasinoAccount
        {
            public String id { get; set; }
            public long money { get; set; }
            public String deletionTime { get; set; }
        }

        public class CasinoResponse
        {
            public String message { get; set; }
            public CasinoAccount account { get; set; }
            public long realNumber { get; set; }
        }

        private static readonly HttpClient client = new HttpClient();

        private readonly String casinoURL = "http://95.217.177.249/casino/";

        private readonly String createAccParam = "createacc";

        private readonly String playParam = "play";

        public CasinoAccount account = new();

        public List<long> history = new();

        public async Task InitAccount(string name)
        {
            var response = await client.GetAsync($"{casinoURL}{createAccParam}?id={name}");
            
            var text = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<CasinoAccount>(text);

            account = json;
        }

        public async Task Play(int bet, long number, GameMode mod)
        {
            var response =
                await client.GetAsync(
                    $"{casinoURL}{playParam}{mod:g}?id={account.id}&bet={bet}&number={number}");

            var text = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<CasinoResponse>(text);

            if (json != null)
            {
                history.Add(json.realNumber);
                account.money = json.account.money;
            }
        }
    }
}