using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    public event Action<DragonAI> DeathEvent;

    /**
     * Uwaga: ten kod celowo jest taki paskudny. To część zadania. Zachęcam do posprzątania przy okazji.
     */

    public enum State
    {
        Chasing,
        Attacking,
        Dead
    }

    [SerializeField] private int maxHP = 30;
    [SerializeField] private DragonAnimator dragonAnimator;
    public bool IsDead => state == State.Dead;

    private GameObject player;
    private int currentHp;
    private bool initialized = false;

    private async void Start()
    {
        await WaitForPlayer();
        Debug.Assert(dragonAnimator != null, "Dragon animator cannot be null!");

        currentHp = maxHP;
        var renderers = GetComponentsInChildren<Renderer>();

        foreach (var ren in renderers)
        {
            for (int i = 0; i < ren.materials.Length; i++)
            {
                Debug.Log($"Check material on Dragon {name} renderer {ren.name} slot{i}");
                if (ren.materials[i] == null)
                {
                    Debug.LogError($"Missing material on Dragon {name} renderer {ren.name} slot{i}");
                }
            }
        }
        Debug.Log("iniy");
        initialized = true;
    }

    private async UniTask WaitForPlayer()
    {
        while (player == null)
        {
            player = GameObject.FindObjectOfType<PlayerController>()?.gameObject;
            await UniTask.Yield();
        }
    }

    private State state = State.Chasing;

    private void Update()
    {
        if (!initialized)
        {
            return;
        }

        switch (state)
        {
            case State.Chasing:
            {
                if (player == null) return;

                transform.LookAt(player.transform);
                dragonAnimator.PlayWalkAnimation();
                transform.Translate(Vector3.forward * Time.deltaTime * 2);
                if (Vector3.Distance(transform.position, player.transform.position) < 10)
                {
                    state = State.Attacking;
                }
                break;
            }
            case State.Attacking:
            {
                dragonAnimator.PlayAttackAnimation();
                GameObject.Find("UI").SendMessage("ShowTextAndQuit", "YOU DIEDED");
                break;
            }
            case State.Dead:
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Trap")
        {
            state = State.Dead;
        }
    }

    public void DealDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            DeathEvent?.Invoke(this);
            state = State.Dead;
        }
    }
}
