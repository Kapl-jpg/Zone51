using System;
using TMPro;
using UnityEngine;

namespace Player
{
    public class InterfaceController : Subscriber
    {
        [SerializeField] private GameObject crosshair;
        [SerializeField] private TMP_Text tipText;
        [SerializeField] private string interactTipText;
        [SerializeField] private string telekinesisTipText;

        [Event("ShowCrosshair")]
        private void ShowCrosshair()
        {
            crosshair.SetActive(true);
        }

        [Event("HideCrosshair")]
        private void HideCrosshair()
        {
            crosshair.SetActive(false);
        }

        [Event("ShowTip")]
        private void ShowTip(TipType tipType)
        {
            tipText.gameObject.SetActive(true);
            if (tipType == TipType.Interact)
            {
                tipText.text = interactTipText;
            }

            if (tipType == TipType.Telekinesis)
            {
                tipText.text = telekinesisTipText;
            }
        }

        [Event("HideTip")]
        private void HideTip()
        {
            tipText.text = String.Empty;
            tipText.gameObject.SetActive(false);
        }
    }
}