using System.Diagnostics;

class Client
{
    static void Main(string[] args)
    {
        var clientDice = 4;
        var prefix = "f1a";
        var hash = "1f3";
        int serverDice;

        var clientStartInfo = new ProcessStartInfo()
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            FileName = "node",
            Arguments = "./xclient.js"
        };

        using(var client = Process.Start(clientStartInfo))
        {
            var stdIn = client.StandardOutput;
            var stdOut = client.StandardInput;

            string str;

            str = WaitUntil(stdIn, "client connected authorized");
            stdOut.WriteLine($"HASH={hash}");

            str = WaitUntil(stdIn, "PLAIN="); Console.WriteLine(str);
            serverDice = int.Parse(str.Replace("PLAIN=", ""));

            stdOut.WriteLine($"PREFIX={prefix}");
            stdOut.WriteLine($"CLIENTDICE={clientDice}");

            Console.WriteLine($"client: {clientDice}, server: {serverDice}");

            WaitUntil(stdIn, "#never");
        }
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