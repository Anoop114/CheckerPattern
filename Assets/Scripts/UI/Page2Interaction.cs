using UnityEngine;
using Utils;

namespace UI
{
    public class Page2Interaction : MonoBehaviour
    {
        [SerializeField] private RectTransform downloadPopup;
        [SerializeField] private RectTransform loadingWheel;
        [SerializeField] private InteractiveUI popupUI;

        private void OnEnable()
        {
            popupUI.Show(downloadPopup,()=> CheckerPatternUtils.IsloadPopup = true);
        }

        private void OnDisable()
        {
            popupUI.CancelQuit(downloadPopup);
        }

        private void Update()
        {
            loadingWheel.eulerAngles -= Vector3.forward*(Time.deltaTime*100);
        }
    }
}