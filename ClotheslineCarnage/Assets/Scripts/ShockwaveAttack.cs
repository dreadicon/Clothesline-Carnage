using UnityEngine;
using System.Collections;

public enum AttackType
{
    None,
    Normal,
    Heavy,
    NonPlayer,
    Tagging
}

public abstract class AttackPrototype : MonoBehaviour
{
    public abstract void Attack();
}

[RequireComponent(typeof(ParticleSystem))]
public class ShockwaveAttack : AttackPrototype
{
    private ParticleSystem pSystem;
    private float animationRadius = 0.5f;
    private float effectiveRadius = 0.5f;

    public float force = 20; //Force to apply to character
    public float radius = 1.3f; //Radius as percent of character size
    


	// Use this for initialization
	protected virtual void Start () {
        pSystem = transform.GetComponent<ParticleSystem>();
        var playerRadius = transform.parent.GetComponent<CircleCollider2D>().radius;
        effectiveRadius = radius * playerRadius;
	}

    public override void Attack()
    {
            pSystem.Emit(1);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.parent.position, effectiveRadius, (1 << transform.parent.gameObject.layer));
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    colliders[i].gameObject.GetComponent<Rigidbody2D>().AddForce((colliders[i].gameObject.transform.position - transform.position).normalized * force);
                }
            }
        

    }
}
