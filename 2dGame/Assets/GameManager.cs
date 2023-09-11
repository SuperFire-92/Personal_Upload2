using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameStarted = false;
    private GameObject[] walls;
    public GameObject wallStorage;
    public GameObject player;
    public GameObject menu;
    public GameObject enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        walls = wallStorage.GetComponent<AllWalls>().walls;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameStarted = true;
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<Walls>().StartGame();
        }
        player.GetComponent<Player>().StartGame();
        menu.GetComponent<MainMenu>().StartGame();
        enemySpawner.GetComponent<EnemySpawner>().StartGame();
    }
}