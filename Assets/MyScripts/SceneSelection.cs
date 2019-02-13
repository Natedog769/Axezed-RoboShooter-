using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour {

    //this is for the fade affect
    public Animator anim;

    int levelToLoad;

    public int mainMenu;
    public int game1;
    Scene currentScreen;
    //and so on


    public void Start()
    {

        currentScreen = SceneManager.GetActiveScene();
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
        levelToLoad = game1; //the level to load;
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
