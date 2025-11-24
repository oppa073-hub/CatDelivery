using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private GameObject StartPanel;
    private GameObject TimerPanel;
    private GameObject ClearPanel;


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
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartPanel = GameObject.Find("StartText");
        TimerPanel = GameObject.Find("Timer");
        ClearPanel = GameObject.Find("ClearPanel");

        TimerPanel.SetActive(false);
        ClearPanel.SetActive(false);

        GameManager.Instance.OnStageClear += SetClearPanel;
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

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStageClear -= SetClearPanel;

    }

}
