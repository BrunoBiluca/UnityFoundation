// GENERATED AUTOMATICALLY FROM 'Assets/UnityFoundation/Systems/Camera/FreeCamera/FreeCameraInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace UnityFoundation.Camera.FreeCamera
{
    public class @FreeCameraInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @FreeCameraInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""FreeCameraInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""706e07b0-7cc8-4156-bcbb-a23a400df270"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""15643067-74e1-4bac-85ec-51200973f80c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""3feec7ca-f32c-4f99-9e70-846a82e31fde"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""d99c6673-ddc2-4545-b506-87fb559ef959"",
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
                    ""id"": ""a942f11e-69e5-48f5-9a79-1a87219d6464"",
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
                    ""id"": ""8e480cb0-90d4-4f33-87e1-93398e18b554"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2f1d2a38-262c-4d18-93ff-08634afefa43"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""239d8f6a-5a32-4bd0-adec-6c4d5f28047c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bee0a284-f000-48f7-8cfe-2f0891ad530a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Cursor"",
            ""id"": ""765495a9-b2ad-4b02-93bc-c213556c555b"",
            ""actions"": [
                {
                    ""name"": ""Lock"",
                    ""type"": ""Button"",
                    ""id"": ""a544b597-ccb0-4bb3-89f4-3a14a0d8a530"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Release"",
                    ""type"": ""Button"",
                    ""id"": ""125cbc68-47d2-41f3-b1bc-b8991dad1649"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c2c01d69-8e4f-4772-ac3d-660f6081d48b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""463a2578-75dd-4794-9f96-d71899022636"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
            // Cursor
            m_Cursor = asset.FindActionMap("Cursor", throwIfNotFound: true);
            m_Cursor_Lock = m_Cursor.FindAction("Lock", throwIfNotFound: true);
            m_Cursor_Release = m_Cursor.FindAction("Release", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Move;
        private readonly InputAction m_Player_Look;
        public struct PlayerActions
        {
            private @FreeCameraInputActions m_Wrapper;
            public PlayerActions(@FreeCameraInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @Look => m_Wrapper.m_Player_Look;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // Cursor
        private readonly InputActionMap m_Cursor;
        private ICursorActions m_CursorActionsCallbackInterface;
        private readonly InputAction m_Cursor_Lock;
        private readonly InputAction m_Cursor_Release;
        public struct CursorActions
        {
            private @FreeCameraInputActions m_Wrapper;
            public CursorActions(@FreeCameraInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Lock => m_Wrapper.m_Cursor_Lock;
            public InputAction @Release => m_Wrapper.m_Cursor_Release;
            public InputActionMap Get() { return m_Wrapper.m_Cursor; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CursorActions set) { return set.Get(); }
            public void SetCallbacks(ICursorActions instance)
            {
                if (m_Wrapper.m_CursorActionsCallbackInterface != null)
                {
                    @Lock.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnLock;
                    @Lock.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnLock;
                    @Lock.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnLock;
                    @Release.started -= m_Wrapper.m_CursorActionsCallbackInterface.OnRelease;
                    @Release.performed -= m_Wrapper.m_CursorActionsCallbackInterface.OnRelease;
                    @Release.canceled -= m_Wrapper.m_CursorActionsCallbackInterface.OnRelease;
                }
                m_Wrapper.m_CursorActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Lock.started += instance.OnLock;
                    @Lock.performed += instance.OnLock;
                    @Lock.canceled += instance.OnLock;
                    @Release.started += instance.OnRelease;
                    @Release.performed += instance.OnRelease;
                    @Release.canceled += instance.OnRelease;
                }
            }
        }
        public CursorActions @Cursor => new CursorActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
        }
        public interface ICursorActions
        {
            void OnLock(InputAction.CallbackContext context);
            void OnRelease(InputAction.CallbackContext context);
        }
    }
}
