using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Pause menu hit");
            ToggleVisibility();
        }
    }

    public void ToggleVisibility()
    {
        if (pausePanel.activeInHierarchy == true)
        {
            Continue();
            return;
        }
        if (pausePanel.activeInHierarchy == false)
        {
            Pause();
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
