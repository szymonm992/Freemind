using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragonSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject dragonPrefab;

    
    private IEnumerator Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Dragon Spawn Point")
                                .Select(x => x.transform)
                                .ToList();

        yield return new WaitForSeconds(10f);
        while (true) {
            SpawnDragon();

            var dedDragons = FindObjectsOfType<DragonAI>().Where(x => x.IsDed).ToList().Count;
            var livDragons = FindObjectsOfType<DragonAI>().Where(y => !y.IsDed).ToList().Count;

            
            
            var t = Mathf.Max(3 + livDragons / 10 - dedDragons / 10, 1);
            yield return new WaitForSeconds(t);
        }
    }

    private void SpawnDragon()
    {
        var spawnPoint = spawnPoints[0];
        spawnPoints.RemoveAt(0);

        var newDragon = Instantiate(dragonPrefab);

        newDragon.transform.position = spawnPoint.position;
        spawnPoints.Add(spawnPoint);
    }
}