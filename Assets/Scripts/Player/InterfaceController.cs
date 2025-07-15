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
        [SerializeField] private string needDisableChipTipText;
        [SerializeField] private string needChangeFormTipText;

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

            if (tipType == TipType.NeedDisableChip)
            {
                tipText.text = needDisableChipTipText;
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