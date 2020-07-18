using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawn : MonoBehaviour
{
    public GameObject[] obj;                        // A public object array for which objects to spawn
    public float spawnMin = 2f;                     // min and max times for another spawn
    public float spawnMax = 1f;

    void Start()
    {
        float rand = Random.Range(0, 1000);         //get random number

        for (int i = 0; i < 10; i++)
        {
            Instantiate(obj[Random.Range(0, obj.GetLength(0))], new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        }
    }

    
}
