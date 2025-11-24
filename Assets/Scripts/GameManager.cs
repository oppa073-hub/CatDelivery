using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; }
    public float limitTime = 60f;
    private float currentTime;


    public event Action OnGameOver;
    public event Action OnStageClear;
    public event Action<float> OnTimeChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 🔹 GameManager는 계속 산다
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (State != GameState.Playing) return;  //플레이 상태가 아니면 시간이 멈춤

        currentTime -= Time.deltaTime;
        OnTimeChanged?.Invoke(currentTime);
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            GameOver();
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 여기서 "그 씬의 초기상태"로 되돌리기
        State = GameState.Ready;
        currentTime = limitTime;
        Time.timeScale = 0f;

        // 타이머 UI 초기화해주고 싶으면
        OnTimeChanged?.Invoke(currentTime);
    }
    public void GameStart()
    {
        if (State != GameState.Ready) return;

        State = GameState.Playing;
        currentTime = limitTime;  // 필요하면 시작 시점에 타이머 리셋
        Time.timeScale = 1f;      // 🔹 게임 재생
        // 여기서 타이틀 패널 끄고 HUD 켜는 것도 가능
    }
    
    public void GameOver()
    {
        State = GameState.GameOver;
        OnGameOver?.Invoke();  // UI, 사운드 등 자동 반응
        // UI 띄우기, 재시작 버튼 등
    }
  
    public void StageClear()
    {
        if (State != GameState.Playing) return; 
        State = GameState.Clear;
        Time.timeScale = 0f;
        OnStageClear?.Invoke();
        // 클리어 UI 등 }
        // 예: 2초 뒤 다음 스테이지 로드
        // StartCoroutine(NextStageCoroutine());
    }
}
