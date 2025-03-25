using Akasha;
using System.Collections.Generic;

public static class RxListExtensions
{
    public static List<T> ToList<T>(this RxList<T> rxList)
    {
        var result = new List<T>();
        foreach (var item in rxList)
            result.Add(item);
        return result;
    }
}
