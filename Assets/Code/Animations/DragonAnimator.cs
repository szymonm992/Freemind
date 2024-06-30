using UnityEngine;

public class DragonAnimator : MonoBehaviour
{
    private string currentAnimation;

    public void PlayAttackAnimation()
    {
        StopWalkingAndRunningAnimations();
        TryChangingAnimationTo("Attack_1");
    }

    public void PlayDeadAnimation()
    {
        StopWalkingAndRunningAnimations();
        TryChangingAnimationTo("Dead");
    }

    public void PlayWalkAnimation()
    {
        GetComponent<Animator>().SetBool("IsWalk", true);
    }

    public void StopWalkingAndRunningAnimations()
    {
        GetComponent<Animator>().SetBool("IsWalk", false);
        GetComponent<Animator>().SetBool("IsRun", false);
    }

    public void PlayRunAnimation()
    {
        GetComponent<Animator>().SetBool("IsWalk", true);
        GetComponent<Animator>().SetBool("IsRun", true);
    }

    private void TryChangingAnimationTo(string clipName)
    {
        if (currentAnimation == clipName)
        {
            return;
        }

        GetComponent<Animator>().SetTrigger(clipName);
        currentAnimation = clipName;
    }
}
