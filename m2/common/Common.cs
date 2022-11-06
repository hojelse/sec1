namespace Common;
using System.Security.Cryptography;
using System.Text;

public static class HashBasedCommitments
{
  private static SHA512 sha = SHA512.Create();
  private static Random rand = new Random();

  public static void SayHi()
  {
    Console.WriteLine("hi");
  }

  private static bool ValidateCommitment(string commitmentHash, string salt, int clientDice)
  {
    return true;
  }

  public static int GenerateRandomDiceRoll()
  {
    return 1 + ((int)Math.Floor(rand.NextSingle() * 6) % 6);
  }

  public static byte[] GenerateSalt()
  {
    var size = 512/8;
    var buf = new byte[size];
    rand.NextBytes(buf);
    return buf;
  }

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

  public static string Encode(byte[] bytes)
  {
    return String.Join(',', bytes.Select(x => (int)x).ToArray());
  }

  public static byte[] Decode(string msg)
  {
    return msg.Split(',').Select(byte.Parse).ToArray();
  }

  public static int ComputeDiceRoll(int clientDice, int serverDice)
  {
    return 1 + (((clientDice-1) + (serverDice-1)) % 6);
  }
}


