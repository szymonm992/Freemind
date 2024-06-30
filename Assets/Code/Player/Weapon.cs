using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float bulletsPerSeconds = 10;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnTransform;

    private float shotDeadline;

    public void Shoot() 
    {
        if (IsReadyToShoot())
        {
            return;
        }

        var newBullet = SpawnBullet();
        var bulletMovementDirection = Camera.main.ViewportPointToRay(new Vector3(0.49f, 0.51f, 0)).direction;
        newBullet.Initialize(bulletMovementDirection);

        SetNextShootDeadline();
    }

    private bool IsReadyToShoot()
    {
        return shotDeadline > Time.timeSinceLevelLoad;
    }

    private Bullet SpawnBullet()
    {
        var bulletGameObject = Instantiate(bulletPrefab, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
        return bulletGameObject;
    }

    private void SetNextShootDeadline()
    {
        shotDeadline = Time.timeSinceLevelLoad + 1 / bulletsPerSeconds;
    }
}