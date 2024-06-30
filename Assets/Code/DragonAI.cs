using System;
using UnityEngine;

public class DragonAI : MonoBehaviour 
{
	
	/**
	 * Uwaga: ten kod celowo jest taki paskudny. To część zadania. Zachęcam do posprzątania przy okazji.
	 */

	public enum State
	{
		 Chasing,
		 Attacking,
		 Dead
	}

	public int maxHP = 30;
    public bool IsDed => state == State.Dead;

    private int hp;

    private void Start()
	{
		hp = maxHP;
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
	}

	State state = State.Chasing;

    private void Update ()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		var dragonAnimator = GetComponent<DragonAnimator>();

		switch (state)
		{
			case State.Chasing:
			{
                transform.LookAt(player.transform);
                dragonAnimator.PlayWalkAnimation();
                transform.Translate(Vector3.forward * Time.deltaTime * 2);
                if (Vector3.Distance(transform.position, player.transform.position) < 10)
                {
                    state = State.Attacking;
                }
            }
			break;
			case State.Attacking:
			{
                dragonAnimator.PlayAttackAnimation();
                GameObject.Find("UI").SendMessage("ShowTextAndQuit", "YOU DIEDED");
            }
			break;
			case State.Dead:
			{
                dragonAnimator.PlayDeadAnimation();
            }
			break;
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
		hp -= damage;
		 
		if (hp <= 0)
		{
			state = State.Dead;
		}
	}
}
