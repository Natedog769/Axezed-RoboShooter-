using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventManager : MonoBehaviour {

    //so this will hopefully manage my ui with out needing to connect things with the inspector. the ui will listen for events and change the ui
    // the idea here is to free the player from any UI spagetti stretching.
    [HideInInspector]
    public GameObject player;

    //public Canvas startScreen;
    public Canvas playScreen;
    public Canvas bossColorScreen;
    public Canvas pauseScreen;
    public Canvas winScreen;
    public Canvas endScreen;
    public HealthBattery playersBattery;
    public Text xPUILabel;
    public Text greenButtonLabel;
    public Text redButtonLabel;


    public int xPEarned;
    public static bool GameIsPause = false;


    public enum StateGame { lose = 0, win = 1, play = 2, pause = 3 }
    public StateGame gameState;


    void Start () {
        player = FindObjectOfType<TopDownControlls>().gameObject;
        EnemyTank.EnemyDiedEvent += OnEnemyDied;
        ObjectiveScript.CompleteObjectiveEvent += OnObjectComplete;
        ColoredBossScript.CompleteObjective += OnBossDefeat;
    }
	
	// Update is called once per frame
	void Update () {
        xPUILabel.text = "XP: " + xPEarned.ToString();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

	}

    public void Resume()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void Pause()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

   


    //start events

    void OnBossDefeat(ColoredBossScript bossDeth)
    {
        //gameState = StateGame.win;
    }

    void OnObjectComplete(ObjectiveScript targetThatFinished)
    {
       // gameState = StateGame.win;
    }

    void OnEnemyDied(EnemyTank enemyThatDied)
    {
        xPEarned += enemyThatDied.xPValue;
    }

    //end events

    //void GameCheck()
    //{
    //    if (gameState == StateGame.play)
    //    {
    //        playScreen.gameObject.SetActive(true);
    //        winScreen.gameObject.SetActive(false);
    //        endScreen.gameObject.SetActive(false);
    //    }
    //    else if (gameState == StateGame.pause)
    //    {

    //    }
    //    else if (gameState == StateGame.win)
    //    {
    //        winScreen.gameObject.SetActive(true);
    //    }
    //    else if (gameState == StateGame.lose)
    //    {
    //        endScreen.gameObject.SetActive(true);
    //    }
    //}

}
