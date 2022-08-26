using UnityEngine;

namespace Utils {
    [RequireComponent(typeof(RectTransform))]
    public sealed class SafeAreaFilter : MonoBehaviour {
        RectTransform _rectTransform;
        
        ScreenOrientation _currentOrientation;
        Rect _currentSafeArea;
        
        void Awake() {
            _rectTransform = GetComponent<RectTransform>();

            _currentOrientation = Screen.orientation;
            _currentSafeArea = Screen.safeArea;
        }

        void Update() {
            if ( (_currentOrientation != Screen.orientation) || (_currentSafeArea != Screen.safeArea) ) {
                ApplySafeArea();
            }
        }

        void ApplySafeArea() {
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}