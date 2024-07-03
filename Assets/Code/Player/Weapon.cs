using UnityEngine;

namespace DragonsGame
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float bulletsPerSeconds = 10;
        [SerializeField] private Transform bulletSpawnTransform;
        [SerializeField] private BulletsSpawner bulletsSpawner;

        private float shotDeadline;

        public async void Shoot()
        {
            if (IsReadyToShoot())
            {
                return;
            }

            var newBullet = await bulletsSpawner.GetNew(bulletSpawnTransform);
            var bulletMovementDirection = Camera.main.ViewportPointToRay(new Vector3(0.49f, 0.51f, 0)).direction;
            newBullet.Initialize(bulletMovementDirection);

            SetNextShootDeadline();
        }

        private bool IsReadyToShoot()
        {
            return shotDeadline > Time.timeSinceLevelLoad;
        }

        private void SetNextShootDeadline()
        {
            shotDeadline = Time.timeSinceLevelLoad + 1 / bulletsPerSeconds;
        }
    }
}
