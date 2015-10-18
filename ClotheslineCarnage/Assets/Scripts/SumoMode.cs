using UnityEngine;
using System.Collections;
using System;

namespace ClotheslineCarnage
{
    [Prefab("Game Controller", true)]
    public class SumoMode : GameMode
    {
        public float maxGameTime = 60;
        public float gameTimeRemaining;

        public override string modeName()
        {
            return "Sumo Mode";
        }

        public override string description()
        {
            return "A timed match where the player or team with the most opponents KO'ed at the end of the time limit wins!";
        }

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
