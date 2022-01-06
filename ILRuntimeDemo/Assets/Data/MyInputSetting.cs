// GENERATED AUTOMATICALLY FROM 'Assets/Data/MyInputSetting.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MyInputSetting : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MyInputSetting()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyInputSetting"",
    ""maps"": [
        {
            ""name"": ""Land"",
            ""id"": ""7b01db37-1d65-4dd5-b04d-4a39333c335d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""914f2748-f12f-4e1f-92f4-fff8c9044598"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""33bfc091-806f-4614-a2f8-075074830c15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""f8f6f287-9fde-4847-8100-8ca616aa5a70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""d788e5ee-a231-4c5b-9487-8bec7dac9330"",
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
                    ""id"": ""d2c99338-098f-420d-952a-6501ceb0766e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f603200c-71b2-460d-a7a5-51a856093cb7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f64c760c-e10e-47aa-8249-a160ea4fed6a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4f031872-961c-4edc-85c6-45dd732d8a2a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ctrl+leftBtn"",
                    ""id"": ""968a5c1f-8eaa-4489-b823-42cdf27199f2"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""cc1a0068-d1a0-4382-aa6f-8c5ec32136f4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""7bf4675c-233f-4ff4-ba0a-eb3c60e8eee0"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b9b2b2b6-5b50-4c0e-a9c6-1e057ad4b7ae"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Water"",
            ""id"": ""fe28fe97-ef61-4e28-bdc3-7ff468f1b75f"",
            ""actions"": [
                {
                    ""name"": ""Swim"",
                    ""type"": ""Button"",
                    ""id"": ""8d07f702-ccb1-4b79-951f-0579cda98118"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""FG"",
                    ""id"": ""6b33eae1-a925-4391-b256-377efaab57a5"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""6210384d-8fa6-4aa5-a0b5-a2d605d08cff"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Swim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""615d5bbe-f761-4ddb-8c04-c9ce18e114e7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Swim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<VirtualMouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Land
        m_Land = asset.FindActionMap("Land", throwIfNotFound: true);
        m_Land_Move = m_Land.FindAction("Move", throwIfNotFound: true);
        m_Land_Fire = m_Land.FindAction("Fire", throwIfNotFound: true);
        m_Land_PickUp = m_Land.FindAction("PickUp", throwIfNotFound: true);
        // Water
        m_Water = asset.FindActionMap("Water", throwIfNotFound: true);
        m_Water_Swim = m_Water.FindAction("Swim", throwIfNotFound: true);
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

    // Land
    private readonly InputActionMap m_Land;
    private ILandActions m_LandActionsCallbackInterface;
    private readonly InputAction m_Land_Move;
    private readonly InputAction m_Land_Fire;
    private readonly InputAction m_Land_PickUp;
    public struct LandActions
    {
        private @MyInputSetting m_Wrapper;
        public LandActions(@MyInputSetting wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Land_Move;
        public InputAction @Fire => m_Wrapper.m_Land_Fire;
        public InputAction @PickUp => m_Wrapper.m_Land_PickUp;
        public InputActionMap Get() { return m_Wrapper.m_Land; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LandActions set) { return set.Get(); }
        public void SetCallbacks(ILandActions instance)
        {
            if (m_Wrapper.m_LandActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Fire.started -= m_Wrapper.m_LandActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnFire;
                @PickUp.started -= m_Wrapper.m_LandActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnPickUp;
            }
            m_Wrapper.m_LandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
            }
        }
    }
    public LandActions @Land => new LandActions(this);

    // Water
    private readonly InputActionMap m_Water;
    private IWaterActions m_WaterActionsCallbackInterface;
    private readonly InputAction m_Water_Swim;
    public struct WaterActions
    {
        private @MyInputSetting m_Wrapper;
        public WaterActions(@MyInputSetting wrapper) { m_Wrapper = wrapper; }
        public InputAction @Swim => m_Wrapper.m_Water_Swim;
        public InputActionMap Get() { return m_Wrapper.m_Water; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WaterActions set) { return set.Get(); }
        public void SetCallbacks(IWaterActions instance)
        {
            if (m_Wrapper.m_WaterActionsCallbackInterface != null)
            {
                @Swim.started -= m_Wrapper.m_WaterActionsCallbackInterface.OnSwim;
                @Swim.performed -= m_Wrapper.m_WaterActionsCallbackInterface.OnSwim;
                @Swim.canceled -= m_Wrapper.m_WaterActionsCallbackInterface.OnSwim;
            }
            m_Wrapper.m_WaterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Swim.started += instance.OnSwim;
                @Swim.performed += instance.OnSwim;
                @Swim.canceled += instance.OnSwim;
            }
        }
    }
    public WaterActions @Water => new WaterActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface ILandActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
    }
    public interface IWaterActions
    {
        void OnSwim(InputAction.CallbackContext context);
    }
}
