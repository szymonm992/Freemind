using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float bulletsPerSeconds = 10;
    [SerializeField] private float bulletSpeed = 100;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnTransform;

    private float shotDeadline;

    public void Shoot() 
    {
        if (IsReadyToShoot())
        {
            return;
        }

        AddVelocity(SpawnBullet());
        SetNextShootDeadline();
    }

    private bool IsReadyToShoot()
    {
        return shotDeadline > Time.timeSinceLevelLoad;
    }

    private GameObject SpawnBullet()
    {
        var bulletGameObject = Instantiate(bulletPrefab, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
        bulletGameObject.GetComponent<Bullet>().Init();
        return bulletGameObject;
    }

    private void AddVelocity(GameObject bulletGameObject)
    {
        var direction = Camera.main.ViewportPointToRay(new Vector3(0.49f, 0.51f, 0)).direction;
        var bulletRigidbody = bulletGameObject.GetComponent<Rigidbody>();
        bulletRigidbody.velocity += direction * bulletSpeed;
    }

    private void SetNextShootDeadline()
    {
        shotDeadline = Time.timeSinceLevelLoad + 1 / bulletsPerSeconds;
    }
}