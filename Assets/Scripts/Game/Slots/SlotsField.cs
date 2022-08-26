using System;
using System.Collections.Generic;
using Configs;
using DG.Tweening;
using Infrastructure.Services.SoundManager;
using UnityEngine;

namespace Game.Slots {
    public sealed class SlotsField : MonoBehaviour {
        const float ShowAnimOffset = 1000f;
        const float ShowAnimDuration = 0.5f;

        [SerializeField] List<Slot> Slots = new List<Slot>();

        ISoundManager _soundManager;

        public void InitSlotsField(ISoundManager soundManager, List<SlotDescription> slotDescriptions, Action<SlotType> onSlotCompleted) {
            _soundManager = soundManager;
            
            for (var i = 0; i < Slots.Count; i++) {
                var slotDesc = slotDescriptions[i];
                Slots[i].Init(slotDesc.SlotType, slotDesc.BackSprite, slotDesc.ItemSprite, onSlotCompleted);
            }
        }

        public void PlaySlotsShowAnimationAndSound(Action onComplete) {
            var sequence = DOTween.Sequence();

            foreach (var slot in Slots) {
                var currentPosition = slot.transform.localPosition;

                slot.transform.localPosition = Vector2.up * ShowAnimOffset;
                sequence.Append(slot.transform.DOLocalMove(currentPosition, ShowAnimDuration)
                    .OnComplete(() => _soundManager.PlaySound(SoundType.ItemAppear)));
            }

            sequence.Play()
                .OnComplete(() => { onComplete?.Invoke(); });
        }

        public void DisableSlot(SlotType slotType) {
            foreach (var slot in Slots) {
                if ( slot.SlotType == slotType ) {
                    slot.DisableSlot();
                }
            }
        }
    }
}