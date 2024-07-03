using Cysharp.Threading.Tasks;
using DragonsGame.Pooling;
using UnityEngine;

namespace DragonsGame
{
    public class BulletsSpawner : MonoBehaviour
    {
        public const int INITIAL_POOL_SIZE = 100;

        [SerializeField] private Bullet bulletPrefab;

        private MonoObjectPool<Bullet> bulletsPool;

        public async UniTask<Bullet> GetNew(Transform newBulletTransform)
        {
            var newBullet = await bulletsPool.GetFreeObject(newBulletTransform);
            newBullet.DisposedEvent += OnDisposedBullet;
            return newBullet;
        }

        private void OnDisposedBullet(Bullet bullet)
        {
            bullet.DisposedEvent -= OnDisposedBullet;
            bulletsPool.ReturnObjectToPool(bullet);
        }

        private void Start()
        {
            bulletsPool = new MonoObjectPool<Bullet>(bulletPrefab, INITIAL_POOL_SIZE, INITIAL_POOL_SIZE);
        }
    }
}

