using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

namespace ClotheslineCarnage
{
    [Flags]
    public enum AttackType
    {
        None = 0,
        Player = 2,
        NPC = 4,
        Object = 8,
        Force = 16
    }

    public abstract class AttackPrototype : NetworkBehaviour
    {
        public float force = 20; //Force to apply to character
        public int chargeTime = 0;
        public abstract void AttackVisual();
        public abstract float getRadius();
        public abstract void Attack(PlatformerCharacter2D character);
    }

    public class ShockwaveAttack : AttackPrototype
    {
        private ParticleSystem pSystem;
        private float animationRadius = 0.5f;
        private float effectiveRadius = 0.5f;

        public override float getRadius()
        {
            return effectiveRadius;
        }


        public float radius = 1.3f; //Radius as percent of character size



        // Use this for initialization
        protected virtual void Start()
        {
            pSystem = transform.GetComponent<ParticleSystem>();
            var playerRadius = transform.parent.GetComponent<CircleCollider2D>().radius;
            effectiveRadius = radius * playerRadius;
        }



        public override void AttackVisual()
        {
            pSystem.Emit(1);
        }
    }
}


