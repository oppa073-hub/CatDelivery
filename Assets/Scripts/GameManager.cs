using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; }
    public float limitTime = 60f;
    private float currentTime;

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
        State = GameState.Playing;
        currentTime = Time.time;
    }

    private void Update()
    {
        if (State != GameState.Playing) return;

        currentTime = Time.time;
        if (currentTime <= 0f)
        {
            currentTime = 0; OnTimeOver();
        }
    }

    public void OnTimeOver()
    {
        State = GameState.GameOver;
        // UI 띄우기, 재시작 버튼 등
    }
    public void OnPlayerDead()
    {
        if (State != GameState.Playing) return;
        State = GameState.GameOver;
        // UI 띄우기, 재시작 등
    }
    public void OnStageClear()
    {
        if (State != GameState.Playing) return; 
        State = GameState.Clear;
        // 클리어 UI 등 }

    }
}
