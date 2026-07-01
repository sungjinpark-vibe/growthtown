using UnityEngine;
using UnityEngine.UI;

namespace LifeTown.UI
{
    public enum TimerMode
    {
        Circular,
        Digital
    }

    /// <summary>
    /// 원형 프로그레스 바(Circular)와 디지털 시계(Digital) 모드를 스위칭하고
    /// 타이머 기능을 관리하는 뼈대 스크립트입니다.
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        [Header("Mode Configuration")]
        [SerializeField] private TimerMode currentMode = TimerMode.Circular;
        
        [Header("Circular Mode Elements")]
        [SerializeField] private GameObject circularTimerRoot;
        [SerializeField] private Image circularProgressBar; // Image type with fill amount

        [Header("Digital Mode Elements")]
        [SerializeField] private GameObject digitalTimerRoot;
        [SerializeField] private Text digitalTimeText; // Text or TMPro.TextMeshProUGUI

        [Header("Popup References")]
        [SerializeField] private WarningPopupUI warningPopup;

        private float elapsedTime = 0f;
        private bool isTimerRunning = false;
        private const float MINIMUM_REWARD_TIME = 300f; // 5 minutes in seconds

        private void Start()
        {
            UpdateModeUI();
        }

        private void Update()
        {
            if (isTimerRunning)
            {
                elapsedTime += Time.deltaTime;
                UpdateTimerDisplay();
            }
        }

        public void StartTimer()
        {
            isTimerRunning = true;
        }

        /// <summary>
        /// 유저가 '정지' 버튼을 눌렀을 때 호출되는 메서드
        /// </summary>
        public void OnStopButtonClicked()
        {
            // 5분 미만인 경우 경고 팝업 표시
            if (elapsedTime < MINIMUM_REWARD_TIME)
            {
                warningPopup.ShowWarning("보상을 받을 수 없습니다. 정지하시겠습니까?", ConfirmStop, CancelStop);
            }
            else
            {
                ConfirmStop();
            }
        }

        private void ConfirmStop()
        {
            isTimerRunning = false;
            // 보상 지급 및 타이머 정지 로직 추가
            Debug.Log("타이머가 정지되었습니다.");
        }

        private void CancelStop()
        {
            // 타이머 계속 진행 (아무것도 하지 않음)
            Debug.Log("정지가 취소되었습니다. 타이머 계속 진행.");
        }

        public void SwitchMode(TimerMode newMode)
        {
            currentMode = newMode;
            UpdateModeUI();
        }

        private void UpdateModeUI()
        {
            switch (currentMode)
            {
                case TimerMode.Circular:
                    if (circularTimerRoot != null) circularTimerRoot.SetActive(true);
                    if (digitalTimerRoot != null) digitalTimerRoot.SetActive(false);
                    break;
                case TimerMode.Digital:
                    if (circularTimerRoot != null) circularTimerRoot.SetActive(false);
                    if (digitalTimerRoot != null) digitalTimerRoot.SetActive(true);
                    break;
            }
        }

        private void UpdateTimerDisplay()
        {
            // 여기서 elapsedTime을 기반으로 UI(프로그레스 바, 텍스트)를 업데이트합니다.
        }
    }
}
