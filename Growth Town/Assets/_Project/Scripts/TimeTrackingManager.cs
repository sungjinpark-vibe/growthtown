using UnityEngine;
using System;

public class TimeTrackingManager : MonoBehaviour
{
    private DateTime lastPauseTime;
    
    [Header("Timer Settings")]
    public float currentTimer = 0f;
    public bool isTimerRunning = false;
    
    // 5 minutes in seconds = 300 seconds
    private const float REWARD_THRESHOLD_SECONDS = 300f; 

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// 1. 백그라운드 시간 오프셋 시뮬레이션 로직
    /// 앱이 백그라운드로 진입할 때 시간을 저장하고, 
    /// 다시 포그라운드로 복귀할 때 경과 시간을 계산하여 타이머에 더합니다.
    /// </summary>
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // 백그라운드 진입 시 현재 시간 저장
            if (isTimerRunning)
            {
                lastPauseTime = DateTime.UtcNow;
                Debug.Log($"[TimeTrackingManager] App Paused. Time saved: {lastPauseTime}");
            }
        }
        else
        {
            // 포그라운드 복귀 시 경과 시간 계산 및 반영
            if (isTimerRunning)
            {
                TimeSpan elapsedTime = DateTime.UtcNow - lastPauseTime;
                currentTimer += (float)elapsedTime.TotalSeconds;
                Debug.Log($"[TimeTrackingManager] App Resumed. Elapsed time added: {elapsedTime.TotalSeconds} seconds. Current Timer: {currentTimer}");
            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        currentTimer = 0f;
        Debug.Log("[TimeTrackingManager] Timer Started.");
    }

    /// <summary>
    /// 정상 종료 호출
    /// </summary>
    public void CompleteTimer()
    {
        ProcessTimerEnd("정상 종료");
    }

    /// <summary>
    /// 포기 호출
    /// </summary>
    public void GiveUpTimer()
    {
        ProcessTimerEnd("포기");
    }

    /// <summary>
    /// 2. 타이머가 5분을 넘어 정상 종료(또는 포기)될 경우 보상 건물을 지급하는 로직
    /// </summary>
    private void ProcessTimerEnd(string endType)
    {
        isTimerRunning = false;
        Debug.Log($"[TimeTrackingManager] 타이머 {endType}. 기록된 시간: {currentTimer}초");

        if (currentTimer >= REWARD_THRESHOLD_SECONDS)
        {
            GiveRewardBuilding();
        }
        else
        {
            Debug.Log("[TimeTrackingManager] 5분이 경과하지 않아 보상이 지급되지 않습니다.");
        }
    }

    private void GiveRewardBuilding()
    {
        // TODO: 실제 인벤토리 매니저나 보상 시스템 로직으로 대체하세요.
        Debug.Log("[TimeTrackingManager] 5분 초과 달성! 보상 건물을 인벤토리에 지급합니다.");
        
        // 예시: 
        // InventoryManager.Instance.AddItem("Reward_Building_01");
    }
}
