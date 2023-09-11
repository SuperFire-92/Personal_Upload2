using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    private List<GameObject> listOfEnemies = new();
    float timer = 0;
    private bool gameRunning = false;
    public float spawnTime;
    public int numToSpawn;
    private Vector2 pos;
    private bool wallOrFloor;

    private void Start()
    {
        //Instantiate(enemy, new Vector3(0,0,0), Quaternion.identity);
    }

    public void StartGame()
    {
        gameRunning = true;
        timer = spawnTime;
    }

    private void FixedUpdate()
    {
        if (gameRunning)
        {
            //Increase the ingame timer for counting how long until the enemy spawns
            timer += Time.deltaTime;
            
            //Once the timer hits, resset the timer, and spawn the enemy
            if (timer > spawnTime)
            {
                timer = 0;
                for (int i = 0; i < numToSpawn; i++)
                {
                    wallOrFloor = !wallOrFloor;
                    SpawnLocation();
                    GameObject curEnemy = Instantiate(enemy, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                    listOfEnemies.Add(curEnemy);
                }

                for (int i = 0; i < listOfEnemies.Count; i++)
                {
                    if (listOfEnemies[i].GetComponent<Enemy>().destroy == true)
                    {
                        Destroy(listOfEnemies[i]);
                        listOfEnemies.RemoveAt(i);
                    }
                }
            }
        }
        

        
    }
    
    //Chooses a random location to spawn the enemy
    public void SpawnLocation()
    {
        if (wallOrFloor)
        {
            if (Random.value < .5f)
            {
                pos.x = -5;
            }
            else
            {
                pos.x = 5;
            }
            pos.y = Random.Range(-3.8f, 3.8f);
        }
        else
        {
            if (Random.value < .5f)
            {
                pos.y = -4;
            }
            else
            {
                pos.y = 4;
            }
            pos.x = Random.Range(-4.8f, 4.8f);
        }
    }
}
