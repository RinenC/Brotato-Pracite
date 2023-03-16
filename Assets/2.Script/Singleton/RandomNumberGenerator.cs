using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumberGenerator : MonoSingleton<RandomNumberGenerator>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int RNGCount(int count)
    {
        int rand = Random.Range(1, count + 1);
        return rand;
    }

    public int RNGPercent()
    {
        int rand = Random.Range(1, 101);
        return rand;
    }
}
