using System.Collections;
using System.Collections.Generic;
using Interactable;
using SVR;
using SVR.Interactable;
using SVR.Workflow.TriangleFactory;
using SVR.Workflow.TriangleFactory.Scripts.Mechanics;
using SysmetisVR.V1.VRTK_extended;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class TireChangeSim : CustomSim
{
    public int currentTime = 0;

    [SerializeField] private SoundDataTireChangeSim _soundData;

    public SoundDataTireChangeSim soundData => _soundData;
    public AudioSource WrinchAudioSource;
    public AudioSource lifterAudio;
    public int StepIndexToComplete=0;


    [Header("Button On ")]
    public Touchable buttonOn;
    public GameObject buttonOnPartical;
    public GameObject buttonOnObject; 
    public GameObject buttonOnHighlight;
    public Animator carLifter1;
    public Animator carLifter2;
    public Animator carLifter3;
    public Animator carLifter4;


    [Header("Button Off")]
    public Touchable buttonOff;
    public GameObject buttonOffPartical;
    public GameObject buttonOffObject;
    public GameObject buttonOffHighlight;

    [Header("Car Up and Down Handel")]
    public GameObject HandelHighlighter;
    public Animator carUpAnimation;
    public Animator carLifterAnimatoin; 
    public CustomRotatorV2 carUpAndDownHandel;
    public GameObject handelAnimUp;
    public GameObject handelAnimDown;
    public Animator RotationNutAnim;

    [Header("Lug nut variables And TabelTrigger")]

    public BaseItem lugNut1;
    public BaseItem lugNut2;
    public BaseItem lugNut3;
    public BaseItem lugNut4;
    public BaseItem lugNut5;

    public GameObject tyerAttachlugNut1;
    public GameObject tyerAttachlugNut2;
    public GameObject tyerAttachlugNut3;
    public GameObject tyerAttachlugNut4;
    public GameObject tyerAttachlugNut5;

    public TableTrigger tabelTrigger;
    public GameObject tabelPlane;



    [Header("Tire variables")]

    public CustomSnapDropZone oldTireDropZone;
    public GameObject oldTireDrop;
    public CustomSnapDropZone newTireDropZone;

    public BaseItem tireDamaged;
    public BaseItem tireGood;

    public MeshRenderer tireDamagedVisualOnly;
    public MeshRenderer tireDamagedVisualOnly2;
    public Lightonoff TyerMaterial;
    public BaseItem tireReset;


    [Header("Wrench variables")]
    public BaseItem wrench;

    public ExtendedSnapAttachV2 SnapWrench;
    public ExtendedSnapAttachV2 SnapWrench2;
    public ExtendedSnapAttachV2 SnapWrench3;
    public ExtendedSnapAttachV2 SnapWrench4;
    public ExtendedSnapAttachV2 SnapWrench5;

    public CustomSnapDropZone nut1DropZoneTire;
    public CustomSnapDropZone nut2DropZoneTire;
    public CustomSnapDropZone nut3DropZoneTire;
    public CustomSnapDropZone nut4DropZoneTire;
    public CustomSnapDropZone nut5DropZoneTire;

    #region Initialization

    protected override void Start()
    {
        base.Start();
        EnableRightPointer(true);
    }

    public void CarLifterDownAnimation()
    {
        carUpAnimation.SetBool("a", true);
        carLifterAnimatoin.SetBool("a", true);
    }
    public void LifterArmsRotation()
    {
        carLifter1.SetBool("a", true);
        carLifter2.SetBool("a", true);
        carLifter3.SetBool("a", true);
        carLifter4.SetBool("a", true);

    }
    public void sceneload()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
    #endregion
}
