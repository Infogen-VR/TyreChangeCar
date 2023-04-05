using Interactable;
using SVR.Interactable;
using SVR.Workflow.TriangleFactory;
using SVR.Workflow.TriangleFactory.Scripts.Mechanics;
using UnityEngine;

public class TireChangeSim : CustomSim
{
    [SerializeField] private SoundDataTireChangeSim _soundData;

    public SoundDataTireChangeSim soundData => _soundData;

    [Header("Car jack variables")]
    public BaseItem carJack;

    public CustomSnapDropZone jackDropZoneUnderCar;
    public CustomSnapDropZone jackDropZoneOriginalPos;

    public CustomRotatorV2 carJackHandlePushRotator;

    [Header("Lug nut variables")]

    public BaseItem lugNut1;
    public BaseItem lugNut2;
    public BaseItem lugNut3;
    public BaseItem lugNut4;

    public CustomSnapDropZone nut1DropZoneTable;
    public CustomSnapDropZone nut2DropZoneTable;
    public CustomSnapDropZone nut3DropZoneTable;
    public CustomSnapDropZone nut4DropZoneTable;

    public CustomSnapDropZone nut1DropZoneTire;
    public CustomSnapDropZone nut2DropZoneTire;
    public CustomSnapDropZone nut3DropZoneTire;
    public CustomSnapDropZone nut4DropZoneTire;


    [Header("Tire variables")]

    public CustomSnapDropZone oldTireDropZone;
    public CustomSnapDropZone newTireDropZone;

    public BaseItem tireDamaged;
    public BaseItem tireGood;

    public MeshRenderer tireDamagedVisualOnly;


    [Header("Wrench variables")]
    public BaseItem wrench;

    public CustomRotatorV2 nut1LooseRotator;
    public CustomRotatorV2 nut2LooseRotator;
    public CustomRotatorV2 nut3LooseRotator;
    public CustomRotatorV2 nut4LooseRotator;
    public CustomRotatorV2 nut1TightenRotator;
    public CustomRotatorV2 nut2TightenRotator;
    public CustomRotatorV2 nut3TightenRotator;
    public CustomRotatorV2 nut4TightenRotator;
    public CustomRotatorV2 carJackLowerRotator;

    public CustomSnapDropZone wrenchDropZoneNut1;
    public CustomSnapDropZone wrenchDropZoneNut2;
    public CustomSnapDropZone wrenchDropZoneNut3;
    public CustomSnapDropZone wrenchDropZoneNut4;
    public CustomSnapDropZone wrenchDropZoneTable;

    public MovableMainPiston jackLeverPush;

    #region Initialization

    protected override void Start()
    {
        base.Start();
        EnableRightPointer(true);
    }
    #endregion
}
