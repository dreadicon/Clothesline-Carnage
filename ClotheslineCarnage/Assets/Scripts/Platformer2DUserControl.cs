using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace ClotheslineCarnage
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : NetworkBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool attack;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!isLocalPlayer) return;
            // Read the jump/attack input in Update so button presses aren't missed.
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            if(!attack)
            {
                attack = CrossPlatformInputManager.GetButtonUp("Attack");
            }
        }


        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            // Read the inputs.
            if(attack)
            {
                m_Character.Attack();
                attack = false;
            }
            else if(CrossPlatformInputManager.GetButton("Attack"))
            {
                m_Character.Charge();
                
            }
            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, m_Jump);
            m_Jump = false;
        }
    }
}
