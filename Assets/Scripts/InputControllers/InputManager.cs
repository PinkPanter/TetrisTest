using System;
using UnityEngine;

namespace InputControllers
{
    /// <summary>
    /// Invokes input events
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static event Action<int> OnMovedHorizontal = delegate { };

        public static event Action<bool> OnSpeedUp = delegate { };

        public static event Action OnRotate = delegate { };

        [SerializeField]
        private KeyCode speedUpKey = KeyCode.S;
        [SerializeField]
        private KeyCode rotateKey = KeyCode.Space;
        [SerializeField]
        private KeyCode leftKey = KeyCode.A;
        [SerializeField]
        private KeyCode rightKey = KeyCode.D;

        private bool isSpeedUp = false;

        private void Update()
        {
            if (Input.GetKeyDown(leftKey))
            {
                OnMovedHorizontal(-1);
            }
            else if(Input.GetKeyDown(rightKey))
            {
                OnMovedHorizontal(1);
            }

            if (Input.GetKeyDown(rotateKey))
                OnRotate();

            var speedKeyState = Input.GetKey(speedUpKey);

            if (!isSpeedUp && speedKeyState)
            {
                OnSpeedUp(true);
                isSpeedUp = true;
            }

            if(isSpeedUp && !speedKeyState)
            {
                OnSpeedUp(false);
                isSpeedUp = false;
            }


#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Break();
            }
#endif
        }
    }
}
