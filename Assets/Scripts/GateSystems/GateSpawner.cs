using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    public int gateAmount;
    public float zPadding;
    private Vector3 gatePos;

    public List<GameObject> gatePrefabs;
    public List<GameObject> SpawnedGates = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < gateAmount; i++) 
        {

            int RandomIndex = Random.Range(0, gatePrefabs.Count);
            GameObject selectedGate = gatePrefabs[RandomIndex];
            float xPos = GetRandomXPos();
            if (SpawnedGates.Count == 0)
            {   
                gatePos = new Vector3(xPos, 0, transform.position.z);
                
            }
            else
            {
               float zPos = SpawnedGates[SpawnedGates.Count-1].transform.position.z;
                gatePos = new Vector3(xPos, 0, zPos + zPadding);
            }

            GameObject spawnedGate = Instantiate(selectedGate, gatePos, Quaternion.identity);
            SpawnedGates.Add(spawnedGate);

        }
    }


    private float GetRandomXPos()
    {
        float xPos = Random.Range(0, 4);
        return xPos;

    }
}
