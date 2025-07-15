using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Player
{
    public class Tips : Subscriber
    {
        [SerializeField] private GameObject crosshair;
        [SerializeField] private TMP_Text chipText;
        [SerializeField] private float chipTipTimer;
        [SerializeField] private TMP_Text tipText;
        [SerializeField] private string interactTipText;
        [SerializeField] private string telekinesisTipText;

        private bool _showChip;
        
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
                if (!_showChip)
                    StartCoroutine(NeedDisableChip());
            }
        }

        private IEnumerator NeedDisableChip()
        {
            _showChip = true;
            chipText.gameObject.SetActive(true);
            var t = 1f;
            while (t > 0f)
            {
                t -= Time.deltaTime / chipTipTimer;
                yield return null;
            }

            chipText.gameObject.SetActive(false);
            _showChip = false;
        }

        [Event("HideTip")]
        private void HideTip()
        {
            tipText.text = String.Empty;
            tipText.gameObject.SetActive(false);
        }
    }
}