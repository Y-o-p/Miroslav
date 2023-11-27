using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Scene currentScene;

    private static GameObject lose_panel;
    //public GameObject win_panel; 
    //public GameObject gameOverPanel;
    public static int score = 0;
    public static Vector2 respawnPoint;

    private static AudioSource title_song;
    private static AudioSource win_song;
    private static AudioSource lose_song;
    private static AudioSource battle_song;
    private static AudioSource boss_song;
    private static AudioSource play_death_sound;

    //public Text objective;
    //public Text health;

    public static bool game_over = false;
    public static bool is_winner = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Manager started");
        CheckActiveScene();
        //UpdateHUD();
        update_song();
        //Hides pannels
        //lose_panel.SetActive(false);
        //win_panel.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void update_song()
    {
        title_song.Stop();
        battle_song.Stop();
        boss_song.Stop();
        win_song.Stop();
        lose_song.Stop();

        if (currentScene.name == "Title"&&!game_over)
        {
            Debug.Log(currentScene.name+" Music ");
            title_song.Play();
        }else if (currentScene.name == "Game" && !game_over)
        {
            Debug.Log(currentScene.name + " Music ");
            battle_song.Play();
        }
        else if (currentScene.name == "Boss" && !game_over)
        {
            Debug.Log(currentScene.name + " Music ");
            boss_song.Play();
        }
        else if (game_over && is_winner)
        {
            win_song.Play();
        }
        else if (game_over && !is_winner)
        {
            lose_song.Play();
        }
        



    }
    // Displays current game state to hud
    private void UpdateHUD()
    {
        

    }
    void CheckActiveScene()
    {
        // Get the currently active scene
        currentScene = SceneManager.GetActiveScene();

        // Print the name of the active scene
        Debug.Log("Active Scene: " + currentScene.name);

    }
}
