using UnityEngine;

namespace RadioSilence.GameRoot
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Canvas _childCanvas;

        public Transform UICanvasTransform => _childCanvas.transform;

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }
    }
}