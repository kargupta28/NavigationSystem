using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; // for Odin package

namespace ObjectSpawner
{

    public class ObjectSpawner : MonoBehaviour
    {
        public GameObject spawnObject;
        public List<GameObject> spawnObjectList = new List<GameObject>();

        [Button("Spawn Object")]
        void SpawnNewObject()
        {
            GameObject newSpawnedObject = Instantiate(spawnObject, GetRandomPosition(), Quaternion.identity);
            spawnObjectList.Add(newSpawnedObject);
            Debug.Log("New Object Spawned");
        }


        [Button("Destroy Objects")]
        private void DestroySpawnedObjects()
        {

            foreach(var i in spawnObjectList)
            {
                DestroyImmediate(i);
            }

            spawnObjectList = new List<GameObject>();
        }

        Vector3 GetRandomPosition()
        {
            Vector3 newPosition;

            newPosition = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
            return newPosition;
        }
    }
}
