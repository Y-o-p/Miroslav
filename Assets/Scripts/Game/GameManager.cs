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
    private static AudioSource play_death_sound;

    //public Text objective;
    //public Text health;

    public static bool game_over = false;
    public static bool is_winner = false;
    public static int lives = 3;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Manager started");
        //UpdateHUD();
        //Hides pannels
        //lose_panel.SetActive(false);
        //win_panel.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (game_over)
        {
            SceneManager.LoadScene("Game Over");
        }
        if (is_winner)
        {
            SceneManager.LoadScene("Win Scene");
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
