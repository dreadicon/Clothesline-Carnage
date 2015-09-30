using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ClotheslineCarnage
{
    public class EntityCollider : MonoBehaviour
    {
        private Rigidbody2D rigidbody_2D;

        protected virtual void Awake()
        {
            rigidbody_2D = transform.parent.GetComponent<Rigidbody2D>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
            {

                Debug.Log(this.name + "'s velocity before is: " + rigidbody_2D.velocity.ToString());
                HandleCollision((Vector2)other.transform.position);
                Debug.Log(this.name + "'s velocity after is: " + rigidbody_2D.velocity.ToString());

                //colliders.Add(other.gameObject);
            }
        }

        protected void HandleCollision(Vector2 position)
        {
            var repulsorLeft = new Vector2(-2, 0f);
            var repulsorRight = new Vector2(2, 0f);
            Debug.Log(this.name + "'s normal to the other is: " + (position - (Vector2)transform.position).normalized.ToString());
            rigidbody_2D.velocity = Vector2.Reflect(rigidbody_2D.velocity.normalized, ((Vector2)transform.position - position).normalized) * rigidbody_2D.velocity.magnitude;
            if (position.x > transform.position.x)
            {
                //rigidbody_2D.AddForce(repulsorLeft);

            }
            else
            {
                //rigidbody_2D.AddForce(repulsorRight);

            }

        }
    }
}
