using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public abstract class Singleton<TInstance> : MonoBehaviour
        where TInstance : MonoBehaviour
    {
        public static TInstance Instance { get; private set; }

        protected virtual void Awake()
        {
            MakeSingletonInstance();
        }

        #region Singleton
        private void MakeSingletonInstance()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
            {
                Instance = FindObjectOfType(typeof(TInstance)) as TInstance;
                if (Instance == null)
                {
                    var gameObject = new GameObject(typeof(TInstance).ToString());
                    Instance = gameObject.AddComponent<TInstance>();
                }
                DontDestroyOnLoad(gameObject);
            }
        }
        #endregion
    }
}