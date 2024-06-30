using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

namespace DragonsGame.UI
{
    public class GameUi : MonoBehaviour
    {
        private const string PLAYER_DIED_MESSAGE = "You died!";

        [SerializeField] private Text label;
        [SerializeField] private Text enemiesLabel;
        [SerializeField] private EnemiesManager dragonSpawner;

        private PlayerController playerController;

        private async void Awake()
        {
            dragonSpawner.DragonSpawnedEvent += OnDragonsPoolChanged;
            dragonSpawner.DragonDiedEvent += OnDragonsPoolChanged;

            await WaitForPlayer();

            playerController.DeathEvent += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            ShowTextAndQuit(PLAYER_DIED_MESSAGE).Forget();
        }

        private async UniTask WaitForPlayer()
        {
            while (playerController == null)
            {
                playerController = GameObject.FindObjectOfType<PlayerController>();
                await UniTask.Yield();
            }
        }

        private void OnDragonsPoolChanged()
        {
            UpdateInternalHUD();
        }

        private void UpdateInternalHUD()
        {
            enemiesLabel.text = $"Enemies: {dragonSpawner.AliveDragons} \nKilled: {dragonSpawner.DeadDragons}";
        }

        private void OnDestroy()
        {
            dragonSpawner.DragonSpawnedEvent -= OnDragonsPoolChanged;
            dragonSpawner.DragonDiedEvent -= OnDragonsPoolChanged;
            playerController.DeathEvent -= OnPlayerDied;
        }

        private async UniTask ShowTextAndQuit(string message)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.3d)); 
            ShowLabel(message);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5d));
            Stop();
        }

        private void ShowLabel(string message)
        {
            label.text = message;
            label.gameObject.SetActive(true);
        }

        private void Stop()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Debug.Log("Level complete, stopping playmode\n");
        }
    }
}
