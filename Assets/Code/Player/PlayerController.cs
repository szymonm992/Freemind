using System;
using UnityEngine;

namespace DragonsGame
{
    public class PlayerController : MonoBehaviour
    {
        public const string PLAYER_NAME_STRING = "Player";

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
}