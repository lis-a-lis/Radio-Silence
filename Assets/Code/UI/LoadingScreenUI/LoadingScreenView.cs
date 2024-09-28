using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RadioSilence.UI.LoadingScreen
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private string _defaultLoadingDescription;
        [SerializeField] private TextMeshProUGUI _loadingDescriptionText;
        [SerializeField] private Image _loadingProcessBar;

        private void Awake()
        {
            _loadingDescriptionText.text = _defaultLoadingDescription;
        }

        public void SetLoadingStatus(int loadingPrecent)
        {
            _loadingProcessBar.fillAmount = loadingPrecent;
        }
    }
}