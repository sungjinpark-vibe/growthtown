using UnityEngine;
using UnityEngine.UI;
using System;

namespace LifeTown.UI
{
    /// <summary>
    /// 5분 미만 정지 시 표시되는 경고 팝업의 뼈대 스크립트입니다.
    /// </summary>
    public class WarningPopupUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text warningMessageText; // Text or TMPro.TextMeshProUGUI
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        private Action onConfirm;
        private Action onCancel;

        private void Awake()
        {
            // 버튼 이벤트 리스너 등록
            if (confirmButton != null) confirmButton.onClick.AddListener(OnConfirmClicked);
            if (cancelButton != null) cancelButton.onClick.AddListener(OnCancelClicked);
        }

        /// <summary>
        /// 팝업을 표시하고 메시지와 콜백을 설정합니다.
        /// </summary>
        /// <param name="message">표시할 경고 메시지</param>
        /// <param name="confirmCallback">확인(예) 버튼 클릭 시 실행할 동작</param>
        /// <param name="cancelCallback">취소(아니오) 버튼 클릭 시 실행할 동작</param>
        public void ShowWarning(string message, Action confirmCallback, Action cancelCallback)
        {
            if (warningMessageText != null)
            {
                warningMessageText.text = message;
            }

            onConfirm = confirmCallback;
            onCancel = cancelCallback;

            gameObject.SetActive(true);
        }

        private void OnConfirmClicked()
        {
            onConfirm?.Invoke();
            Hide();
        }

        private void OnCancelClicked()
        {
            onCancel?.Invoke();
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
            // 메모리 누수 방지를 위해 콜백 초기화
            onConfirm = null;
            onCancel = null;
        }
    }
}
