using System;
using System.Threading.Tasks;
using SecurityLab3;

class Program
{
    private const int MoneyToWin = 1000000;
    private const int BetAfterFindGenerator = 100;

    public static async Task WinLcg()
    {
        var client = new NetworkClient();
        
        await client.InitAccount(Guid.NewGuid().ToString());
        Console.WriteLine("Preparation before find LCG generator:");
        Console.WriteLine("Play three games with bet 1");

        for (int i = 0; i < 3; i++)
        {
            await client.Play(1,1,GameMode.Lcg);
        }

        var lcg = GeneratorsHacker.FindLcg(client.history.ToArray());
        Console.WriteLine($"Find LCG: a = {lcg.GetA()}, c = {lcg.GetC()}");

        while (client.account.money < MoneyToWin)
        {
            var dit = (int)lcg.Next();
            Console.WriteLine($"Play with bet: {BetAfterFindGenerator} and realNumber: {dit}");

            await client.Play(BetAfterFindGenerator, dit,GameMode.Lcg);

            if (client.history[^1] == dit)
            {
                Console.WriteLine($"Win with bet: {BetAfterFindGenerator} and realNumber: {dit}");
            }
            else
            {
                Console.WriteLine($"Lose with bet: {BetAfterFindGenerator} and realNumber: {dit} != {client.history[^1]}");
            }
            
            Console.WriteLine($"Current money: {client.account.money}");
        }
        Console.WriteLine("Done");

    }
    public static async Task WinMt(GameMode mode)
    {
        var client = new NetworkClient();
        
        await client.InitAccount(Guid.NewGuid().ToString());
        Console.WriteLine("Preparation before find state of MT19937 generator:");
        Console.WriteLine($"Play {MtState.N} with mode {mode:g} games with bet 1");
        for (int i = 0; i < MtState.N; i++)
        {
            await client.Play(1,1, mode);
        }

        var reverse = new MtState();
        
        reverse.Init(client.history.ToArray());
        Console.WriteLine($"Find state of MT19937 generator");

        while (client.account.money < MoneyToWin)
        {
            var dit = reverse.Predict();
            await client.Play(BetAfterFindGenerator,dit, mode);
            if (client.history[^1] == dit)
            {
                Console.WriteLine($"Win with bet: {BetAfterFindGenerator} and realNumber: {dit}");
            }
            else
            {
                Console.WriteLine($"Lose with bet: {BetAfterFindGenerator} and realNumber: {dit} != {client.history[^1]}");
            }
            
            Console.WriteLine($"Current money: {client.account.money}");
        }
        Console.WriteLine("Done");
    }
    public static async Task Main()
    {
       await WinLcg();
       await WinMt(GameMode.Mt);
       await WinMt(GameMode.BetterMt);
    }
}