using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private GameObject StartPanel;
    private GameObject TimerPanel;
    private GameObject ClearPanel;
    private GameObject DeadPanel;
    private GameObject HpPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
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
        HpPanel = GameObject.Find("HpPanel");

        TimerPanel.SetActive(false);
        ClearPanel.SetActive(false);
        DeadPanel.SetActive(false);
        HpPanel.SetActive(false);

        GameManager.Instance.OnStageClear += SetClearPanel;
        GameManager.Instance.OnGameOver += SetDeadPanel;
    }

    public void SetStartPanel()
    {
        GameManager.Instance.GameStart();
        StartPanel.SetActive(false);
        TimerPanel.SetActive(true);
        HpPanel.SetActive(true);
    }

    public void SetClearPanel()
    {
        ClearPanel.SetActive(true);
        TimerPanel.SetActive(false);
        HpPanel.SetActive(false);
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
