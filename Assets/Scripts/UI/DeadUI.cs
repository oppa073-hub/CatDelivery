using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeadUI : MonoBehaviour
{
    private bool started = false;

    private void Update()
    {
        if (started) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.State != GameState.GameOver) return;

        bool anyKey = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;
        bool mouseClick = Mouse.current != null &&
                         (Mouse.current.leftButton.wasPressedThisFrame ||
                          Mouse.current.rightButton.wasPressedThisFrame);
        if (anyKey || mouseClick)
        {
            started = true;
            Time.timeScale = 1f;
            var current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
    }
}
