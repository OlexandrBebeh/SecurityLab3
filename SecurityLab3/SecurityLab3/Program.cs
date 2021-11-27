// See https://aka.ms/new-console-template for more information

using System;
using System.Threading.Tasks;
using SecurityLab3;
using SecurityLab3.Generators;

class Program
{
    public static async Task Main()
    {
        /*var client = new NetworkClient();
        
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
        
        client = new NetworkClient();*/

        var m = new Mt(8675309);

        var p = new MtReverse();
        
        p.Init(m.GetSequence());

        while (true)
        {
            var p1 = m.Next() ;
            var p2 = p.Predict();
            if (p1 != p2)
            {
                break;
            }
        }
    }
}

