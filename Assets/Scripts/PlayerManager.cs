using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public bool[] Completition;

    int current;

    void UpdateCompletition()
    {
        Completition[current] = true;
        current++;
    }

    public bool IsComplete()
    {
        bool output = true;
        for (int i = 0; i < Completition.Length; i++)
        {
            output = output && Completition[i];
        }
        return output;
    }
}
