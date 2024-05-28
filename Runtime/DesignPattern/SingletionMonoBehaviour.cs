using UnityEngine;

namespace GiangCustom.DesignPattern
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        [SerializeField]
        private bool canDestroyOnLoad;

        public static T Instance => _instance;

        public virtual void Awake()
        {
            _instance = this as T;
            if (!canDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
    }
}
