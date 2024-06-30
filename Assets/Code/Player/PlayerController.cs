using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            weapon.Shoot();
        }
    }
}