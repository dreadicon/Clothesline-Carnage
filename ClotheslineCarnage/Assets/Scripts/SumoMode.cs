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
            base.GameStart();
        }

        public override void GameLoad()
        {
            base.GameLoad();
            gameTimeRemaining = maxGameTime;
        }

        // Update is called once per frame
        public override void GameUpdate()
        {
            gameTimeRemaining = maxGameTime - Time.timeSinceLevelLoad;
        }
    }
}
