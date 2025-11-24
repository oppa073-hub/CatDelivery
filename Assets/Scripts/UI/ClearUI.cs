using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ClearUI : MonoBehaviour
{
  
    public void BackBtn()
    {

    }

    public void restartBtn()
    {
        Time.timeScale = 1f; // 혹시 멈춰있을 수 있으니까
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
