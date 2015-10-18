using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace ClotheslineCarnage
{
    public abstract class GameMode : MonoBehaviour
    {
        public GameObject playerPrefab;

        public Teams teamTypes;

        public float playerElasticity = 0.5f;
        public float playerSpeed = 10;
        public float playerJumpForce = 200;

        public float specialAttackChargeTime = 30;
        public float attackGlobalCooldown = 1;

        public float normalForce = 20;
        public float specialForce = 100;
        public float normalRange = 0.5f;
        public float specialRange = 1.0f;

        protected float normalRangeScaled = 0;
        protected float specialRangeScaled = 0;

        protected int playerLayerMask;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject.transform);
            if(playerPrefab != null)
            {
                var radius = playerPrefab.GetComponent<CircleCollider2D>().radius;
                normalRangeScaled = normalRange * radius;
                specialRangeScaled = specialRange * radius;
                playerLayerMask = 1 << playerPrefab.gameObject.layer;
            }
            
        }

        //TODO: load this from a text file or somewhere other than code
        public abstract string modeName();
        public abstract string description();

        public virtual void GameStart()
        {

        }

        public virtual void GameUpdate()
        {

        }

        public virtual void GameLoad()
        {

        }

        public virtual void GameEnd()
        {

        }

        public virtual void CharacterUpdate(PlatformerCharacter2D character)
        {
            if (character.chargeTime > 0)
            {
                //character.
            }
        }

        public virtual void CharacterCharge(PlatformerCharacter2D character)
        {

        }

        public virtual void CharacterAttack(PlatformerCharacter2D character)
        {
            if (character.cooldown <= 0)
            {
                if ((Time.timeSinceLevelLoad - character.chargeTime) < specialAttackChargeTime)
                {
                    NormalAttack(character);
                    Debug.Log("Normal Attack. Charge time: " + (Time.timeSinceLevelLoad - character.chargeTime).ToString());
                }
                else
                {
                    SpecialAttack(character);
                    Debug.Log("Special Attack. Charge time: " + (Time.timeSinceLevelLoad - character.chargeTime).ToString());
                }
                character.cooldown = attackGlobalCooldown;
            }
            character.chargeTime = 0;
        }

        public virtual void NormalAttack(PlatformerCharacter2D character)
        {
            character.RpcAttackEffect(true);
            ShockwaveAttack(character.transform.position, normalRangeScaled);
        }

        public virtual void SpecialAttack(PlatformerCharacter2D character)
        {
            character.RpcAttackEffect(false);
            ShockwaveAttack(character.transform.position, normalRangeScaled);
        }

        protected virtual void ShockwaveAttack(Vector3 position, float range)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range, playerLayerMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    colliders[i].gameObject.GetComponent<PlatformerCharacter2D>().RpcTakeHit((colliders[i].gameObject.transform.position - position).normalized * normalForce);
                }
            }
        }
    }
}


