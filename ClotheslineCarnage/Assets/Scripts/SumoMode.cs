using UnityEngine;
using System.Collections;
using System;

namespace ClotheslineCarnage
{
    public class SumoMode : GameMode
    {
        public float maxGameTime = 60;
        public float gameTimeRemaining;

        public override void GameStart()
        {
            
        }

        public override void GameLoad()
        {
            gameTimeRemaining = maxGameTime;
        }

        // Use this for initialization
        void Awake()
        {
            
        }

        // Update is called once per frame
        public override void GameUpdate()
        {
            gameTimeRemaining -= Time.deltaTime;
        }
    }
}
