/* Inspired by Unity Standard assets file by the same name.
 *
*/

using System;
using UnityEngine;

namespace ClotheslineCarnage
{
    public enum AttackTypes
    {
        Charging,
        Normal,
        Strong
    }

    public class PlatformerCharacter2D : GameEntity
    {
        [SerializeField] private float m_MaxSpeed = 8f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 200f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField]
        private float heavyAttackChargeTime = 10;
        [SerializeField]
        private float maxJumpTime = 3;
        [SerializeField]
        private float speed = 16;

        private ParticleSystem normal;

        

        private float jumpTime = 0;
        private float chargeTime = 0;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        protected override void Awake()
        {
            base.Awake();
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            normal = transform.Find("NormalAttack").GetComponent<ParticleSystem>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }

        }



        public void Charge()
        {
            chargeTime += Time.deltaTime;
        }

        public void Attack()
        {
            if(chargeTime < heavyAttackChargeTime)
            {
                normal.Stop();
                normal.Play();
            }
            else
            {
                normal.Stop();
                normal.Play();
            }
            chargeTime = 0;
            Debug.Log("Attack up");
        }



        public void Move(float move, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (true)
            {

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                //m_Anim.SetFloat("Speed", Mathf.Abs(move));
                /*
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject && colliders[i].CompareTag(tag))
                    {
                        if (colliders[i].transform.position.x > transform.position.x)
                        {
                            if (move > 0) return;
                        }
                        else if (move < 0) return; 
                        //TODO: add case for jumping
                    }
                        
                }
                */

                // Move the character
                    if (move < 0 && m_Rigidbody2D.velocity.x > -m_MaxSpeed)
                            m_Rigidbody2D.AddForce(new Vector2(-speed, 0));
                    else if (move > 0 && m_Rigidbody2D.velocity.x < m_MaxSpeed)
                        m_Rigidbody2D.AddForce(new Vector2(speed, 0));
                    if(colliders.Count > 0)
                    {
                        foreach(var collision in colliders)
                        {
                            m_Rigidbody2D.AddForce((transform.position - collision.transform.position).normalized * speed);
                        }
                    }
                
                

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                //m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
