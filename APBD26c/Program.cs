Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World!");

int GetMax(int[] ints)
{
  int max = 0;
  foreach (int item in ints)
  {
    if (max < item) max = item;
  }
  return max;
}