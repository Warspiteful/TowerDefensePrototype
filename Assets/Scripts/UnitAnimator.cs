using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    protected AnimatorOverrideController animatorOverrideController;

    private Animator _animator;
    private SpriteRenderer _renderer;

    
    public void SetOverrides(AnimationOverridesDictionary overrides)
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

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
        Debug.Log("TAKEN DAMAGE in Animator");
        _animator.Play("TakeDamage");
    }
    
}
