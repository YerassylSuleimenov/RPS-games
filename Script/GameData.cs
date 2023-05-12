using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    private bool wins = true;
    [SerializeField]private GameObject WinsWindow;
    [SerializeField]private GameObject Wins;
    private Image w;
    [SerializeField] private List<Transform> rockTransforms;
    [SerializeField] private List<Transform> paperTransforms;
    [SerializeField] private List<Transform> scissorsTransforms;
    [SerializeField] private List<RPS_Movement> rpsMovemets;
    [SerializeField] private Sprite Rock;
    [SerializeField] private Sprite Paper;
    [SerializeField] private Sprite Scissors;


    private void Awake()
    {
        w = Wins.GetComponent<Image>();
        instance = this;
        Application.targetFrameRate = 30;
    }
    void Update()
    {
        if (((rockTransforms.Count == 0 && paperTransforms.Count == 0 && scissorsTransforms.Count != 0) || (rockTransforms.Count != 0 && paperTransforms.Count == 0 && scissorsTransforms.Count == 0) || (rockTransforms.Count == 0 && paperTransforms.Count != 0 && scissorsTransforms.Count == 0)) && wins)
        {
            wins= false;
            WinsWindow.SetActive(true);
            if (rockTransforms.Count > 0)
                w.sprite = Rock;
            else if (paperTransforms.Count > 0)
                w.sprite = Paper;
            else
                w.sprite = Scissors;

        }
    }
    public Transform FindRock(Vector3 currentPosition)
    {
        if (rockTransforms.Count > 0)
        {
            float enemyDistance = (rockTransforms[0].position - currentPosition).magnitude;
            Transform enemyTransform = rockTransforms[0];
            foreach (Transform t in rockTransforms)
            {
                float distance = (t.position - currentPosition).magnitude;
                if (distance < enemyDistance)
                {
                    enemyTransform = t;
                    enemyDistance= distance;
                }
            }
            return enemyTransform;
        }
        else
            return null;
    }
    public Transform FindPaper(Vector3 currentPosition)
    {
        if (paperTransforms.Count > 0)
        {
            float enemyDistance = (paperTransforms[0].position - currentPosition).magnitude;
            Transform enemyTransform = paperTransforms[0];
            foreach (Transform t in paperTransforms)
            {
                float distance = (t.position - currentPosition).magnitude;
                if (distance < enemyDistance)
                {
                    enemyTransform = t;
                    enemyDistance = distance;
                }
            }
            return enemyTransform;
        }
        else
            return null;
    }
    public Transform FindScissors(Vector3 currentPosition)
    {
        if (scissorsTransforms.Count > 0)
        {
            float enemyDistance = (scissorsTransforms[0].position - currentPosition).magnitude;
            Transform enemyTransform = scissorsTransforms[0];
            foreach (Transform t in scissorsTransforms)
            {
                float distance = (t.position - currentPosition).magnitude;
                if (distance < enemyDistance)
                {
                    enemyTransform = t;
                    enemyDistance = distance;
                }
            }
            return enemyTransform;
        }
        else
            return null;
    }

    public void AddRock(Transform rock)
    { 
        rockTransforms.Add(rock);
    }
    public void AddPaper(Transform paper)
    {
        paperTransforms.Add(paper);
    }
    public void AddScissors(Transform scissors)
    {
        scissorsTransforms.Add(scissors);
    }

    public void RemoveRock(Transform removedRock)
    {
        foreach (Transform t in rockTransforms)
        {
            if (t == removedRock) 
            {
                rockTransforms.Remove(t);
                break;
            }
        }
    }
    public void RemovePaper(Transform removedPaper)
    {
        foreach (Transform t in paperTransforms)
        {
            if (t == removedPaper)
            {
                paperTransforms.Remove(t);
                break;
            }
        }
    }
    public void RemoveScissors(Transform removedScissors)
    {
        foreach (Transform t in scissorsTransforms)
        {
            if (t == removedScissors)
            {
                scissorsTransforms.Remove(t); 
                break;
            }
        }
    }
    public void RemoveTargets()
    {
        foreach (RPS_Movement rpsmv in rpsMovemets)
        { 
            rpsmv.isTargetLocked = false;
        }
    }
    public void addRpsMovement(RPS_Movement rps_Movement)
    { 
        rpsMovemets.Add(rps_Movement);
    }
}
