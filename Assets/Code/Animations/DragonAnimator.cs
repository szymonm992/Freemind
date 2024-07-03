using UnityEngine;

public class DragonAnimator : MonoBehaviour
{
    public const string WALKING_STRING = "IsWalk";
    public const string RUNNING_STRING = "IsRun";
    public const string DEAD_STRING = "Dead";
    public const string ATTACK_STRING = "Attack_1";

    [SerializeField] private Animator animator;

    private string currentAnimation;

    public void PlayAttackAnimation()
    {
        StopWalkingAndRunningAnimations();
        TryChangingAnimationTo(ATTACK_STRING);
    }

    public void PlayDeadAnimation()
    {
        StopWalkingAndRunningAnimations();
        TryChangingAnimationTo(DEAD_STRING);
    }

    public void PlayWalkAnimation()
    {
        animator.SetBool(WALKING_STRING, true);
    }

    public void StopWalkingAndRunningAnimations()
    {
        animator.SetBool(WALKING_STRING, false);
        animator.SetBool(RUNNING_STRING, false);
    }

    public void PlayRunAnimation()
    {
        animator.SetBool(WALKING_STRING, true);
        animator.SetBool(RUNNING_STRING, true);
    }

    private void TryChangingAnimationTo(string clipName)
    {
        if (currentAnimation == clipName)
        {
            return;
        }

        animator.SetTrigger(clipName);
        currentAnimation = clipName;
    }
}
