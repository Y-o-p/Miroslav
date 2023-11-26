using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Scene currentScene;

    public GameObject lose_panel;        
    //public GameObject win_panel; 
    //public GameObject gameOverPanel;

    public AudioSource title_song;          
    public AudioSource win_song;           
    public AudioSource lose_song;           
    public AudioSource battle_song;          
    public AudioSource boss_song;
    public AudioSource play_death_sound;

    //public Text objective;
    //public Text health;

    private bool game_over = false;
    private bool is_winner = false;

    // when awake ensure there is only one game manager called when spawned
    private void Awake()
    {
        // Ensure that only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    void update_song()
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
    public void player_die()
    {
        play_death_sound.Play();
        lose_panel.SetActive(true);
        game_over = true;
        update_song();
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
