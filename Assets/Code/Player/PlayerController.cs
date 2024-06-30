using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action DeathEvent;

    [SerializeField] private Weapon weapon;

    public void Hit()
    {
        DeathEvent?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            weapon.Shoot();
        }
    }
}