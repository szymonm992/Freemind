using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField] float bulletsPerSeconds = 10;
    [SerializeField] float bulletSpeed = 100;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnTransform;

    float shotDeadline;

    public void Shoot() {
        if (IsReadyToShoot())
            return;
        AddVelocity(SpawnBullet());
        SetNextShootDeadline();
    }

    bool IsReadyToShoot() {
        return shotDeadline > Time.timeSinceLevelLoad;
    }

    GameObject SpawnBullet() {
        var bulletGameObject = Instantiate(bulletPrefab, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
        bulletGameObject.GetComponent<Bullet>().Init();
        return bulletGameObject;
    }

    void AddVelocity(GameObject bulletGameObject) {
        var direction = Camera.main.ViewportPointToRay(new Vector3(0.49f, 0.51f, 0)).direction;
        var bulletRigidbody = bulletGameObject.GetComponent<Rigidbody>();
        bulletRigidbody.velocity += direction * bulletSpeed;
    }

    void SetNextShootDeadline() {
        shotDeadline = Time.timeSinceLevelLoad + 1 / bulletsPerSeconds;
    }
}