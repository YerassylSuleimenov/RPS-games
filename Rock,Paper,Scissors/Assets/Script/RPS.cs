using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS : MonoBehaviour
{
    private GameData gameData;
    private LevelManager levelManager;
    public int CurrentRPS_Type;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite RockSprite;
    [SerializeField] private Sprite PaperSprite;
    [SerializeField] private Sprite ScissorsSprite;
    [SerializeField] private AudioSource soundOfBeat;
    void Start()
    {
        soundOfBeat = GetComponent<AudioSource>();
        levelManager = LevelManager.instance;
        gameData = GameData.instance;
        CurrentRPS_Type = Random.Range(0, 3);
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitializeSprite();
    }
    private void InitializeSprite()
    {
        if (CurrentRPS_Type == 0)
        {
            spriteRenderer.sprite = RockSprite;
            gameData.AddRock(transform);
        }
        else if (CurrentRPS_Type == 1)
        {
            spriteRenderer.sprite = PaperSprite;
            gameData.AddPaper(transform);
        }
        else
        { 
            spriteRenderer.sprite = ScissorsSprite;
            gameData.AddScissors(transform);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    { 
        if (levelManager.isGameOn)
        {
            int NewCurrentRPS_Type = other.GetComponent<RPS>().CurrentRPS_Type;

            if (NewCurrentRPS_Type != CurrentRPS_Type)
            {
                ChangeSprite(NewCurrentRPS_Type);
                gameData.RemoveTargets();
            }
        }
    }
    private void ChangeSprite(int NewCurrentRPS_Type)
    {
        if (CurrentRPS_Type == 0)
        {
            if (NewCurrentRPS_Type == 1)
            {

                soundOfBeat.Play();
                spriteRenderer.sprite = PaperSprite;
                CurrentRPS_Type = NewCurrentRPS_Type;

                gameData.RemoveRock(transform);
                gameData.AddPaper(transform);
            }
        }
        else if (CurrentRPS_Type == 1)
        {
            if (NewCurrentRPS_Type == 2)
            {

                soundOfBeat.Play();
                spriteRenderer.sprite = ScissorsSprite;
                CurrentRPS_Type = NewCurrentRPS_Type;

                gameData.RemovePaper(transform);
                gameData.AddScissors(transform);
            }
        }
        else
        {
            if (NewCurrentRPS_Type == 0)
            {
                soundOfBeat.Play();
                spriteRenderer.sprite = RockSprite;
                CurrentRPS_Type = NewCurrentRPS_Type;

                gameData.RemoveScissors(transform);
                gameData.AddRock(transform);
            }
        }
    }

}
