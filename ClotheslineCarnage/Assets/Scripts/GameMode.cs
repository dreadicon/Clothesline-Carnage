using UnityEngine;
using System.Collections;

namespace ClotheslineCarnage
{
    public abstract class GameMode : MonoBehaviour
    {
        public Teams[] teams;
        public string modeName;
        public string description;

        LevelManager manager;

        public abstract void GameStart();

        public abstract void GameUpdate();

        public abstract void GameLoad();
    }
}


