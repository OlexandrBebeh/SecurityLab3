// See https://aka.ms/new-console-template for more information

using System;
using System.Threading.Tasks;
using SecurityLab3;

class Program
{
    public static async Task Main()
    {
        var client = new NetworkClient();
        
        await client.InitAccount(Guid.NewGuid().ToString());

        for (int i = 0; i < 3; i++)
        {
            await client.Play(1,1,GameMode.Lcg);
        }

        var lcg = GeneratorsHacker.FindLcg(client.history.ToArray());

        while (client.account.money < 1000000)
        {
            await client.Play(100,lcg.Next(),GameMode.Lcg);
        }
        
        client = new NetworkClient();
    }
}

