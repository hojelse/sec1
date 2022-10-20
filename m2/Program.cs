using System.Security.Cryptography;
using System.Text;

class Program
{
  static void Main(string[] args)
  {

  }
}

class HashBasedCommitments
{
  HashAlgorithm sha = SHA512.Create();
  Random random = new Random();
  
  int ranSize = 1024;
  int numSize = 8;

  public HashBasedCommitments()
  {}

  public byte[] Commitment(byte[] num)
  {
    var rand = new byte[ranSize];
    random.NextBytes(rand);
    return Com(num, rand);
  }

  public byte[] Com(byte[] num, byte[] rand)
  {
    if (num.Length != numSize)
      throw new Exception("");

    var bytes = rand.Concat(num).ToArray();
    var commitment = sha.ComputeHash(bytes);
    return commitment;
  }
}