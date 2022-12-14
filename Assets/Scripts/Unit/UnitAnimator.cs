using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    protected AnimatorOverrideController animatorOverrideController;
    
    [SerializeField] private Animator _animator;
   [SerializeField] private SpriteRenderer _renderer;

    
    
    public void SetOverrides(AnimationOverridesDictionary overrides)
    {
 

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
        StartCoroutine(DamageAnimation());
    }
    
    IEnumerator DamageAnimation()
    {
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        _renderer.material.color = Color.white;
    }

    public void PlayDeath()
    {
        _animator.Play("Death");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    
}
