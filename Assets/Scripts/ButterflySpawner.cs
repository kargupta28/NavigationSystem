using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ButterflySpawner
{
    public class ButterflySpawner : MonoBehaviour
    {
        // Ideas:
        // 1. Add a toggle button for butterfly to be in front or even behind you
        // 2. Limit y axis, ie. how high the butterfly can fly. It shouldn't be too high so the user can't hit it.
        // 3. Destroy Target/Distractor butterfly to destroy based on Zones (minRadius and maxRadius) and not just butterflyType,
        //    ie. don't just destroy any distrator butterfly but destroy the one which belongs to a particular Zone.

        [FoldoutGroup("Butterfly Properties", expanded: true)]
        public float speed;
        [FoldoutGroup("Butterfly Properties")]
        public float verticalSpeed;
        [FoldoutGroup("Butterfly Properties")]
        public float amplitude;
        [FoldoutGroup("Butterfly Properties")]
        public float reachDist;
        
        [FoldoutGroup("Zones", expanded: true)]
        [LabelText("Player Zone Radius")]
        public int playerZone;
        [FoldoutGroup("Zones")]
        [LabelText("Zone 1 Radius")]
        public int Zone1;
        [FoldoutGroup("Zones")]
        [LabelText("Zone 2 Radius")]
        public int Zone2;
        [FoldoutGroup("Zones")]
        [LabelText("Zone 3 Radius")]
        public int Zone3;
        [FoldoutGroup("Zones")]
        public Vector3 center;
        [LabelText("Butterfly Object")]
        public GameObject spawnObject;
        [LabelText("Butterfly Object List")]
        public List<GameObject> spawnObjectList = new List<GameObject>();

        
        [LabelText("Target Butterfly Zones")]
        public Zone target;

        [LabelText("Distractor Butterfly Zones")]
        public Zone distractor;

        public enum Zone
        {
            Player_Zone1, Player_Zone2, Player_Zone3,
            Zone1_Zone2, Zone1_Zone3, Zone2_Zone3
        }

        [Button("Spawn Target Butterfly")]
        [ButtonGroup("Target")]
        [GUIColor(0, 1, 0)]
        void SpawnTargetButterfly()
        {
            int[] radius = getMinMaxRadius(target);
            GameObject newSpawnedObject = Instantiate(spawnObject, new Vector3(0, -1, 0), Quaternion.Euler(-90, -90, 0));
            newSpawnedObject.GetComponent<FlyingAround>().setValue(
                "Target", radius[0], radius[1], speed, verticalSpeed, amplitude, reachDist, center
                );
            spawnObjectList.Add(newSpawnedObject);
            Debug.Log("Target Butterfly Spawned!");
        }

        [Button("Destroy Target Butterfly")]
        [ButtonGroup("Target")]
        [GUIColor(1, 0.6f, 0.4f)]
        private void DestroyTargetButterfly()
        {

            foreach (var i in spawnObjectList)
            {
                if(i.GetComponent<FlyingAround>().butterflyType == "Target")
                {
                    spawnObjectList.Remove(i);
                    DestroyImmediate(i);
                    break;
                }
                    
            }
        }

        [Button("Spawn Distractor Butterfly")]
        [ButtonGroup("Distractor")]
        [GUIColor(0, 1, 0)]
        void SpawnDistractorButterfly()
        {
            int[] radius = getMinMaxRadius(distractor);
            GameObject newSpawnedObject = Instantiate(spawnObject, new Vector3(0, -1, 0), Quaternion.Euler(-90, -90, 0));
            newSpawnedObject.GetComponent<FlyingAround>().setValue(
                "Distractor", radius[0], radius[1], speed, verticalSpeed, amplitude, reachDist, center
                );
            spawnObjectList.Add(newSpawnedObject);
            Debug.Log("Distractor Butterfly Spawned!");
        }

        [Button("Destroy Distractor Butterfly")]
        [ButtonGroup("Distractor")]
        [GUIColor(1, 0.6f, 0.4f)]
        private void DestroyDistractorButterfly()
        {

            foreach (var i in spawnObjectList)
            {
                if (i.GetComponent<FlyingAround>().butterflyType == "Distractor")
                {
                    spawnObjectList.Remove(i);
                    DestroyImmediate(i);
                    break;
                }

            }
        }

        int[] getMinMaxRadius(Zone zones)
        {
            int[] radius = new int[2];

            if (zones == Zone.Player_Zone1)
            {
                radius[0] = playerZone;
                radius[1] = Zone1;
            }
            else if (zones == Zone.Player_Zone2)
            {
                radius[0] = playerZone;
                radius[1] = Zone2;
            }
            else if (zones == Zone.Player_Zone3)
            {
                radius[0] = playerZone;
                radius[1] = Zone3;
            }
            else if (zones == Zone.Zone1_Zone2)
            {
                radius[0] = Zone1;
                radius[1] = Zone2;
            }
            else if (zones == Zone.Zone1_Zone3)
            {
                radius[0] = Zone1;
                radius[1] = Zone3;
            }
            else if (zones == Zone.Zone2_Zone3)
            {
                radius[0] = Zone2;
                radius[1] = Zone3;
            }
            else
            {
                Debug.Log("Error: No Zone selected!");
            }

            return radius;
        }
    }

}