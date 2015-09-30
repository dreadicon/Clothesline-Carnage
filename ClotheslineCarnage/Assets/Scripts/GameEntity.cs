using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ClotheslineCarnage
{
    public class GameEntity : MonoBehaviour
    {
        private Rigidbody2D rigidbody_2D;

        protected List<GameObject> colliders = new List<GameObject>();

        public float Elasticity = 0.4f;

        public float xVelocity;

        protected virtual void Awake()
        {
            rigidbody_2D = this.GetComponent<Rigidbody2D>();
        }

        protected virtual void Update()
        {
            xVelocity = rigidbody_2D.velocity.x;
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
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
            {
                //Debug.Log("Distance:" + Vector3.Distance(transform.position, other.transform.position));
                
                //HandleCollision(other.transform.position, 0.2f/Vector3.Distance(transform.position, other.transform.position));
            }
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
            Debug.Log(this.name + "'s normal to the other is: " + (position - (Vector2)transform.position).normalized.ToString());
            float deflection = Vector2.Angle(normal, ((Vector2)transform.position - position).normalized);
            Debug.Log("Deflection is " + deflection.ToString());
            float momentumPreservation = 1;
            if(deflection > 90)
            {
                deflection = deflection / 90f;
                //momentumPreservation = (Elasticity - 1f) * deflection + (1f - Elasticity);
            }
            momentumPreservation *= multi;
            rigidbody_2D.velocity = Vector2.Reflect(normal, ((Vector2)transform.position -position ).normalized) * (magnetude * momentumPreservation);
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
