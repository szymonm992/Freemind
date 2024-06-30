using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace DragonsGame
{
    public class EnemiesManager : MonoBehaviour
    {
        public const float INITIAL_SPAWN_DELAY = 10f;

        public event Action DragonSpawnedEvent;
        public event Action DragonDiedEvent;

        public int SpawnedDragonsAmount { get; private set; } = 0;
        public int AliveDragons { get; private set; } = 0;
        public int DeadDragons { get; private set; } = 0;

        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private DragonAI dragonPrefab;

        public List<DragonAI> allDragons = new List<DragonAI>();

        private async void Start()
        {
            FindExistingEnemies();
            await UniTask.Delay(TimeSpan.FromSeconds(INITIAL_SPAWN_DELAY));
            await StartSpawningDragons();
        }

        private async UniTask StartSpawningDragons()
        {
            while (true)
            {
                SpawnDragon();

                var t = Mathf.Max(3 + AliveDragons / 10 - DeadDragons / 10, 1);
                await UniTask.Delay(TimeSpan.FromSeconds(t), true);
            }
        }

        private void FindExistingEnemies()
        {
            var alreadyExistingDragons = GameObject.FindObjectsOfType<DragonAI>();
            if (alreadyExistingDragons != null && alreadyExistingDragons.Length > 0)
            {
                foreach (var dragon in alreadyExistingDragons)
                {
                    SetupNewDragon(dragon);
                }
            }
        }

        private void SpawnDragon()
        {
            var spawnPoint = spawnPoints[0];
            spawnPoints = spawnPoints.Skip(1).Concat(new[] { spawnPoint }).ToArray();

            var newDragon = Instantiate(dragonPrefab);

            SetupNewDragon(newDragon);
            newDragon.transform.position = spawnPoint.position;
        }

        private void OnDeath(DragonAI dragonAI)
        {
            dragonAI.DeathEvent -= OnDeath;

            AliveDragons--;
            DeadDragons++;
            DragonDiedEvent?.Invoke();
        }

        private void SetupNewDragon(DragonAI newDragon)
        {
            if (newDragon != null)
            {
                allDragons.Add(newDragon);
                SpawnedDragonsAmount++;

                if (!newDragon.IsDead)
                {
                    newDragon.DeathEvent += OnDeath;
                    AliveDragons++;
                }
                else
                {
                    DeadDragons++;
                }

                DragonSpawnedEvent?.Invoke();
            }
        }
    }
}



