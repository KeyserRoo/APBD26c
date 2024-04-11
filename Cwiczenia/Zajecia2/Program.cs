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

System.Console.WriteLine(GetMax([1, 2, 3, 4]));
System.Console.WriteLine(GetAvg([1, 2, 3, 4]));