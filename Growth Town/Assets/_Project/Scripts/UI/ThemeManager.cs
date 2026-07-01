using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LifeTown.UI
{
    public class ThemeManager : MonoBehaviour
    {
        [Header("Bouncy Animation Settings")]
        public float shrinkScale = 0.9f;
        public float animationDuration = 0.15f;
        public bool autoApplyToAllButtons = true;

        private void Start()
        {
            if (autoApplyToAllButtons)
            {
                ApplyThemeToAllButtons();
            }
        }

        public void ApplyThemeToAllButtons()
        {
            Button[] allButtons = FindObjectsOfType<Button>(true);
            foreach (Button btn in allButtons)
            {
                if (btn.GetComponent<BouncyElement>() == null)
                {
                    BouncyElement bouncy = btn.gameObject.AddComponent<BouncyElement>();
                    bouncy.Initialize(shrinkScale, animationDuration);
                }
            }
        }
    }

    [RequireComponent(typeof(Selectable))]
    public class BouncyElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector3 _originalScale;
        private float _shrinkScale = 0.9f;
        private float _duration = 0.15f;
        private Selectable _selectable;

        public void Initialize(float shrinkScale, float duration)
        {
            _shrinkScale = shrinkScale;
            _duration = duration;
        }

        private void Awake()
        {
            _selectable = GetComponent<Selectable>();
            _originalScale = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;
            transform.localScale = _originalScale * _shrinkScale;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;
            transform.localScale = _originalScale;
        }
        
        private void OnDisable()
        {
            transform.localScale = _originalScale;
        }
    }
}
