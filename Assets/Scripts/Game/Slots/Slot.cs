using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Slots {
    public class Slot : MonoBehaviour, IDropHandler {
        [SerializeField] Image BackImage;
        [SerializeField] Image FillImage;

        public SlotType SlotType { get; private set; }

        bool _isActive;
        Action<SlotType> _onSlotCompleted;

        public void Init(SlotType slotType, Sprite backSprite, Sprite fillSprite, Action<SlotType> onSlotCompleted) {
            _isActive = true;

            SlotType = slotType;

            BackImage.sprite = backSprite;
            BackImage.SetNativeSize();

            FillImage.sprite = fillSprite;
            FillImage.SetNativeSize();
            FillImage.gameObject.SetActive(false);

            _onSlotCompleted = onSlotCompleted;
        }

        public void DisableSlot() {
            _isActive = false;
        }

        public void OnDrop(PointerEventData eventData) {
            if ( !_isActive ) {
                return;
            }

            if ( eventData.pointerDrag == null ) {
                Debug.LogError("Pointer drag is null");
                return;
            }

            var item = eventData.pointerDrag.GetComponent<PlayableItem.PlayableItem>();

            if ( item == null ) {
                Debug.LogError($"Item dont have {typeof(PlayableItem.PlayableItem)} component");
                return;
            }

            if ( item.SlotType != SlotType ) {
                return;
            }

            FillImage.gameObject.SetActive(true);

            _onSlotCompleted?.Invoke(SlotType);
        }
    }
}