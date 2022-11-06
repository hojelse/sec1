namespace Common;
using System.Security.Cryptography;

public static class HashBasedCommitments
{
  private static SHA512 sha = SHA512.Create();
  private static Random rand = new Random();

  // Generate a random number in range [1;6]
  public static int GenerateRandomDiceRoll()
  {
    return 1 + ((int)Math.Floor(rand.NextSingle() * 6) % 6);
  }

  // Generate a 512bit byte array
  public static byte[] GenerateSalt()
  {
    var sizeInBytes = 512/8;
    var buf = new byte[sizeInBytes];
    rand.NextBytes(buf);
    return buf;
  }

  // Concatenate salt and dice, and the hashing to a 512bit byte array
  public static byte[] GenerateCommitmentHash(int dice, byte[] salt)
  {
    var l = salt.Length;

    // clone byte array
    var con = new byte[l+1];
    for (int i = 0; i < l; i++)
      con[i] = salt[i];

    // append dice
    con[l] = (byte)dice;

    var hash = sha.ComputeHash(salt);

    return hash;
  }

  // Encode a byte array to a comma seperated string of decimals
  public static string Encode(byte[] bytes)
  {
    return String.Join(',', bytes.Select(x => (int)x).ToArray());
  }

  // Decode a comma seperated string of decimals to a byte array
  public static byte[] Decode(string msg)
  {
    return msg.Split(',').Select(byte.Parse).ToArray();
  }

  // Combine two dice rolls from each party into one diceroll
  public static int ComputeDiceRoll(int clientDice, int serverDice)
  {
    return 1 + (((clientDice-1) + (serverDice-1)) % 6);
  }

  // Compare an excisting hash to a new hash from a salt and a dice roll
  public static bool MatchesCommitment(byte[] commitmentHash, byte[] salt, int clientDice)
  {
    return Encode(commitmentHash) == Encode(GenerateCommitmentHash(clientDice, salt));
  }

  public static string WaitUntil(StreamReader stdIn, string startsWith)
  {
    string str;
    while ((str = stdIn.ReadLine()) != null)
      if (str.StartsWith(startsWith))
        break;
    Console.WriteLine(str);
    return str.Replace(startsWith, "");
  }
}


