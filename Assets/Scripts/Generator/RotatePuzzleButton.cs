using System.Collections;
using Interfaces;
using UnityEngine;

namespace Generator
{
    public class RotatePuzzleButton : MonoBehaviour, IInteractable
    {
        private static readonly int puzzleColor = Shader.PropertyToID("_Color");
        
        [SerializeField] private Transform mechanism;
        [SerializeField] private float rotateTime;


        [SerializeField] [ColorUsage(false, true)]
        private Color lightColor;
        private Color _defaultColor = Color.white;
        
        [Header("Materials")]
        [SerializeField] private Material upMaterial;
        [SerializeField] private Material leftMaterial;
        [SerializeField] private Material rightMaterial;
        private Directions _currentDirection = Directions.Right;
        
        private bool _locked;
        
        public void Interact()
        {
            if(!_locked)
                StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
            _locked = true;
            var angle = mechanism.rotation;
            var targetAngle = Quaternion.Euler(0, 0, TargetAngle());
            
            DisableWires();
            
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / rotateTime;
                mechanism.transform.rotation = Quaternion.Lerp(angle, targetAngle, t);
                yield return null;
            }

            if (_currentDirection == Directions.Up)
            {
                mechanism.transform.rotation = Quaternion.identity;
            }
            
            EnableWires();
            _currentDirection = GetNextDirection();
            
            _locked = false;
        }

        private void DisableWires()
        {
            print("DisableWires");
            // upMaterial.SetColor(puzzleColor, defaultColor);
            // leftMaterial.SetColor(puzzleColor, defaultColor);
            // rightMaterial.SetColor(puzzleColor, defaultColor);
        }
        
        private void EnableWires()
        {
            //Добавить условие победы
            switch (_currentDirection)
            {
                case Directions.Up:
                    print("UpRight");
                    // upMaterial.SetColor(puzzleColor, lightColor);
                    // rightMaterial.SetColor(puzzleColor, lightColor);
                    break;
                case Directions.Right:
                    print("LeftRight");
                    // leftMaterial.SetColor(puzzleColor, lightColor);
                    // rightMaterial.SetColor(puzzleColor, lightColor);
                    break;
                case Directions.Down:
                    print("UpLeft");
                    // upMaterial.SetColor(puzzleColor, lightColor);
                    // leftMaterial.SetColor(puzzleColor, lightColor);
                    break;
            }
        }

        private float TargetAngle()
        {
            return _currentDirection switch
            {
                Directions.Right => -90f,
                Directions.Down => -180f,
                Directions.Left => -270f,
                _ => -360f
            };
        }

        private Directions GetNextDirection()
        {
            return _currentDirection switch
            {
                Directions.Right => Directions.Down,
                Directions.Down => Directions.Left,
                Directions.Left => Directions.Up,
                _ => Directions.Right
            };
        }
    }
}