using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ClotheslineCarnage
{
    public class GameEntity : MonoBehaviour
    {
        protected Rigidbody2D rigidbody_2D;

        protected List<GameObject> colliders = new List<GameObject>();

        protected float Elasticity = 0.4f;

        protected virtual void Awake()
        {
            rigidbody_2D = this.GetComponent<Rigidbody2D>();
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
            {
                colliders.RemoveAll(x => x.gameObject == other.gameObject);
            }
        }

        protected virtual void OnTriggerStay2D(Collider2D other)
        {
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
            {
                
                Debug.Log(this.name + "'s velocity before is: " + rigidbody_2D.velocity.ToString());
                HandleCollision((Vector2)other.transform.position);
                Debug.Log(this.name + "'s velocity after is: " + rigidbody_2D.velocity.ToString());

                colliders.Add(other.gameObject);
            }
        }

        protected void HandleCollision(Vector2 position, float multi = 1)
        {
            var magnetude = rigidbody_2D.velocity.magnitude;
            var normal = rigidbody_2D.velocity.normalized;
            float deflection = Vector2.Angle(normal, ((Vector2)transform.position - position).normalized);
            Debug.Log("Deflection is " + deflection.ToString());
            float momentumPreservation = multi;
            if(deflection > 90)
            {
                deflection = (180 - deflection) / 90f;
                momentumPreservation *= (1f - Elasticity) * deflection + Elasticity;
            }

            rigidbody_2D.velocity = Vector2.Reflect(normal, ((Vector2)transform.position -position ).normalized) * (magnetude * momentumPreservation);
            
        }
    }
}
