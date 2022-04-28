//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputSystem/PlayerActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Player_Map"",
            ""id"": ""84cbb4c8-1bce-41bc-a087-53b67f792fa2"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ede9a44c-4f4f-4549-a2c6-4d42c7689921"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5df9fda9-02ca-489a-9128-3b448551fc51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LedgeHang"",
                    ""type"": ""Button"",
                    ""id"": ""7f3c40cc-3909-464d-9f6e-5c98a1db10e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CordHang"",
                    ""type"": ""Value"",
                    ""id"": ""6a66a3a0-c17f-45e9-9297-8561b2652916"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Swimming"",
                    ""type"": ""Value"",
                    ""id"": ""07b4b960-a051-4ff4-8fca-f0c71af06bba"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Diving"",
                    ""type"": ""Value"",
                    ""id"": ""92a29ee5-dfab-4745-8870-bacbdd5fe1dc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a92db2d6-be56-482f-b215-1e21582aa447"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""13ba935d-8e4f-49ab-a07f-78a601df635b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6917b89c-5b6d-4fd1-9419-9f1a5316bac7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""85be1a47-add0-47f4-ba3f-2c1aaf406b2a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67178273-9a8a-40be-80de-00cb24ed1d46"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LedgeHang"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""423a0a27-de5f-4a5d-884f-4ca83d465b60"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LedgeHang"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6973695f-604b-4779-abd1-a0f326dda88d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CordHang"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a30b3cb8-9633-4f1a-b87d-7b779bfd63c0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CordHang"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4da862b2-b2a1-4f0e-91fd-4b9d89256d4c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CordHang"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e7cacb87-18c5-408f-a65e-8398cdb9f041"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swimming"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""879c8076-9fec-4a34-9fca-85d4ebcb6a82"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swimming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e705dfbb-831b-49e2-9069-514d28beadfa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swimming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9aa3a740-3453-484e-b6e5-94276c27c033"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Diving"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8d5253a0-1ae8-4681-af51-aa12104e67b3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Diving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""be917b1e-0933-4a4e-99e3-07dedda55e6a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Diving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4693aa37-bd97-423d-903b-63b6daad9108"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Diving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bdb42cb0-4258-4c54-89b6-bdaa6dd113dc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Diving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_Map
        m_Player_Map = asset.FindActionMap("Player_Map", throwIfNotFound: true);
        m_Player_Map_Movement = m_Player_Map.FindAction("Movement", throwIfNotFound: true);
        m_Player_Map_Jump = m_Player_Map.FindAction("Jump", throwIfNotFound: true);
        m_Player_Map_LedgeHang = m_Player_Map.FindAction("LedgeHang", throwIfNotFound: true);
        m_Player_Map_CordHang = m_Player_Map.FindAction("CordHang", throwIfNotFound: true);
        m_Player_Map_Swimming = m_Player_Map.FindAction("Swimming", throwIfNotFound: true);
        m_Player_Map_Diving = m_Player_Map.FindAction("Diving", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player_Map
    private readonly InputActionMap m_Player_Map;
    private IPlayer_MapActions m_Player_MapActionsCallbackInterface;
    private readonly InputAction m_Player_Map_Movement;
    private readonly InputAction m_Player_Map_Jump;
    private readonly InputAction m_Player_Map_LedgeHang;
    private readonly InputAction m_Player_Map_CordHang;
    private readonly InputAction m_Player_Map_Swimming;
    private readonly InputAction m_Player_Map_Diving;
    public struct Player_MapActions
    {
        private @PlayerActions m_Wrapper;
        public Player_MapActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Map_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Map_Jump;
        public InputAction @LedgeHang => m_Wrapper.m_Player_Map_LedgeHang;
        public InputAction @CordHang => m_Wrapper.m_Player_Map_CordHang;
        public InputAction @Swimming => m_Wrapper.m_Player_Map_Swimming;
        public InputAction @Diving => m_Wrapper.m_Player_Map_Diving;
        public InputActionMap Get() { return m_Wrapper.m_Player_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_MapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer_MapActions instance)
        {
            if (m_Wrapper.m_Player_MapActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnJump;
                @LedgeHang.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnLedgeHang;
                @LedgeHang.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnLedgeHang;
                @LedgeHang.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnLedgeHang;
                @CordHang.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnCordHang;
                @CordHang.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnCordHang;
                @CordHang.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnCordHang;
                @Swimming.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnSwimming;
                @Swimming.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnSwimming;
                @Swimming.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnSwimming;
                @Diving.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnDiving;
                @Diving.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnDiving;
                @Diving.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnDiving;
            }
            m_Wrapper.m_Player_MapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LedgeHang.started += instance.OnLedgeHang;
                @LedgeHang.performed += instance.OnLedgeHang;
                @LedgeHang.canceled += instance.OnLedgeHang;
                @CordHang.started += instance.OnCordHang;
                @CordHang.performed += instance.OnCordHang;
                @CordHang.canceled += instance.OnCordHang;
                @Swimming.started += instance.OnSwimming;
                @Swimming.performed += instance.OnSwimming;
                @Swimming.canceled += instance.OnSwimming;
                @Diving.started += instance.OnDiving;
                @Diving.performed += instance.OnDiving;
                @Diving.canceled += instance.OnDiving;
            }
        }
    }
    public Player_MapActions @Player_Map => new Player_MapActions(this);
    public interface IPlayer_MapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLedgeHang(InputAction.CallbackContext context);
        void OnCordHang(InputAction.CallbackContext context);
        void OnSwimming(InputAction.CallbackContext context);
        void OnDiving(InputAction.CallbackContext context);
    }
}
