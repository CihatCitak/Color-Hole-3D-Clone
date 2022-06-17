using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] InputSettings inputSettings;

        private Vector2 inputDrag;
        private Vector2 previousMousePosition;

        void Update()
        {
            inputSettings.InputDrag = HandleInput();
        }

        private Vector2 mousePositionCM
        {
            get
            {
                Vector2 pixels = Input.mousePosition;
                var inches = pixels / Screen.dpi;
                var centimeter = inches * 2.54f;

                return centimeter;
            }
        }

        private Vector2 HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousMousePosition = mousePositionCM;
            }

            if (Input.GetMouseButton(0))
            {
                var deltaMouse = mousePositionCM - previousMousePosition;
                inputDrag = deltaMouse;
                previousMousePosition = mousePositionCM;

                return inputDrag;
            }
            else
            {
                return inputDrag = Vector2.zero;
            }
        }
    }
}