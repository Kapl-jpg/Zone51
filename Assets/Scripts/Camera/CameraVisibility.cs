using UnityEngine;

namespace Camera
{
    public class CameraVisibility : Subscriber
    {
        [SerializeField] private UnityEngine.Camera mainCamera;
        [SerializeField] private int playerLayer = 7;
        private int _uiMasks;
        
        [Request("PlayerIsVisible")] 
        private ObservableField<bool> _playerIsVisible = new(true);
        
        [Event("HidePlayerVisible")]
        public void HidePlayer()
        {
            mainCamera.cullingMask &= ~(1 << playerLayer);
            _playerIsVisible.Value = false;
        }

        [Event("ShowPlayerVisible")]
        public void ShowPlayer()
        {
            mainCamera.cullingMask |= (1 << playerLayer);
            _playerIsVisible.Value = true;
        }
    }
}