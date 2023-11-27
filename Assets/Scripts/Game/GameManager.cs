using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Scene currentScene;
    private static GameObject lose_panel;
    private static AudioSource play_death_sound;

    public static bool game_over = false;
    public static bool is_winner = false;
    public static int lives = 3;
    public static int score = 0;
    public static bool player_in_arena = false;
    public static bool player_alive = true;
    public static Vector2 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Manager started");
    }

    public static void Reset() {
        game_over = false;
        is_winner = false;
        player_in_arena = false;
        player_alive = true;
        lives = 3;
        score = 0;
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
