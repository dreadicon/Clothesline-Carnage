/* Inspired by Unity Standard assets file by the same name.
 *
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ClotheslineCarnage
{


    public class PlatformerCharacter2D : GameEntity
    {
        [SerializeField] private float maxSpeed = 8f;
        private float jumpForce = 200f;
        [SerializeField] private bool airControl = true;
        
        
        private float heavyAttackChargeTime = 60;
        //[SerializeField] private float maxJumpTime = 3;
        private float speed = 16;
        [SerializeField] private float globalAttackCooldown = 40;

        [SerializeField]
        public ParticleSystem normalAttack;
        [SerializeField]
        public ParticleSystem heavyAttack;
        //private float jumpTime = 0;
        public float chargeTime;
        public float cooldown = 0;
        
        const float groundedRadius = .2f;
        private bool facingRight = true; //False means character is facing left

        protected override void Awake()
        {
            base.Awake();
            chargeTime = 0;
            normalAttack = transform.FindChild("NormalAttack").GetComponent<ParticleSystem>();
            heavyAttack = transform.FindChild("HeavyAttack").GetComponent<ParticleSystem>();
        }

        protected void Update()
        {
            if (!isLocalPlayer && ((rigidbody_2D.velocity.x > 0.01f && !facingRight) || (rigidbody_2D.velocity.x < 0 && facingRight))) Flip();
            if (!isServer) return;
            GameMode.current.CharacterUpdate(this);

        }

        protected void Start()
        {
            speed = GameMode.current.playerSpeed;
            Elasticity = GameMode.current.playerElasticity;
            jumpForce = GameMode.current.playerJumpForce;
            heavyAttackChargeTime = GameMode.current.specialAttackChargeTime;
        }

        private void FixedUpdate()
        {
            if (cooldown > 0) cooldown--;
        }

        [Command]
        public void CmdCharge()
        {
            chargeTime = Time.timeSinceLevelLoad;
        }

        [Command]
        public void CmdAttack()
        {
            if (!isServer) return;
            GameMode.current.CharacterAttack(this);
        }

        [ClientRpc]
        public void RpcAttackEffect(bool isNormal)
        {
            if (isNormal)
                normalAttack.Emit(1);
            else 
                heavyAttack.Emit(1);
        }

        [ClientRpc]
        public void RpcTakeHit(Vector2 force)
        {
            rigidbody_2D.AddForce(force);
        }

        public void Move(float move, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (airControl)
            {
                // Move the character
                if (move < 0 && rigidbody_2D.velocity.x > -maxSpeed)
                        rigidbody_2D.AddForce(new Vector2(-speed, 0));
                else if (move > 0 && rigidbody_2D.velocity.x < maxSpeed)
                    rigidbody_2D.AddForce(new Vector2(speed, 0));

                if(colliders.Count > 0)
                {
                    foreach(var collision in colliders)
                    {
                        if(Vector3.Distance(transform.position, collision.transform.position) < 0.28)
                        {
                            rigidbody_2D.velocity = new Vector2(0, 0);
                            rigidbody_2D.AddForce((transform.position - collision.gameObject.transform.position).normalized * speed * 2);
                        }

                    }
                }

                // Flip player left/right
                if ((move > 0 && !facingRight) || (move < 0 && facingRight)) Flip();

            }

            // Handle jump
            if (jump && Physics2D.OverlapCircle(transform.position, groundedRadius, LevelManager.GroundMask) != null && rigidbody_2D.velocity.y < maxSpeed)
            {
                rigidbody_2D.AddForce(new Vector2(0f, jumpForce));
            }
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
