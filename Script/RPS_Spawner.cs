using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class RPS_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject rpsPrefab;
    [SerializeField] private GameObject menu;
    private LevelManager level;
    private List<GameObject> rps_Object;
    private bool Game = true;
    private enum SpawnType {Random,Even,Numbered};
    [SerializeField] private SpawnType spawnType;
    [SerializeField] int spawnNumber;
    [SerializeField] int rockNumber;
    [SerializeField] int paperNumber;
    [SerializeField] int scissorsNumber;
    private float upBoarder;
    private float downBoarder;
    private float leftBoarder;
    private float rightBoarder;
    void Start()
    {
        level = LevelManager.instance;
        rps_Object= new List<GameObject>();
        CalculateBoarder();
        SpawnRPS();
        
    }
    void Update()
    {
        if (level.isGameOn && Game) {
            menu.SetActive(true);
            for (int i = 0; i < rps_Object.Count; i++) 
            {
                rps_Object[i].SetActive(true);
            }
            Game= false;
        } 
    }
    private void SpawnRPS()
    {
        switch (spawnType)
        {
            case SpawnType.Random:
                for (int i = 0; i < spawnNumber; i++)
                {
                    GameObject instRPS = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity,transform);
                    instRPS.GetComponent<RPS>().CurrentRPS_Type = Random.Range(0, 3);
                    instRPS.SetActive(false);
                    rps_Object.Add(instRPS);
                    
                }
                break;
            case SpawnType.Even:
                int firstSpawn = (int)Mathf.Ceil((float)spawnNumber / 3);
                int secondSpawn = (int)Mathf.Ceil(((float)spawnNumber / 3) * 2);
                for (int i = 1; i <= spawnNumber; i++) 
                {
                    GameObject instRPS = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity, transform);
                    if (i <= firstSpawn)
                    {
                        instRPS.GetComponent<RPS>().CurrentRPS_Type = 0;
                    }
                    else if (i > firstSpawn && i <= secondSpawn)
                    {
                        instRPS.GetComponent<RPS>().CurrentRPS_Type = 1;
                    }
                    else
                        instRPS.GetComponent<RPS>().CurrentRPS_Type = 2;
                }
                break;
            case SpawnType.Numbered:
                for (int i = 1; i <= paperNumber; i++)
                {
                    GameObject instRPS = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity, transform);
                    instRPS.GetComponent<RPS>().CurrentRPS_Type = 0;
                }
                for (int i = 1; i <= rockNumber; i++)
                {
                    GameObject instRPS = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity, transform);
                    instRPS.GetComponent<RPS>().CurrentRPS_Type = 1;
                }
                for (int i = 1; i <= scissorsNumber; i++)
                {
                    GameObject instRPS = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity, transform);
                    instRPS.GetComponent<RPS>().CurrentRPS_Type = 2;
                }
                break;
        }
    }
    private Vector2 FindSpawnPoint() 
    {
        Vector2 position;
        position = new Vector2(Random.Range(leftBoarder,rightBoarder),Random.Range(downBoarder,upBoarder));
        return position;
    }
    private void CalculateBoarder()
    {
        Vector3 boarder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


        upBoarder = boarder.y;
        downBoarder = -boarder.y;
        leftBoarder = -boarder.x;
        rightBoarder = boarder.x;
    }
}
