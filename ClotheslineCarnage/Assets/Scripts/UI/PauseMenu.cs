using UnityEngine;
using System.Collections;

namespace ClotheslineCarnage
{
    public class PauseMenu : Menu
    {
        protected override void Awake()
        {
            base.Awake();
            this.Hide();
        }

        protected override void Update()
        {
            base.Update();
        }

        public override void Back()
        {

        }


        public void ReturnToMainMenu()
        {
            Application.LoadLevel("MainMenu");
        }

    }
}


