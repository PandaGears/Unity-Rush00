// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WeaponSpawn : MonoBehaviour
// {
//     public GameObject[] weapons;
//     public Transform[] spawnSpots;
//     private float timeBetSpawns = 0F;


// private int weapCount = 0;
// public int maxweapCount = 8;
//     void Start()
//     {
//          InvokeRepeating("Spawn", 0, timeBetSpawns);
//     }

//     void Update()
//     {
//     }
//     void Spawn(){
//         int spawnPos = 0;
//         int randweap = Random.Range(0, weapons.Length);
//         int maxPos = spawnSpots.Length;
//         while (spawnPos != maxPos){
//         Instantiate(randweap, spawnSpots[spawnPos].position, Quaternion.identity);
//         spawnPos++;
//         weapCount++;
//         if(weapCount>=maxweapCount){
//           CancelInvoke("Spawn");
//         }
//         }
//     }
// }