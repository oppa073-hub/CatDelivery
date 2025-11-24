using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private GameObject StartPanel;
    private GameObject TimerPanel;
    private GameObject ClearPanel;
    private GameObject DeadPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
      
    }

    private void Start()
    {
        StartPanel = GameObject.Find("StartText");
        TimerPanel = GameObject.Find("Timer");
        ClearPanel = GameObject.Find("ClearPanel");
        DeadPanel = GameObject.Find("DeadPanel");

        TimerPanel.SetActive(false);
        ClearPanel.SetActive(false);
        DeadPanel.SetActive(false);

        GameManager.Instance.OnStageClear += SetClearPanel;
        GameManager.Instance.OnGameOver += SetDeadPanel;
    }

    public void SetStartPanel()
    {
        GameManager.Instance.GameStart();
        StartPanel.SetActive(false);
        TimerPanel.SetActive(true);
    }

    public void SetClearPanel()
    {
        ClearPanel.SetActive(true);
        TimerPanel.SetActive(false);
    }

    public void SetDeadPanel()
    {
        DeadPanel.SetActive(true);
    }
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStageClear -= SetClearPanel;
            GameManager.Instance.OnGameOver -= SetDeadPanel;
        }
    }

}
