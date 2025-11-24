using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class StartUI : MonoBehaviour
{
    private bool started = false;

    private void Update()
    {
        if (started) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.State != GameState.Ready) return;

        bool anyKey = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;
        bool mouseClick = Mouse.current != null &&
                         (Mouse.current.leftButton.wasPressedThisFrame ||
                          Mouse.current.rightButton.wasPressedThisFrame);
        if (anyKey || mouseClick)
        {
            started = true;
            UIManager.Instance.SetStartPanel();
        }
            
        
    }
}
