using System.Diagnostics;
using static Common.HashBasedCommitments;

class Client
{
    static void Main(string[] args)
    {
        int clientDice = GenerateRandomDiceRoll();
        byte[] salt = GenerateSalt();
        byte[] hash = GenerateCommitmentHash(clientDice, salt);
        int serverDice;

        var clientStartInfo = new ProcessStartInfo() {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            FileName = "node",
            Arguments = "./xclient.js",
        };

        using(var client = Process.Start(clientStartInfo))
        {
            var stdIn = client.StandardOutput;
            var stdOut = client.StandardInput;

            string str;

            str = WaitUntil(stdIn, "client connected authorized");
            stdOut.WriteLine($"HASH={Encode(hash)}");

            str = WaitUntil(stdIn, "PLAIN="); Console.WriteLine(str);
            serverDice = int.Parse(str.Replace("PLAIN=", ""));

            stdOut.WriteLine($"SALT={Encode(salt)}");
            stdOut.WriteLine($"CLIENTDICE={clientDice}");

            var roll = ComputeDiceRoll(clientDice, serverDice);
            Console.WriteLine($"The dice roll is: {roll}");
        }
    }

    private static string WaitUntil(StreamReader stdIn, string startsWith)
    {
        string str;
        while ((str = stdIn.ReadLine()) != null)
            if (str.StartsWith(startsWith))
                break;
        return str;
    }
}
