using System;
using System.Diagnostics;
using static Common.HashBasedCommitments;

class Server
{
  static void Main(string[] args)
  {
    var serverDice = GenerateRandomDiceRoll();

    using (var server = Process.Start(serverStartInfo))
    {
      var stdIn = server.StandardOutput;
      var stdOut = server.StandardInput;

      var commitmentHash = Decode(WaitUntil(stdIn, "HASH="));
      stdOut.WriteLine($"SERVERDICE={serverDice}");

      var salt = Decode(WaitUntil(stdIn, "SALT="));
      int clientDice = int.Parse(WaitUntil(stdIn, "CLIENTDICE="));

      if (!MatchesCommitment(commitmentHash, salt, clientDice))
        throw new Exception($"Dice roll {clientDice} and salt {salt} doesn't match commitment. Client must have cheated.");

      var roll = ComputeDiceRoll(clientDice, serverDice);
      Console.WriteLine($"The dice roll is: {roll}");
    }
  }

  static ProcessStartInfo serverStartInfo = new ProcessStartInfo()
  {
    CreateNoWindow = true,
    UseShellExecute = false,
    RedirectStandardInput = true,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    FileName = "node",
    Arguments = "./server.js"
  };
}
