using UnityEngine;

public class UnitData : ScriptableObject
{

  
    [Header("Audio")]
    public FMODUnity.EventReference AttackSound;
    public string name;
    public int atkPower;
    //public int atkSpeed;
    public int def;
    public int health;
    public Vector2 range;
    public Sprite sprite;

    [Header("Animation")]
    public AnimationOverridesDictionary animationOverrides;

    public float GetAnimationDuration(string animation)
    {
        return animationOverrides[animation].length;
    }
        
}
