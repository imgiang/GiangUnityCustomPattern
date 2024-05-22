using UnityEngine;

namespace GiangCustom.DesignPattern
{
    public abstract class SingletonMonoBehaviour<T> where T : MonoBehaviour, new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<T>();
                }
                return instance;
            }
        }

        void OnDestroy()
        {
            instance = null;
        }
    }
}
