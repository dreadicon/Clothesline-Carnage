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
        private AttackPrototype normalAttack;
        [SerializeField]
        private AttackPrototype heavyAttack;
        //private float jumpTime = 0;
        private float chargeTime = 0;
        private float cooldown = 0;
        
        const float groundedRadius = .2f;
        private bool facingRight = true; //False means character is facing left

        protected override void Awake()
        {
            base.Awake();
        }

        protected void Update()
        {
            if (!isLocalPlayer && ((rigidbody_2D.velocity.x > 0.01f && !facingRight) || (rigidbody_2D.velocity.x < 0 && facingRight))) Flip();
        }

        protected void Start()
        {
            speed = LevelManager.Instance.playerSpeed;
            Elasticity = LevelManager.Instance.playerElasticity;
            jumpForce = LevelManager.Instance.playerJumpForce;
            heavyAttackChargeTime = LevelManager.Instance.heavyAttackChargeTime;
        }

        private void FixedUpdate()
        {
            if (cooldown > 0) cooldown--;
        }

        public void Charge()
        {
            CmdCharge();
        }

        [Command]
        public void CmdCharge()
        {
            chargeTime = Time.timeSinceLevelLoad;
        }

        public void Attack()
        {
            CmdAttack();
        }

        [ClientRpc]
        public void RpcTakeHit(Vector2 force)
        {
            rigidbody_2D.AddForce(force);
        }

        [Command]
        public void CmdAttack()
        {
            if (!isServer) return;
            if (cooldown <= 0)
            {
                if ((Time.timeSinceLevelLoad - chargeTime) < heavyAttackChargeTime)
                {
                    Attack(AttackType.Normal, normalAttack.getRadius(), normalAttack.force);
                    Debug.Log("Normal Attack. Charge time: " + (Time.timeSinceLevelLoad - chargeTime).ToString());
                }
                else
                {
                    Attack(AttackType.Heavy, heavyAttack.getRadius(), heavyAttack.force);
                    Debug.Log("Heavy Attack. Charge time: " + (Time.timeSinceLevelLoad - chargeTime).ToString());
                }
                cooldown = globalAttackCooldown;
            }
            chargeTime = 0;
        }

        private void Attack(AttackType attackType, float radius, float force)
        {
            RpcAttackEffect(attackType);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, (1 << transform.gameObject.layer));
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    var characterComponent = colliders[i].gameObject.GetComponent<PlatformerCharacter2D>();
                    if(characterComponent != null)
                        characterComponent.RpcTakeHit((colliders[i].gameObject.transform.position - transform.position).normalized * force);
                }
            }
            
        }

        [ClientRpc]
        private void RpcAttackEffect(AttackType attackType)
        {
            if (attackType == AttackType.Normal)
                normalAttack.AttackVisual();
            else if (attackType == AttackType.Heavy)
                heavyAttack.AttackVisual();
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
