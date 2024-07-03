using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace DragonsGame
{
    public class DragonAI : MonoBehaviour
    {
        public const string TRAP_TAG = "Trap";

        public event Action<DragonAI> DeathEvent;

        public bool IsDead => currentState == EnemyState.Dead;

        public enum EnemyState
        {
            Chasing,
            Attacking,
            Dead
        }

        [SerializeField] private int maxHP = 30;
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float playerAttackDistanceThreshold = 10f;
        [SerializeField] private DragonAnimator dragonAnimator;

        private EnemyState currentState = EnemyState.Chasing;
        private PlayerController player;
        private int currentHp;
        private bool initialized = false;
        private float playerAttackDistanceThresholdSqr; 

        public void DealDamage(int damage)
        {
            currentHp -= damage;

            if (currentHp <= 0)
            {
                DeathEvent?.Invoke(this);
                Die();
            }
        }

        private async void Start()
        {
            await WaitForPlayer();
            Debug.Assert(dragonAnimator != null, "Dragon animator cannot be null!");

            currentHp = maxHP;
            var renderers = GetComponentsInChildren<Renderer>();

            foreach (var renderer in renderers)
            {
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    Debug.Log($"Check material on Dragon {name} renderer {renderer.name} slot{i}");
                    if (renderer.materials[i] == null)
                    {
                        Debug.LogError($"Missing material on Dragon {name} renderer {renderer.name} slot{i}");
                    }
                }
            }

            playerAttackDistanceThresholdSqr = playerAttackDistanceThreshold * playerAttackDistanceThreshold;
            initialized = true;
        }

        private async UniTask WaitForPlayer()
        {
            while (player == null)
            {
                player = GameObject.FindObjectOfType<PlayerController>();
                await UniTask.Yield();
            }
        }

        private void Update()
        {
            if (!initialized)
            {
                return;
            }

            switch (currentState)
            {
                case EnemyState.Chasing:
                {
                    if (player == null)
                    {
                        return;
                    }

                    transform.LookAt(player.transform);
                    dragonAnimator.PlayWalkAnimation();
                    transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

                    Vector3 directionToPlayer = player.transform.position - transform.position;
                    if (directionToPlayer.sqrMagnitude < playerAttackDistanceThresholdSqr)
                    {
                        currentState = EnemyState.Attacking;
                    }

                    break;
                }
                case EnemyState.Attacking:
                {
                    Attack();
                    break;
                }
                case EnemyState.Dead:
                {
                    dragonAnimator.PlayDeadAnimation();
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Attack()
        {
            player.Hit();
            dragonAnimator.PlayAttackAnimation();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == TRAP_TAG)
            {
                Die();
            }
        }

        private void Die()
        {
            currentState = EnemyState.Dead;
        }
    }
}