using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedCharacterPrefab;
    [SerializeField] private Transform destination;
    [SerializeField] private float delayBeforeFirst;
    [SerializeField] private float spawnPeriod;
    private bool isActive;
    private float timeToNextSpawn = 0;

    private void Update()
    {
        if (isActive)
        {
            timeToNextSpawn -= Time.deltaTime;
            if (timeToNextSpawn < 0)
                SpawnUnit();
        }
    }

    public void Activate()
    {
        isActive = true;
        timeToNextSpawn = delayBeforeFirst;
    }

    private void SpawnUnit()
    {
        GameObject newCharacter = Instantiate(spawnedCharacterPrefab, transform.position, Quaternion.identity);
        newCharacter.GetComponent<MoveToPoint>().guardPoint = destination;
        timeToNextSpawn = spawnPeriod;
    }
    
}
