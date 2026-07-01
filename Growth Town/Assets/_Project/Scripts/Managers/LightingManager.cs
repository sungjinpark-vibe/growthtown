using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeTown.Managers;
public class LightingManager : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float timeScale = 10f; // Real seconds per in-game hour
    [SerializeField] private float currentTime = 12f; // Start at 12:00 PM (Noon)

    [Header("Audio Keys")]
    [SerializeField] private string dayBGMKey = "DayBGM";
    [SerializeField] private string nightBGMKey = "NightBGM";

    private bool isNight = false;

    private void Start()
    {
        // Initial BGM setup based on start time
        if (currentTime >= 18f || currentTime < 6f)
        {
            isNight = true;
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayBGM(nightBGMKey);
        }
        else
        {
            isNight = false;
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayBGM(dayBGMKey);
        }
    }

    private void Update()
    {
        UpdateTime();
        CheckTimeTransitions();
    }

    private void UpdateTime()
    {
        // Advance time
        currentTime += (Time.deltaTime / timeScale);
        if (currentTime >= 24f)
        {
            currentTime %= 24f; // Reset to next day
        }
    }

    private void CheckTimeTransitions()
    {
        // Day to Night transition (18:00)
        if (!isNight && (currentTime >= 18f || currentTime < 6f))
        {
            isNight = true;
            TriggerNightTransition();
        }
        // Night to Day transition (06:00)
        else if (isNight && currentTime >= 6f && currentTime < 18f)
        {
            isNight = false;
            TriggerDayTransition();
        }
    }

    private void TriggerNightTransition()
    {
        Debug.Log("Transitioning to Night...");
        // Change visual lighting settings here...
        
        // Play acoustic night BGM
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM(nightBGMKey);
        }
    }

    private void TriggerDayTransition()
    {
        Debug.Log("Transitioning to Day...");
        // Change visual lighting settings here...
        
        // Play acoustic day BGM
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM(dayBGMKey);
        }
    }
}
