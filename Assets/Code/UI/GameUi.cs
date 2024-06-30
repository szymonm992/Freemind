using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DragonsGame.UI
{
    public class GameUi : MonoBehaviour
    {
        [SerializeField] private Text label;
        [SerializeField] private Text enemiesLabel;
        [SerializeField] private EnemiesManager dragonSpawner;

        public void ShowTextAndQuit(string message)
        {
            StartCoroutine(ShowTextAneQuitCoroutine(message));
        }

        private void Awake()
        {
            dragonSpawner.DragonSpawnedEvent += OnDragonsPoolChanged;
            dragonSpawner.DragonDiedEvent += OnDragonsPoolChanged;
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
        }

        private IEnumerator ShowTextAneQuitCoroutine(string message)
        {
            yield return new WaitForSeconds(0.3f);
            ShowLabel(message);
            yield return new WaitForSeconds(0.5f);
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
