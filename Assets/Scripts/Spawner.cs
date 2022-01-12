using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] spawners;
    public float enemyCount;
    public float delayTime;
    private int RNG;
   
    void Start() 
    {
        enemyCount = 0;
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        if(enemyCount <= 50)
        {
            RNG = Random.Range(0, spawners.Length);
            GameObject.Instantiate(enemy, spawners[RNG].transform.position, spawners[RNG].transform.rotation);
            enemyCount ++;
        }

        yield return new WaitForSeconds(delayTime);
        StartCoroutine(EnemySpawn());
    }
}
