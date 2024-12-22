using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void StartGamePressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMainPressed()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Function to exit the game
    public void ExitGamePressed()
    {
        // If running in the editor, this will stop the play mode (for testing purposes)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // Exits the game
        Application.Quit();

    }

    public void ResumeGame()
    {
        GameManager.instance.Unpause();
    }
}
