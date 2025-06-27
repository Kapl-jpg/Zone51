using System.Collections;
using UnityEngine;

namespace Terminal
{
    public class SwitchNextScreen : MonoBehaviour
    {
        [SerializeField] private GameObject currentScreen;
        [SerializeField] private GameObject nextScreen;
        [SerializeField] private float delay;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);
            currentScreen.SetActive(false);
            nextScreen.SetActive(true);
        }
    }
}