using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStart : MonoBehaviour
{
    // Detects if any key has been pressed.

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(1);
            
            Debug.Log("A key or mouse click has been detected");
        }
    }
}