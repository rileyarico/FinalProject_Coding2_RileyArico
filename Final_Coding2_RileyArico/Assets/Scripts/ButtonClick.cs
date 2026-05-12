using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void NextScene(string sceneName)
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(sceneName);
    }
}
