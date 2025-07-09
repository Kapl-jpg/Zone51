using UnityEngine;

namespace Patterns.Singleton
{
    public class MouseSingleton : MonoBehaviour
    {
        public static MouseSingleton instance { get; set; }

        //private MouseSingleton()
        //{ }

        //public static MouseSingleton getInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = FindObjectOfType<MouseSingleton>();
        //    }
        //    else
        //    {
        //        return instance;
        //    }

        //}

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
                return;
            }

            Destroy(this.gameObject);
        }
    }
}

