using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool isGameOn = false;
    [SerializeField] private int CountDown;
    [SerializeField] private GameObject startPannel;
    [SerializeField] private TextMeshProUGUI countDownText;
    private void Awake()
    {
       instance = this;
        PauseGame();
    }
    void Start()
    {
        countDownText.text = CountDown.ToString();
        StartCoroutine(DoCountDown());
    }
    private IEnumerator DoCountDown()
    { 
        yield return new WaitForSeconds(1);
        CountDown--;
        countDownText.text = CountDown.ToString();
        if (CountDown > 0)
        {
            StartCoroutine(DoCountDown());
        }
        else
        { 
            startPannel.SetActive(false);
            ResumeGame();
        }
    }
    public void PauseGame()
    {
        isGameOn = false;
    }
    public void ResumeGame()
    { 
        isGameOn = true;
    }
}
