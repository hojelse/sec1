using System;
using System.Diagnostics;
using System.IO;

class Server
{
    static void Main(string[] args)
    {
        var serverDice = 3;
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
            var commitmentHash = str.Replace("HASH=", "");
            stdOut.WriteLine($"PLAIN={serverDice}");

            str = WaitUntil(stdIn, "PREFIX="); Console.WriteLine(str);
            var prefix = str.Replace("PREFIX=", "");
            str = WaitUntil(stdIn, "CLIENTDICE="); Console.WriteLine(str);
            clientDice = int.Parse(str.Replace("CLIENTDICE=", ""));

            bool isValid = ValidateCommitment(commitmentHash, prefix, clientDice);
            if (!isValid) {
                Console.WriteLine("Client cheated");
                return;
            }

            Console.WriteLine($"client: {clientDice}, server: {serverDice}");

            WaitUntil(stdIn, "#never");
        }
    }

  private static bool ValidateCommitment(string commitmentHash, string prefix, int clientDice)
  {
    return true;
  }

  private static string WaitUntil(StreamReader stdIn, string startsWith)
    {
        string str;
        while ((str = stdIn.ReadLine()) != null)
        {
            if (str.StartsWith(startsWith)) break;
        }
        return str;
    }
}