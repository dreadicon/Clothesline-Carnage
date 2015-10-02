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

        private Dictionary<AttackType, AttackPrototype> attackEffects = new Dictionary<AttackType, AttackPrototype>();
        //private float jumpTime = 0;
        private float chargeTime = 0;
        private float cooldown = 0;
        
        const float groundedRadius = .2f;
        private bool facingRight = true; //False means character is facing left

        protected override void Awake()
        {
            base.Awake();
            // Setting up references.
            attackEffects[AttackType.Normal] = transform.Find("NormalAttack").GetComponent<ShockwaveAttack>();
            attackEffects[AttackType.Heavy] = transform.Find("HeavyAttack").GetComponent<ShockwaveAttack>();
        }

        protected void Update()
        {
            if (!isLocalPlayer && ((rigidbody_2D.velocity.x > 0 && !facingRight) || (rigidbody_2D.velocity.x < 0 && facingRight))) Flip();
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
            chargeTime += 1;
        }

        public void Attack()
        {
            if (cooldown <= 0)
            {
                CmdAttack(chargeTime);
                cooldown = globalAttackCooldown;
            }
            chargeTime = 0;
        }

        [Command]
        public void CmdAttack(float chargeTimeAmount)
        {
                if (chargeTimeAmount < heavyAttackChargeTime)
                {
                    attackEffects[AttackType.Normal].Attack();
                attackEffects[AttackType.Normal].RpcAttackVisual();
                    Debug.Log("Normal Attack. Charge time: " + chargeTime.ToString());

                }
                else
                {
                    attackEffects[AttackType.Heavy].Attack();
                attackEffects[AttackType.Heavy].RpcAttackVisual();
                Debug.Log("Heavy Attack");
                }
                
            
                
            
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
            //WARNING: this could theoretically be exploited to double-jump. added max speed check as fix, but could still be scenarios where this becomes a problem.
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
