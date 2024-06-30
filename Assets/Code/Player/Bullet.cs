using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DragonsGame
{
    public class Bullet : MonoBehaviour
    {
        public const float DISABLING_TIME_THERESHOLD = 4f;

        public event Action<Bullet> DisposedEvent;

        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float movementSpeed = 100f;
        [SerializeField] private int damage = 10;

        private readonly Color[] colors = { Color.yellow, Color.red, Color.white, Color.blue, Color.green };

        public void Initialize(Vector3 movementDirection)
        {
            rigidbody.isKinematic = false;
            SetRandomColor();
            rigidbody.velocity += movementDirection * movementSpeed;
            DisableAfterTime().Forget();
        }

        private void SetRandomColor()
        {
            var material = meshRenderer.material;
            material.color = colors[Random.Range(0, colors.Length)];
        }

        private void OnCollisionEnter(Collision other)
        {
            rigidbody.useGravity = true;

            if (other.gameObject.TryGetComponent(out DragonAI dragonAi))
            {
                dragonAi.DealDamage(damage);
                DisableBullet();
            }
        }

        private void DisableBullet()
        {
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector3.zero;
            DisposedEvent?.Invoke(this);
        }
        private async UniTask DisableAfterTime()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(DISABLING_TIME_THERESHOLD));
            DisableBullet();
        }
    }
}