using System;
using System.Collections.Generic;
using System.Net.Http;
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

        private CasinoAccount account = new();

        public List<CasinoResponse> history;

        public async void InitAccount(String name)
        {
            var response = await client.GetAsync($"{casinoURL}{createAccParam}?id={account.id}");
            
            var text = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<CasinoAccount>(text);
        }

        public async void Play(int bet, long number, GameMode mod)
        {
            var response =
                await client.GetAsync(
                    $"{casinoURL}{playParam}{mod.ToString("g")}?id={account.id}&bet={bet}&number={number}");

            var text = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<CasinoResponse>(text);
        }
    }
}