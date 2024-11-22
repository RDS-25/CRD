using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumarable : MonoBehaviour
{
    public class NumberCollection : IEnumerable<int>
    {
        List<int> list = new List<int>();
        HashSet<int> set = new HashSet<int>();

        public IEnumerator<int> GetEnumerator()
        {
            foreach (int n in list)
            {
                yield return n;
            }
            foreach (int n in set)
            {
                yield return n;
            }
            yield return 10;
            yield return 11;
            yield return 12;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    IEnumerable<int> GetNumbers()
    {
        yield return 10;
        yield return 11;
        yield return 12;
    }

    IEnumerator GetNumbersByEnumberator()
    {
        yield return 20;
        yield return 21;
        yield return 22;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var number in GetNumbers())
        {
            Debug.Log(number);
        }

        IEnumerator enumerator = GetNumbersByEnumberator();
        while (enumerator.MoveNext())
        {
            Debug.Log(enumerator.Current);
        }

        NumberCollection num = new NumberCollection();
        foreach (var number in num)
        {
            Debug.Log(number);
        }

    }

}
