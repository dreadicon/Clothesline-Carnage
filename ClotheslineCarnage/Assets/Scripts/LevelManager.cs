using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClotheslineCarnage
{

    public class LevelManager : MonoBehaviour
    {
        public static int GroundMask = 0;

        public static LevelManager Instance = null;

        public GameMode gameMode;

        public float playerElasticity = 0.5f;
        public float playerSpeed = 10;
        public float playerJumpForce = 200;

        public float normalAttackForce = 20;
        public float heavyAttackForce = 100;

        public float heavyAttackChargeTime = 30;

        public float levelDieBottom = -6;
        public float levelDieLeft = -10;
        public float levelDieRight = 10;

        // Use this for initialization
        void Awake()
        {
            GroundMask |= (1 << LayerMask.NameToLayer("Ground"));
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


