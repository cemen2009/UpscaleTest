using UnityEngine;

public struct Damage
{
    public int amount;
    public float pushForce;
    public Vector3 origin;

    public Damage(int amount, float pushForce, Vector3 origin)
    {
        this.amount = amount;
        this.pushForce = pushForce;
        this.origin = origin;
    }
}