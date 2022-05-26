// GENERATED AUTOMATICALLY FROM 'Assets/UnityFoundation/Systems/CarSystem/car_input_actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Assets.UnityFoundation.Systems.CarSystem
{
    public class @CarInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @CarInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""car_input_actions"",
    ""maps"": [
        {
            ""name"": ""Car"",
            ""id"": ""d8d83ea3-13de-457c-9acd-27a98f972dbe"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""657b4a9c-300a-480f-bfac-7c62f8ee5ddc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Value"",
                    ""id"": ""8355b24e-d5de-494e-9095-caf42b4458c0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""ws"",
                    ""id"": ""799aaa01-d5ac-44f8-9756-6f736c55f198"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""00f1b8c5-fb09-4295-9470-0d52fa52b108"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a319a4b6-0d4a-4f29-b86e-3587dba9a6ea"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ad"",
                    ""id"": ""4c9246ba-0693-4a00-b10d-e499040678eb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""903e3bf5-f280-4d11-9915-9753126da2f5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""42182b4d-ab3d-4740-b5dc-7f5448236bb0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": []
        }
    ]
}");
            // Car
            m_Car = asset.FindActionMap("Car", throwIfNotFound: true);
            m_Car_Move = m_Car.FindAction("Move", throwIfNotFound: true);
            m_Car_Turn = m_Car.FindAction("Turn", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Car
        private readonly InputActionMap m_Car;
        private ICarActions m_CarActionsCallbackInterface;
        private readonly InputAction m_Car_Move;
        private readonly InputAction m_Car_Turn;
        public struct CarActions
        {
            private @CarInputActions m_Wrapper;
            public CarActions(@CarInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Car_Move;
            public InputAction @Turn => m_Wrapper.m_Car_Turn;
            public InputActionMap Get() { return m_Wrapper.m_Car; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CarActions set) { return set.Get(); }
            public void SetCallbacks(ICarActions instance)
            {
                if (m_Wrapper.m_CarActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_CarActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_CarActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_CarActionsCallbackInterface.OnMove;
                    @Turn.started -= m_Wrapper.m_CarActionsCallbackInterface.OnTurn;
                    @Turn.performed -= m_Wrapper.m_CarActionsCallbackInterface.OnTurn;
                    @Turn.canceled -= m_Wrapper.m_CarActionsCallbackInterface.OnTurn;
                }
                m_Wrapper.m_CarActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Turn.started += instance.OnTurn;
                    @Turn.performed += instance.OnTurn;
                    @Turn.canceled += instance.OnTurn;
                }
            }
        }
        public CarActions @Car => new CarActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface ICarActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnTurn(InputAction.CallbackContext context);
        }
    }
}
