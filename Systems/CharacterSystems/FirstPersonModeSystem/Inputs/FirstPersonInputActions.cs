// GENERATED AUTOMATICALLY FROM 'Assets/UnityFoundation/Systems/CharacterSystems/FirstPersonModeSystem/Inputs/FirstPersonInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class @FirstPersonInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @FirstPersonInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""FirstPersonInputActions"",
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
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a0f2891d-c221-44fe-ba0f-ad5955d601f4"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""e5fa375d-c45b-4514-bd78-b2aa4be6cc06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""22e7fd89-29cb-462c-8123-0f5f409e8bbe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""452924a7-7752-4c36-bea7-d3b743695114"",
                    ""expectedControlType"": ""Button"",
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
                    ""id"": ""4d849e1f-c966-473d-8248-c037c29f72ef"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                },
                {
                    ""name"": """",
                    ""id"": ""f3d7f970-b75b-4b38-9a2e-95967cfff97e"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f71e7457-8f2f-48ed-ac03-56bd9211c69a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35b53b33-526d-43f0-82f9-3d83b8e02cc3"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Reload"",
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
            m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
            m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
            m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
            m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
            m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
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
        private readonly InputAction m_Player_Jump;
        private readonly InputAction m_Player_Look;
        private readonly InputAction m_Player_Aim;
        private readonly InputAction m_Player_Fire;
        private readonly InputAction m_Player_Reload;
        public struct PlayerActions
        {
            private @FirstPersonInputActions m_Wrapper;
            public PlayerActions(@FirstPersonInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @Jump => m_Wrapper.m_Player_Jump;
            public InputAction @Look => m_Wrapper.m_Player_Look;
            public InputAction @Aim => m_Wrapper.m_Player_Aim;
            public InputAction @Fire => m_Wrapper.m_Player_Fire;
            public InputAction @Reload => m_Wrapper.m_Player_Reload;
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
                    @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                    @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                    @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                    @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                    @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                    @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                    @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                    @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                    @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                    @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                    @Aim.started += instance.OnAim;
                    @Aim.performed += instance.OnAim;
                    @Aim.canceled += instance.OnAim;
                    @Fire.started += instance.OnFire;
                    @Fire.performed += instance.OnFire;
                    @Fire.canceled += instance.OnFire;
                    @Reload.started += instance.OnReload;
                    @Reload.performed += instance.OnReload;
                    @Reload.canceled += instance.OnReload;
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
            private @FirstPersonInputActions m_Wrapper;
            public CursorActions(@FirstPersonInputActions wrapper) { m_Wrapper = wrapper; }
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
            void OnJump(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
            void OnAim(InputAction.CallbackContext context);
            void OnFire(InputAction.CallbackContext context);
            void OnReload(InputAction.CallbackContext context);
        }
        public interface ICursorActions
        {
            void OnLock(InputAction.CallbackContext context);
            void OnRelease(InputAction.CallbackContext context);
        }
    }
}
