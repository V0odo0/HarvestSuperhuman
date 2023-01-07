using UnityEngine;

namespace HSH
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviourBase where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();
                return _instance;
            }
        }
        private static T _instance;


        protected SingletonMonoBehaviour() { }


        protected override void Awake()
        {
            base.Awake();
            _instance = this as T;
        }
    }
}
