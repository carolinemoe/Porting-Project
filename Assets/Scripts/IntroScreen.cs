using UnityEngine;

public class IntroScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Load the next scene, which is the main game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainGameScene");
        }
    }
}
