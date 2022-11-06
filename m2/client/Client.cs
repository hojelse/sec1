using System.Diagnostics;
using static Common.HashBasedCommitments;

class Client
{
  static void Main(string[] args)
  {
    int clientDice = GenerateRandomDiceRoll();
    byte[] salt = GenerateSalt();
    byte[] hash = GenerateCommitmentHash(clientDice, salt);

    using (var client = Process.Start(clientStartInfo))
    {
      var stdIn = client.StandardOutput;
      var stdOut = client.StandardInput;

      WaitUntil(stdIn, "client connected authorized");

      stdOut.WriteLine($"HASH={Encode(hash)}");

      int serverDice = int.Parse(WaitUntil(stdIn, "SERVERDICE="));

      stdOut.WriteLine($"SALT={Encode(salt)}");
      stdOut.WriteLine($"CLIENTDICE={clientDice}");

      var roll = ComputeDiceRoll(clientDice, serverDice);
      Console.WriteLine($"The dice roll is: {roll}");
    }
  }

  static ProcessStartInfo clientStartInfo = new ProcessStartInfo()
  {
    CreateNoWindow = true,
    UseShellExecute = false,
    RedirectStandardInput = true,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    FileName = "node",
    Arguments = "./xclient.js",
  };
}
