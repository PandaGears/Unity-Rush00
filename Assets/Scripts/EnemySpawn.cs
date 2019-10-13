using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public Text enemydeets;
    public GameObject enemy;
    public Transform[] spawnSpots;
    private float timeBetSpawns = 0F;


private int enemyCount = 0;
public int maxEnemyCount = 8;
    void Start()
    {
         InvokeRepeating("Spawn", 0, timeBetSpawns);
    }

    void Update()
    {
        enemydeets.text = enemyCount + " Enemies left!";
    }
    void Spawn(){
        int spawnPos = 0;
        int maxPos = spawnSpots.Length;
        while (spawnPos != maxPos){
        Instantiate(enemy, spawnSpots[spawnPos].position, Quaternion.identity);
        spawnPos++;
        enemyCount++;
        if(enemyCount>=maxEnemyCount){
          CancelInvoke("Spawn");
        }
        }
    }
}