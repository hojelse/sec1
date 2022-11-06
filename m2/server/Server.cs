using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using static Common.HashBasedCommitments;

class Server
{
    static void Main(string[] args)
    {
        var serverDice = GenerateRandomDiceRoll();
        int clientDice;

        var serverStartInfo = new ProcessStartInfo()
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            FileName = "node",
            Arguments = "./xserver.js"
        };

        using(var server = Process.Start(serverStartInfo)){
            var stdIn = server.StandardOutput;
            var stdOut = server.StandardInput;

            string str;

            str = WaitUntil(stdIn, "HASH="); Console.WriteLine(str);
            var commitmentHashEncoded = str.Replace("HASH=", "");
            var commitmentHash = Decode(commitmentHashEncoded);
            stdOut.WriteLine($"PLAIN={serverDice}");

            str = WaitUntil(stdIn, "SALT="); Console.WriteLine(str);
            var saltEncoded = str.Replace("SALT=", "");
            var salt = Decode(saltEncoded);
            str = WaitUntil(stdIn, "CLIENTDICE="); Console.WriteLine(str);
            clientDice = int.Parse(str.Replace("CLIENTDICE=", ""));

            bool isValid = ValidateCommitment(commitmentHash, salt, clientDice);
            if (!isValid) {
                Console.WriteLine("Client cheated");
                return;
            }

            var roll = ComputeDiceRoll(clientDice, serverDice);
            Console.WriteLine($"The dice roll is: {roll}");
        }
    }

    private static bool ValidateCommitment(byte[] commitmentHash, byte[] salt, int clientDice)
    {
        return Encode(commitmentHash) == Encode(GenerateCommitmentHash(clientDice, salt));
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
