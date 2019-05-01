using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour {

    //this is for the fade affect
    public Animator anim;

    int levelToLoad;

    public int mainMenu;
    public int gameAZStart;
    public int game2Start;
    Scene currentScreen;
    //and so on


    public void Start()
    {
       
        currentScreen = SceneManager.GetActiveScene();
        if (currentScreen.buildIndex == 0) FindObjectOfType<AudioManager>().Play("Intro");
    }

    public void Update()
    {
       
    }

    public void LoadNextLevel()
    {
        levelToLoad = currentScreen.buildIndex + 1;
        anim.SetTrigger("FadeOut");
    }

    public void ReloadLevel()
    { //this will find the GM and then reset some of the stats when it specifically reloads
        GameManager GM;
        GM = FindObjectOfType<GameManager>();
        GM.dualWieldWeapons = false;
        GM.playerArmorState = GameManager.StateArmor.light;
        GM.playerRHGunState = GameManager.StateGun.lvl1;

        levelToLoad = currentScreen.buildIndex;
        anim.SetTrigger("FadeOut");
    }

    //for each level we have we need a method
    public void LoadGame1()
    {
        levelToLoad = gameAZStart; //the level to load;
        anim.SetTrigger("FadeOut");
    }

    public void LoadGame2()
    {
        levelToLoad = game2Start; //the level to load;
        anim.SetTrigger("FadeOut");
    }


    public void QuitToMenu()
    {
        levelToLoad = mainMenu;
        anim.SetTrigger("FadeOut");
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Ejected");
        Application.Quit();
    }


    public void FadeToLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelToLoad);
    }


}
