using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class start_button : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("Hello");
        // Load the Game Scene
        SceneManager.LoadScene("Scene_1");
    }
}
