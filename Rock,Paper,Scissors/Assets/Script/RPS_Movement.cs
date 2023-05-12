using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_Movement : MonoBehaviour
{
    private Vector2 newPosition;
    private GameData gameData;
    private LevelManager levelManager; 

    public bool isTargetLocked;


    [SerializeField] private float upBoarder;
    [SerializeField] private float downBoarder;
    [SerializeField] private float leftBoarder;
    [SerializeField] private float rightBoarder;
    [SerializeField] Vector3 targetLocation;
    [SerializeField] Transform enemyTransform;
    private RPS rps;
    void Start()
    { 
        gameData=  GameData.instance;
        levelManager = LevelManager.instance;
        rps = GetComponent<RPS>();
        gameData.addRpsMovement(this);
        newPosition = transform.position;
        CalculateBoarder();
    }
    void Update()
    {
       if (levelManager.isGameOn)
        {
            if (!isTargetLocked)
                FindNewTarget();
            MoveAround();
        }
    }
    private void MoveAround()
    {
        if (isTargetLocked)
        {
            targetLocation = enemyTransform.position - transform.position;


            if (targetLocation.x > 0)
            {
                newPosition.x = newPosition.x + (float)Random.Range(0, 2) / 35;
            }
            else
            {
                newPosition.x = newPosition.x + +(float)Random.Range(-1,1) / 35;
            }

            if (targetLocation. y > 0)
            {
                newPosition.y = newPosition.y + (float)Random.Range(0,2)/35;
            }
            else
            {
                newPosition.y = newPosition.y + (float)Random.Range(-1, 1) / 35;
            }
        }
        else
        {
            newPosition += new Vector2((float)Random.Range(-1, 2) / 35, (float)Random.Range(-1, 2) / 35);
            newPosition = new Vector2(Mathf.Clamp(newPosition.x, leftBoarder, rightBoarder), Mathf.Clamp(newPosition.y, downBoarder, upBoarder));
        }

        transform.position = newPosition;

    }
    private void CalculateBoarder()
    {
        Vector3 boarder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));


        upBoarder = boarder.y;
        downBoarder = -boarder.y;
        leftBoarder = -boarder.x;
        rightBoarder = boarder.x;
    }
    private void FindNewTarget()
    {
        if (rps.CurrentRPS_Type == 0)
        {
            enemyTransform = gameData.FindScissors(transform.position);
            if (enemyTransform != null)
            {
                isTargetLocked = true;
            }
            else
                isTargetLocked = false;
        }
        else if (rps.CurrentRPS_Type == 1)
        {
            enemyTransform = gameData.FindRock(transform.position);
            if (enemyTransform != null)
            {
                isTargetLocked = true;
            }
            else
                isTargetLocked = false;
        }
        else
        {

            if (rps.CurrentRPS_Type == 0)
            {
                enemyTransform = gameData.FindScissors(transform.position);
                if (enemyTransform != null)
                {
                    isTargetLocked = true;
                }
                else
                    isTargetLocked = false;
            }
            else if (rps.CurrentRPS_Type == 1)
            {
                enemyTransform = gameData.FindRock(transform.position);
                if (enemyTransform != null)
                {
                    isTargetLocked = true;
                }
                else
                    isTargetLocked = false;
            }
            else
            {
                enemyTransform = gameData.FindPaper(transform.position);
                if (enemyTransform != null)
                {
                    isTargetLocked = true;
                }
                else
                    isTargetLocked = false;
            }    
        }
       
    }
    
}
