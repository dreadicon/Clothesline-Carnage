using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClotheslineCarnage
{

    public class LevelManager : Singleton<LevelManager>
    {
        public static int GroundMask = 0;

        public GameMode gameMode;



        public float levelDieBottom = -6;
        public float levelDieLeft = -10;
        public float levelDieRight = 10;

        // Use this for initialization
        void Awake()
        {
            GroundMask |= (1 << LayerMask.NameToLayer("Ground"));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


