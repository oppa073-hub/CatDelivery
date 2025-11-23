using UnityEngine;

public enum GameState
{
    Ready,      // 준비중 (Title 화면)
    Playing,    // 게임 진행중
    Clear,      // 목적지 도착
    GameOver    // 사망 또는 시간초과
}