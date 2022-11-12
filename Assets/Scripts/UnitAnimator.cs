using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    protected AnimatorOverrideController animatorOverrideController;

    private Animator _animator;
    
    public void SetOverrides(AnimationOverridesDictionary overrides)
    {
        _animator = GetComponent<Animator>();
        animatorOverrideController =
            new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = animatorOverrideController;
        foreach (KeyValuePair<string, AnimationClip> overriders in overrides)
        {
            animatorOverrideController[overriders.Key] = overriders.Value;
        }   
    }

    public void PlayIdle()
    {
        _animator.Play("Idle");
    }

    public void PlayAttack()
    {
        _animator.Play("Attack");
    }

    public void PlayTakeDamage()
    {
        _animator.Play("TakeDamage");
    }
}
