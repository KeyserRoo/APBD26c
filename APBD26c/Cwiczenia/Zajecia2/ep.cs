namespace Zajecia2
{
  class EntryPoint
  {
    public static void EP()
    {
      Console.WriteLine("Hello, World!");
      Console.WriteLine("Hello, World!");
      Console.WriteLine(GetMax([1, 2, 3, 4, 5, 6]));
      Console.WriteLine(GetAvg([1, 2, 3, 4, 5, 6]));

      int GetMax(int[] ints)
      {
        int max = 0;
        foreach (int item in ints)
        {
          if (max < item) max = item;
        }
        return max;
      }

      float GetAvg(int[] ints)
      {
        float toRet = 0;
        foreach (int item in ints)
        {
          toRet += item;
        }
        toRet /= ints.Length;
        return toRet;
      }
    }
  }
}