using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("S1D1")]
    [field: SerializeField] public EventReference S1D1 { get; private set; }

    [field: Header("S1D2")]
    [field: SerializeField] public EventReference S1D2 { get; private set; }

    [field: Header("S1D3")]
    [field: SerializeField] public EventReference S1D3 { get; private set; }

    [field: Header("G1D1")]
    [field: SerializeField] public EventReference G1D1 { get; private set; }

    [field: Header("G1D2")]
    [field: SerializeField] public EventReference G1D2 { get; private set; }

    [field: Header("G1D3")]
    [field: SerializeField] public EventReference G1D3 { get; private set; }

    [field: Header("S2D1")]
    [field: SerializeField] public EventReference S2D1 { get; private set; }

    [field: Header("S2D2")]
    [field: SerializeField] public EventReference S2D2 { get; private set; }

    [field: Header("S2D3")]
    [field: SerializeField] public EventReference S2D3 { get; private set; }

    [field: Header("S2D4")]
    [field: SerializeField] public EventReference S2D4 { get; private set; }

    [field: Header("Footstep SFX")]
    [field: SerializeField] public EventReference footsteps { get; private set; }   
    
    [field: Header("Ambience SFX")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Button Press SFX")]
    [field: SerializeField] public EventReference buttonPress { get; private set; }

    [field: Header("Scream SFX")]
    [field: SerializeField] public EventReference scream { get; private set; }

    [field: Header("Armor Jump")]
    [field: SerializeField] public EventReference armorJump { get; private set; }

    [field: Header("Jump SFX")]
    [field: SerializeField] public EventReference jump { get; private set; }

    [field: Header("Sword Swing")]
    [field: SerializeField] public EventReference swordSwing { get; private set; }

    [field: Header("Wood Slicing")]
    [field: SerializeField] public EventReference woodSlicing { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
