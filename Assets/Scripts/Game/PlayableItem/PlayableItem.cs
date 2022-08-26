using DG.Tweening;
using Infrastructure.Services.SoundManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.PlayableItem {
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class PlayableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
        const float ShowAnimOffset = 2000f;
        const float ShowAnimDuration = 0.5f;
        const float BackAnimDuration = 0.5f;

        [SerializeField] Image ItemImage;

        public SlotType SlotType { get; private set; }

        Canvas _gameCanvas;
        CanvasGroup _canvasGroup;
        RectTransform _rectTransform;
        Vector2 _startPosition;

        ISoundManager _soundManager;

        public void Init(Canvas gameCanvas, ISoundManager soundManager) {
            _gameCanvas = gameCanvas;
            _soundManager = soundManager;

            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _startPosition = _rectTransform.anchoredPosition;
        }

        public void ChangeType(SlotType slotType, Sprite itemSprite) {
            SlotType = slotType;

            ItemImage.sprite = itemSprite;
            ItemImage.SetNativeSize();

            gameObject.SetActive(true);
        }

        public void ResetItem() {
            _soundManager.PlaySound(SoundType.ItemSuccess);
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
            _rectTransform.anchoredPosition = _startPosition;
        }

        public void PlayShowAnimationAndSound() {
            _rectTransform.anchoredPosition = Vector2.right * ShowAnimOffset;
            _rectTransform
                .DOAnchorPos(_startPosition, ShowAnimDuration)
                .OnComplete(() => {
                    _canvasGroup.blocksRaycasts = true;
                    _soundManager.PlaySound(SoundType.ItemAppear);
                });
        }

        public void OnDrag(PointerEventData eventData) {
            _rectTransform.anchoredPosition += eventData.delta / _gameCanvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData) {
            _rectTransform
                .DOAnchorPos(_startPosition, BackAnimDuration)
                .OnComplete(() => { _canvasGroup.blocksRaycasts = true; });
        }
    }
}