using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class ShuffleableList<T> : List<T>
{

    public ShuffleableList(List<T> other)
    {
        foreach (T item in other)
        {
            this.Add(item);
        }
    }
    public void Shuffle()
    {
        List<T> newList = new List<T>(this);
        for (int i = 0; i < Count - 1; i++)
        {
            T temp = newList[i];
            int randIndex = Random.Range(i, Count);
            newList[i] = newList[randIndex];
            newList[randIndex] = temp;
        }

        Clear();
        AddRange(newList);
    }

}
