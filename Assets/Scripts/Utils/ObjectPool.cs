using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject myObjectContainer;

        [SerializeField]
        private int poolSize = 10;

        [SerializeField]
        private GameObject myObjectPrefab;

        private Stack<GameObject> pooledObjects;

        private int nextId = 1;

        public void InitializePool()
        {
            pooledObjects = new Stack<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                AddNewObject();
            }
        }

        public GameObject Instantiate()
        {
            GameObject myObject;

            if (pooledObjects.Count == 0)
            {
                myObject = AddNewObject();
            }
            else
            {
                myObject = GetAvaibleObject();
            }

            myObject.SetActive(true);

            return myObject;
        }

        public void Destroy(GameObject myObject)
        {
            myObject.SetActive(false);
            if (myObject.name.Contains("pooled#"))
            {
                pooledObjects.Push(myObject);
            }
        }

        private GameObject AddNewObject()
        {
            var myObject = Instantiate(myObjectPrefab);
            myObject.name = myObject.name + " (pooled#" + (nextId++) + ")";
            myObject.SetActive(false);
            myObject.transform.SetParent(myObjectContainer.transform);

            pooledObjects.Push(myObject);

            return myObject;
        }

        public void HardDestroyAll()
        {
            var size = pooledObjects.Count;

            for (int i = 0; i < size; i++)
            {
                var myObject = GetAvaibleObject();
                Destroy(myObject);
            }
        }

        private GameObject GetAvaibleObject()
        {
            var myObject = pooledObjects.Pop();

            return myObject;
        }
    }
}