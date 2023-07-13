using System;
using System.Collections;
using SVR.Chaining;
using SVR.Workflow.TriangleFactory;
using SVR.Workflow.TriangleFactory.Scripts.Mechanics;
using Transfr.Platform.Fresh;
using UnityEngine;
using VRTK;

namespace SVR.Workflow
{
    [CreateAssetMenu(fileName = "AnimatedStep", menuName = "simulations/TireChange/Steps")]
    public class StepsTireChange : CustomStep<TireChangeSim>
    {
        //public SVRChaining SVRChaining = new SVRChaining();
        
        enum InternalStep
        {
            None = -1,
            IntroAndPressTheOnButton,
            CarLifter,

            GrabImpactWrenchFromTable,
            PlaceWrenchNut1,
            RotateWrenchAnimation1,
            RemoveNut1,
            PlaceNut1OnTable,

            PlaceWrenchNut2,
            RotateWrenchAnimation2,
            RemoveNut2,
            PlaceNut2OnTable,

            PlaceWrenchNut3,
            RotateWrenchAnimation3,
            RemoveNut3,
            PlaceNut3OnTable,

            PlaceWrenchNut4,
            RotateWrenchAnimation4,
            RemoveNut4,
            PlaceNut4OnTable,

            PlaceWrenchNut5,
            RotateWrenchAnimation5,
            RemoveNut5,
            PlaceNut5OnTable,

            PlaceWrenchOnTable,

            RemoveTireFromAxle,
            PlaceTireOnGround,
            GrabNewTire,
            PlaceItOnAxle,

            NowGrabLugNut1,
            PlaceNut1OnTire,
            NowGrabLugNut2,
            PlaceNut2OnTire,
            NowGrabLugNut3,
            PlaceNut3OnTire,
            NowGrabLugNut4,
            PlaceNut4OnTire,
            NowGrabLugNut5,
            PlaceNut5OnTire,

            GrabWrenchAgain,

            WrenchPlaceOnNut1,
            WrenchAnimationTrigger1,
            WrenchPlaceOnNut2,
            WrenchAnimationTrigger2,
            WrenchPlaceOnNut3,
            WrenchAnimationTrigger3,
            WrenchPlaceOnNut4,
            WrenchAnimationTrigger4,
            WrenchPlaceOnNut5,
            WrenchAnimationTrigger5,

            PlaceWrenchOnTabel,
            CarLifterDown,
            PressTheOffButton,



            Congrats
        }

        private void OnDestroy()
        {
            _sim.LeftController.GripPressed -= ValidateGhostModel;
            _sim.RightController.GripPressed -= ValidateGhostModel;
        }

        private void ValidateGhostModel(object sender, ControllerInteractionEventArgs e)
        {
            Debug.Log("sender is " + sender.ToString());
            VRTK_InteractGrab controller = ((VRTK_ControllerEvents)sender).GetComponent<VRTK_InteractGrabExtended>();
            //ConditionCheck(controller);
            //if (controller.HasControllerGrabbedSomething())
            //    Debug.Log("Execute logic here");

        }
        //private async void ConditionCheck(VRTK_InteractGrab controller)
        //{
        //    await Task.Delay(100);
        //    _stepChain = ChainManager.Get(_conditionalCompletion);
        //    switch ((InternalStep)_internalStepHandler.CurrentStepIndex)
        //    {
        //        case InternalStep.PlaceWrenchNut2:
        //            if (controller.HasControllerGrabbedSomething())
        //                _stepChain
        //                    .EnableObject(_sim.wrenchDropZoneNut2.gameObject)
        //                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut2);
        //            break;

        //        case InternalStep.PlaceWrenchNut3:
        //            if (controller.HasControllerGrabbedSomething())
        //                _stepChain
        //                    .EnableObject(_sim.wrenchDropZoneNut3.gameObject)
        //                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut3);
        //            break;

        //        case InternalStep.PlaceWrenchNut4:
        //            if (controller.HasControllerGrabbedSomething())
        //                _stepChain
        //                    .EnableObject(_sim.wrenchDropZoneNut4.gameObject)
        //                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut4);
        //            break;

        //        case InternalStep.PlaceWrenchNut2_Final:
        //            if (controller.HasControllerGrabbedSomething())
        //                _stepChain
        //                    .EnableObject(_sim.wrenchDropZoneNut2.gameObject)
        //                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut2);
        //            break;

        //        case InternalStep.PlaceWrenchNut3_Final:
        //            if (controller.HasControllerGrabbedSomething())
        //                _stepChain
        //                    .EnableObject(_sim.wrenchDropZoneNut3.gameObject)
        //                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut3);
        //            break;

        //        case InternalStep.PlaceWrenchNut4_Final:
        //            if (controller.HasControllerGrabbedSomething())
        //                _stepChain
        //                    .EnableObject(_sim.wrenchDropZoneNut4.gameObject)
        //                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut4);
        //            break;
        //    }
        //}

        public override void OnEnter(ObjectReferences objectRefs, NextAction nextAction = null)
        {
            Debug.Log("Entering: [MainStep]IntroductionAnimated");

            //_sim.LeftController.GripPressed += ValidateGhostModel;
            //_sim.RightController.GripPressed += ValidateGhostModel;
            //UI.ScoreSheet.Instance.SetLevelName("Soups and Sauces");

            base.OnEnter(objectRefs);

            //Init stuff here.
            _internalStepHandler.Initialize(Enum.GetValues(typeof(InternalStep)), _conditionalCompletion);
        }

        protected override void OnInternalStepEnter(InternalStepsHandler sender, ref InternalStepsHandler.EnterStepArgs args)
        {
            _stepChain?.Kill();
            _stepChain = ChainManager.Get(_conditionalCompletion);

            switch ((InternalStep)args.EnteredStepIndex)
            {
                case InternalStep.IntroAndPressTheOnButton:                    
                    _stepChain
                        .Wait(1f)           
                        .PlayCoach(_sim.soundData.Intro)
                        .PlayCoach(_sim.soundData.GreenButtonPress)
                        .HighlightObject(_sim.buttonOnHighlight)
                        .EnableObject(_sim.buttonOnObject)
                        .AddTouchCondition(_sim.buttonOn)
                        .PlayRepeatingReminder(_sim,_sim.soundData.GreenButtonPress,5f)
                        ;
                    break;

                case InternalStep.CarLifter:
                    _stepChain
                        .PlayCoach(_sim.soundData.HandelRotate)
                        .EnableBehaviour(_sim.carUpAndDownHandel)
                        .HighlightObject(_sim.HandelHighlighter)
                        .AddRotationCondition(_sim.carUpAndDownHandel, RotationCriteria.RotationLimitReached.Clockwise)
                        .PlayRepeatingReminder(_sim,_sim.soundData.HandelRotate,5f)
                        ;
                    break;

                case InternalStep.GrabImpactWrenchFromTable:                  
                    _stepChain
                        .PlayCoach(_sim.soundData.GrabWrenchFromTable)
                        .HighlightObject(_sim.wrench)
                        .MakeGrabbable(_sim.wrench)
                        .AddGrabCondition(_sim.wrench)
                        .PlayRepeatingReminder(_sim, _sim.soundData.GrabWrenchFromTable, 5f)
                        ;
                    break;

                case InternalStep.PlaceWrenchNut1:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut1)
                        .EnableObject(_sim.SnapWrench)
                        .AddSnappedCondition(_sim.SnapWrench,_sim.wrench)
                        .PlayRepeatingReminder(_sim,_sim.soundData.PlaceWrenchNut1,5f)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation1:
                    _sim.StepIndexToComplete = 4;
                    TriggerButtonPressedForWrinch();                   
                    _stepChain
                        .PlayCoach(_sim.soundData.PressTriggerLoseTheNut1)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerLoseTheNut1, 5f)
                        ;
                    break;

                case InternalStep.RemoveNut1:
                    _sim.lugNut1.DroppedEvent += LugNut1_DroppedEvent;
                    _stepChain
                        .PlayCoach(_sim.soundData.RemoveNut1)
                        .MakeGrabbable(_sim.lugNut1)
                        .HighlightObject(_sim.lugNut1)
                        .AddGrabCondition(_sim.lugNut1)
                        .PlayRepeatingReminder(_sim,_sim.soundData.RemoveNut1,5f)
                        ;
                    break;

                case InternalStep.PlaceNut1OnTable:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTable)
                        .HighlightObject(_sim.tabelPlane)
                        .AddTableTriggerCondition(_sim.tabelTrigger, _sim.lugNut1)
                        .PlayRepeatingReminder(_sim,_sim.soundData.PlaceNutOnTable,5f)  
                        ;
                    break;


                case InternalStep.PlaceWrenchNut2:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut2)
                        .EnableObject(_sim.SnapWrench2)
                        .AddSnappedCondition(_sim.SnapWrench2, _sim.wrench)
                        .PlayRepeatingReminder(_sim,_sim.soundData.PlaceWrenchNut2,5f)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation2:
                    _sim.StepIndexToComplete = 8;
                    TriggerButtonPressedForWrinch();               
                    _stepChain
                       .PlayCoach(_sim.soundData.PressTriggerLoseTheNut2)
                       .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerLoseTheNut2, 5f)
                        ;
                    break;

                case InternalStep.RemoveNut2:
                    _sim.lugNut2.DroppedEvent += LugNut1_DroppedEvent;
                    _stepChain
                        .EnableObject(_sim.lugNut2)
                        .DisableObject(_sim.tyerAttachlugNut2)
                        .DisableObject(_sim.SnapWrench2);
                    _stepChain
                        .PlayCoach(_sim.soundData.RemoveNut2)
                        .HighlightObject(_sim.lugNut2)
                        .MakeGrabbable(_sim.lugNut2)
                        .AddGrabCondition(_sim.lugNut2)
                        .PlayRepeatingReminder(_sim,_sim.soundData.RemoveNut2,5f)
                        ;
                        break;

                case InternalStep.PlaceNut2OnTable:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTable)
                        .HighlightObject(_sim.tabelPlane)
                        .AddTableTriggerCondition(_sim.tabelTrigger, _sim.lugNut2)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTable, 5f)
                            ;
                    break;

                case InternalStep.PlaceWrenchNut3:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut3)
                        .EnableObject(_sim.SnapWrench3)
                        .AddSnappedCondition(_sim.SnapWrench3, _sim.wrench)
                        .PlayRepeatingReminder(_sim,_sim.soundData.PlaceWrenchNut3,5f)
                        ;
                        break;

                case InternalStep.RotateWrenchAnimation3:
                    _sim.StepIndexToComplete = 12;
                    TriggerButtonPressedForWrinch();               
                    _stepChain
                        .PlayCoach(_sim.soundData.PressTriggerLoseTheNut3)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerLoseTheNut3, 5f)
                        ;
                    break;

                case InternalStep.RemoveNut3:
                    _sim.lugNut3.DroppedEvent += LugNut1_DroppedEvent; 
                    _stepChain
                        .EnableObject(_sim.lugNut3)
                        .DisableObject(_sim.tyerAttachlugNut3)
                        .DisableObject(_sim.SnapWrench3);
                    _stepChain
                        .PlayCoach(_sim.soundData.RemoveNut3)
                        .HighlightObject(_sim.lugNut3)
                        .MakeGrabbable(_sim.lugNut3)                       
                        .AddGrabCondition(_sim.lugNut3)
                        .PlayRepeatingReminder(_sim, _sim.soundData.RemoveNut3, 5f)                        
                        ;
                    break;

                case InternalStep.PlaceNut3OnTable:                    
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTable)
                        .HighlightObject(_sim.tabelPlane)
                        .AddTableTriggerCondition(_sim.tabelTrigger, _sim.lugNut3)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTable, 5f)
                        ;
                    break;

                case InternalStep.PlaceWrenchNut4:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut4)
                        .EnableObject(_sim.SnapWrench4)
                        .AddSnappedCondition(_sim.SnapWrench4, _sim.wrench)
                        .PlayRepeatingReminder(_sim,_sim.soundData.PlaceWrenchNut4,5f)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation4:
                    _sim.StepIndexToComplete = 16;
                    TriggerButtonPressedForWrinch();                   
                    _stepChain
                        .PlayCoach(_sim.soundData.PressTriggerLoseTheNut4)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerLoseTheNut4, 5f)
                        ;
                    break;

                case InternalStep.RemoveNut4:
                    _sim.lugNut4.DroppedEvent += LugNut1_DroppedEvent;
                    _stepChain
                        .EnableObject(_sim.lugNut4)
                        .DisableObject(_sim.tyerAttachlugNut4)
                        .DisableObject(_sim.SnapWrench4);
                    _stepChain
                        .PlayCoach(_sim.soundData.RemoveNut4)
                        .HighlightObject(_sim.lugNut4)
                        .MakeGrabbable(_sim.lugNut4)              
                        .AddGrabCondition(_sim.lugNut4)
                        .PlayRepeatingReminder(_sim, _sim.soundData.RemoveNut4, 5f)
                        ;
                    break;

                case InternalStep.PlaceNut4OnTable:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTable)
                        .HighlightObject(_sim.tabelPlane)
                        .AddTableTriggerCondition(_sim.tabelTrigger, _sim.lugNut4)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTable, 5f)
                            ;
                    break;

                case InternalStep.PlaceWrenchNut5:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut5)
                        .EnableObject(_sim.SnapWrench5)
                        .AddSnappedCondition(_sim.SnapWrench5, _sim.wrench)
                        .PlayRepeatingReminder(_sim,_sim.soundData.PlaceWrenchNut5,5f)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation5:
                    _sim.StepIndexToComplete = 20;
                    TriggerButtonPressedForWrinch();         
                    _stepChain
                        .PlayCoach(_sim.soundData.PressTriggerLoseTheNut5)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerLoseTheNut5, 5f)
                        ;
                    break;

                case InternalStep.RemoveNut5:
                    _sim.lugNut5.DroppedEvent += LugNut5_DroppedEvent;
                    _stepChain
                        .EnableObject(_sim.lugNut5)
                        .DisableObject(_sim.tyerAttachlugNut5)
                        .DisableObject(_sim.SnapWrench5);
                    _stepChain
                        .PlayCoach(_sim.soundData.RemoveNut5)
                        .HighlightObject(_sim.lugNut5)
                        .MakeGrabbable(_sim.lugNut5)                   
                        .AddGrabCondition(_sim.lugNut5)                     
                        .PlayRepeatingReminder(_sim, _sim.soundData.RemoveNut5, 5f)
                    ;
                    break;

                case InternalStep.PlaceNut5OnTable:
                    _sim.lugNut5.DroppedEvent -= LugNut1_DroppedEvent;
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTable)
                        .HighlightObject(_sim.tabelPlane)
                        .AddTableTriggerCondition(_sim.tabelTrigger, _sim.lugNut5)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTable, 5f)
                            ;
                    break;

                case InternalStep.PlaceWrenchOnTable:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchBack)
                        .HighlightObject(_sim.tabelPlane)
                        .AddTableTriggerCondition(_sim.tabelTrigger, _sim.wrench)
                        .PlayRepeatingReminder(_sim,_sim.soundData.PlaceWrenchBack,5f)
                        ;
                    break;

                case InternalStep.RemoveTireFromAxle:
                    _sim.tireDamagedVisualOnly.enabled = false;
                    _sim.tireDamagedVisualOnly2.enabled = false;
                    _stepChain
                        .EnableObject(_sim.tireDamaged)
                        .PlayCoach(_sim.soundData.RemoveTireFromAxle)                       
                        .HighlightObject(_sim.tireDamaged.gameObject)
                        .MakeGrabbable(_sim.tireDamaged)
                        .AddGrabCondition(_sim.tireDamaged)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.RemoveTireFromAxle, 5f);
                    break;

                case InternalStep.PlaceTireOnGround:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceTireOnGround)
                        .EnableObject(_sim.oldTireDropZone)
                        .AddSnappedInDropZoneCondition(_sim.oldTireDropZone)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceTireOnGround, 5f);
                    break;

                case InternalStep.GrabNewTire:
                    _stepChain
                        .PlayCoach(_sim.soundData.GrabNewTire)
                        .HighlightObject(_sim.tireGood)
                        .MakeGrabbable(_sim.tireGood)
                        .AddGrabCondition(_sim.tireGood)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.GrabNewTire, 5f);
                    break;

                case InternalStep.PlaceItOnAxle:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceItOnAxle)
                        .EnableObject(_sim.newTireDropZone)
                        .AddSnappedInDropZoneCondition(_sim.newTireDropZone)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceItOnAxle, 5f);
                    break;

                case InternalStep.NowGrabLugNut1:
                    _stepChain
                        .PlayCoach(_sim.soundData.NowGrabLugNut1)
                        .HighlightObject(_sim.lugNut1)
                        .MakeGrabbable(_sim.lugNut1)
                        .AddGrabCondition(_sim.lugNut1)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut1, 5f);
                    break;

                case InternalStep.PlaceNut1OnTire:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTire)
                        .EnableObject(_sim.nut1DropZoneTire)
                        .AddSnappedInDropZoneCondition(_sim.nut1DropZoneTire)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 5f);
                    break;

                case InternalStep.NowGrabLugNut2:
                    _stepChain
                        .PlayCoach(_sim.soundData.NowGrabLugNut2)
                        .HighlightObject(_sim.lugNut2)
                        .MakeGrabbable(_sim.lugNut2)
                        .AddGrabCondition(_sim.lugNut2)                       
                        .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut2, 5f);
                    break;

                case InternalStep.PlaceNut2OnTire:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTire)
                        .EnableObject(_sim.nut2DropZoneTire)
                        .AddSnappedInDropZoneCondition(_sim.nut2DropZoneTire)                       
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 5f)
                        ;
                    break;

                case InternalStep.NowGrabLugNut3:
                    _stepChain
                        .PlayCoach(_sim.soundData.NowGrabLugNut3)
                        .HighlightObject(_sim.lugNut3)
                        .MakeGrabbable(_sim.lugNut3)
                        .AddGrabCondition(_sim.lugNut3)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut3, 5f)
                        ;
                    break;

                case InternalStep.PlaceNut3OnTire:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTire)
                        .EnableObject(_sim.nut3DropZoneTire)
                        .AddSnappedInDropZoneCondition(_sim.nut3DropZoneTire)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 5f)
                        ;
                    break;

                case InternalStep.NowGrabLugNut4:
                    _stepChain
                        .PlayCoach(_sim.soundData.NowGrabLugNut4)
                        .HighlightObject(_sim.lugNut4)
                        .MakeGrabbable(_sim.lugNut4)
                        .AddGrabCondition(_sim.lugNut4)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut4, 5f)
                        ;
                    break;

                case InternalStep.PlaceNut4OnTire:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTire)
                        .EnableObject(_sim.nut4DropZoneTire)
                        .AddSnappedInDropZoneCondition(_sim.nut4DropZoneTire)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 5f)
                        ;
                    break;

                case InternalStep.NowGrabLugNut5:
                    _stepChain
                        .PlayCoach(_sim.soundData.NowGrabLugNut5)
                        .HighlightObject(_sim.lugNut5)
                        .MakeGrabbable(_sim.lugNut5)
                        .AddGrabCondition(_sim.lugNut5)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut5, 5f)
                        ;
                    break;

                case InternalStep.PlaceNut5OnTire:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceNutOnTire)
                        .EnableObject(_sim.nut5DropZoneTire)
                        .AddSnappedInDropZoneCondition(_sim.nut5DropZoneTire)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 5f)
                        ;
                    break;

                case InternalStep.GrabWrenchAgain:
                    _stepChain
                        .PlayCoach(_sim.soundData.GrabWrenchFromTable)
                        .HighlightObject(_sim.wrench)
                        .MakeGrabbable(_sim.wrench)
                        .AddGrabCondition(_sim.wrench)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.GrabWrenchFromTable, 5f)
                        ;
                    break;

                case InternalStep.WrenchPlaceOnNut1:
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut1)
                        .EnableObject(_sim.SnapWrench)
                        .AddSnappedCondition(_sim.SnapWrench, _sim.wrench)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut1, 5f)
                        ;
                    break;

                case InternalStep.WrenchAnimationTrigger1:
                    _sim.StepIndexToComplete = 40;
                    TriggerButtonPressedForWrinch();
                    _stepChain
                       .PlayCoach(_sim.soundData.PressTriggerTightTheNut1)
                       .PlayRepeatingReminder(_sim,_sim.soundData.PressTriggerTightTheNut1,5f)
                       ;
                    break;

                case InternalStep.WrenchPlaceOnNut2:
                    _stepChain
                        .DisableObject(_sim.SnapWrench);
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut2)
                        .EnableObject(_sim.SnapWrench2)
                        .AddSnappedCondition(_sim.SnapWrench2, _sim.wrench)                      
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut2, 5f)
                        ;
                    break;

                case InternalStep.WrenchAnimationTrigger2:
                    _sim.StepIndexToComplete = 42;
                    TriggerButtonPressedForWrinch();
                    _stepChain
                       .PlayCoach(_sim.soundData.PressTriggerTightTheNut2)
                       .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerTightTheNut2, 5f)
                       ;
                    break;

                case InternalStep.WrenchPlaceOnNut3:
                    _stepChain
                        .DisableObject(_sim.SnapWrench2);
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut3)
                        .EnableObject(_sim.SnapWrench3)
                        .AddSnappedCondition(_sim.SnapWrench3, _sim.wrench)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut3, 5f)
                        ;
                    break;

                case InternalStep.WrenchAnimationTrigger3:
                    _sim.StepIndexToComplete = 44;
                    TriggerButtonPressedForWrinch();
                    _stepChain
                       .PlayCoach(_sim.soundData.PressTriggerTightTheNut3)
                       .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerTightTheNut3, 5f)
                       ;
                    break;

                case InternalStep.WrenchPlaceOnNut4:
                    _stepChain
                        .DisableObject(_sim.SnapWrench3);
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchNut4)
                        .EnableObject(_sim.SnapWrench4)
                        .AddSnappedCondition(_sim.SnapWrench4, _sim.wrench)                        
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut4, 5f)
                        ;
                    break;

                case InternalStep.WrenchAnimationTrigger4:
                    _sim.StepIndexToComplete = 46;
                    TriggerButtonPressedForWrinch();
                    _stepChain
                       .PlayCoach(_sim.soundData.PressTriggerTightTheNut4)
                       .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerTightTheNut4, 5f)
                       ;
                    break;

                case InternalStep.WrenchPlaceOnNut5:
                    _stepChain
                       .DisableObject(_sim.SnapWrench4);
                    _stepChain
                       .PlayCoach(_sim.soundData.PlaceWrenchNut5)
                       .EnableObject(_sim.SnapWrench5)
                       .AddSnappedCondition(_sim.SnapWrench5, _sim.wrench)                       
                       .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut5, 5f)
                       ;
                    break;

                case InternalStep.WrenchAnimationTrigger5:
                    _sim.StepIndexToComplete = 48;
                    TriggerButtonPressedForWrinch();
                    _stepChain
                       .PlayCoach(_sim.soundData.PressTriggerTightTheNut5)
                       .PlayRepeatingReminder(_sim, _sim.soundData.PressTriggerTightTheNut5, 5f)
                       ;
                    break;

                case InternalStep.PlaceWrenchOnTabel:
                    _stepChain
                        .DisableObject(_sim.SnapWrench5);
                    _stepChain
                        .PlayCoach(_sim.soundData.PlaceWrenchBack)
                        .HighlightObject(_sim.tabelPlane)
                        .AddTableTriggerCondition(_sim.tabelTrigger,_sim.wrench)
                        .PlayRepeatingReminder(_sim,_sim.soundData.PlaceWrenchBack,5f)
                        ;
                    break;

                case InternalStep.CarLifterDown:
                    _stepChain
                        .PlayCoach(_sim.soundData.HandelDown)
                        .HighlightObject(_sim.carUpAndDownHandel)
                        .EnableBehaviour(_sim.carUpAndDownHandel)
                        .AddRotationCondition(_sim.carUpAndDownHandel,RotationCriteria.RotationLimitReached.CounterClockwise)
                        .PlayRepeatingReminder(_sim,_sim.soundData.HandelDown,5f)
                        ;
                    break;

                case InternalStep.PressTheOffButton:
                    _stepChain
                        .PlayCoach(_sim.soundData.RedButtonPress)
                        .HighlightObject(_sim.buttonOffHighlight)
                        .EnableObject(_sim.buttonOffObject)
                        .AddTouchCondition(_sim.buttonOff)
                        .PlayRepeatingReminder(_sim,_sim.soundData.RedButtonPress,5f)
                        ;
                    break;

                case InternalStep.Congrats:
                    _stepChain
                        .PlayCoach(_sim.soundData.Congrats)
                        ;
                    break;
            }
        }

        private void LugNut5_DroppedEvent(global::Interactable.BaseItem obj)
        {
            obj.transform.SetParent(null);
        }

        private void LugNut1_DroppedEvent(global::Interactable.BaseItem obj)
        {
            obj.transform.SetParent(null);           
        }

        private void VrtkController_TriggerReleased(object sender, ControllerInteractionEventArgs e)
        {
            Debug.Log("Trigger release");
            _sim.RotationNutAnim.enabled = false;
            _sim.WrinchAudioSource.Stop();
        }
        public void TriggerButtonPressedForWrinch()
        {
            Debug.Log("Auto " + _sim.StepIndexToComplete);           
            _sim.SceneSession.CustomAvatarR.VrtkController.TriggerPressed += VrtkController_TriggerPressed1;
            _sim.SceneSession.CustomAvatarR.VrtkController.TriggerReleased += VrtkController_TriggerReleased;
          
        }       

        private void VrtkController_TriggerPressed1(object sender, ControllerInteractionEventArgs e)
        {
            Debug.Log("Trigger pressed");
            
            if (_sim.wrench.IsGrabbed() && _internalStepHandler.CurrentStepIndex == _sim.StepIndexToComplete && _sim.SnapWrench.IsSnapped || _sim.SnapWrench2.IsSnapped || _sim.SnapWrench3.IsSnapped || _sim.SnapWrench4.IsSnapped || _sim.SnapWrench5.IsSnapped)
            {
                _sim.RotationNutAnim.enabled = true;
                _sim.WrinchAudioSource.Play();
                AutoCompleteOnInternalStepEnter(_sim.StepIndexToComplete);
                _sim.SceneSession.CustomAvatarR.VrtkController.TriggerPressed -= VrtkController_TriggerPressed1;
                _sim.SceneSession.CustomAvatarR.VrtkController.TriggerReleased -= VrtkController_TriggerReleased;
            }
        }
        public override void Update()
        {
            base.Update();         
            
        }
        private void RefreshDropZonePreview(CustomSnapDropZone dropZone)
        {
            dropZone.RemovePreview();
            dropZone.CreatePreview();
            dropZone.UsePreview = true;
        }

        public void CompleteCondition()
        {         
            _stepChain.ConditionalCompletion.AddInstantComplete();
        }

        protected override void OnInternalStepComplete(InternalStepsHandler sender, ref InternalStepsHandler.CompleteStepArgs args)
        {
            _sim.StopReminderAudio();
            _stepChain?.Kill();
            _stepChain = ChainManager.Get();         
           
            

            switch ((InternalStep)args.CompletedStepIndex)
            {
                case InternalStep.IntroAndPressTheOnButton:
                    _stepChain
                        .EnableBehaviour(_sim.carLifter1)
                        .EnableBehaviour(_sim.carLifter2)
                        .EnableBehaviour(_sim.carLifter3)
                        .EnableBehaviour(_sim.carLifter4)
                        .UnhighlightObject(_sim.buttonOnHighlight)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                    ;
                    break;

                case InternalStep.CarLifter:
                    _stepChain
                        .UnhighlightObject(_sim.HandelHighlighter)
                        .EnableBehaviour(_sim.carLifterAnimatoin)
                        .EnableBehaviour(_sim.carUpAnimation)
                        .DisableBehaviour(_sim.carUpAndDownHandel)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.GrabImpactWrenchFromTable:
                    _stepChain
                        .UnhighlightObject(_sim.wrench)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.PlaceWrenchNut1:
                    _stepChain
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation1:
                    _stepChain
                        .Wait(1)
                        .EnableObject(_sim.lugNut1)
                        .DisableObject(_sim.tyerAttachlugNut1)
                        .DisableObject(_sim.SnapWrench)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RemoveNut1:
                    _stepChain                  
                        .UnhighlightObject(_sim.lugNut1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceNut1OnTable:
                    _stepChain
                        .MakeUngrabbable(_sim.lugNut1)
                        .DisableBehaviour(_sim.lugNut1)
                        .UnhighlightObject(_sim.tabelPlane)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceWrenchNut2:
                    _stepChain
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation2:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RemoveNut2:
                    Debug.Log("Complete");
                    _stepChain
                        .UnhighlightObject(_sim.lugNut2)
                        .PlayCoach(_sim.soundData.SfxConfirm)                        
                        ;
                    break;

                case InternalStep.PlaceNut2OnTable:
                    _stepChain
                        .MakeUngrabbable(_sim.lugNut2)
                        .DisableBehaviour(_sim.lugNut2)
                        .UnhighlightObject(_sim.tabelPlane)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                            ;
                    break;

                case InternalStep.PlaceWrenchNut3:
                    _stepChain
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation3:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RemoveNut3:
                    _stepChain
                        .UnhighlightObject(_sim.lugNut3)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceNut3OnTable:
                    _stepChain
                        .MakeGrabbable(_sim.lugNut3)
                        .UnhighlightObject(_sim.tabelPlane)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceWrenchNut4:
                    _stepChain
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation4:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RemoveNut4:
                    _stepChain
                        .UnhighlightObject(_sim.lugNut4)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceNut4OnTable:
                    _stepChain
                        .MakeUngrabbable(_sim.lugNut4)
                        .DisableBehaviour(_sim)
                        .UnhighlightObject(_sim.tabelPlane)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceWrenchNut5:
                    _stepChain
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RotateWrenchAnimation5:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.RemoveNut5:
                    _stepChain
                        .UnhighlightObject(_sim.lugNut5)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceNut5OnTable:
                    _stepChain
                        .UnhighlightObject(_sim.tabelPlane)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceWrenchOnTable:
                    _stepChain
                       .MakeUngrabbable(_sim.lugNut5)
                       .DisableBehaviour(_sim.lugNut5)
                       .UnhighlightObject(_sim.tabelPlane)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.RemoveTireFromAxle:
                    _stepChain
                       .UnhighlightObject(_sim.tireDamaged)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.PlaceTireOnGround:
                    _sim.tireReset.DoReset = false;
                    _stepChain
                      .DisableObject(_sim.oldTireDropZone)
                      .MakeUngrabbable(_sim.tireDamaged)
                      .PlayCoach(_sim.soundData.SfxConfirm)
                      ;
                    break;

                case InternalStep.GrabNewTire:
                    _stepChain
                       .UnhighlightObject(_sim.tireGood)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.PlaceItOnAxle:
                    _sim.tireDamagedVisualOnly2.enabled = true;
                    _sim.tireDamagedVisualOnly.enabled = true;
                    _sim.TyerMaterial.SetOtherMaterial();
                    _stepChain
                       .DisableObject(_sim.newTireDropZone)
                       .DisableObject(_sim.tireGood)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.NowGrabLugNut1:
                    _sim.lugNut1.SetOriginalParent(_sim._extraCoachPositions[0]);
                    _stepChain
                       .UnhighlightObject(_sim.lugNut1)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.PlaceNut1OnTire:
                    _stepChain
                       .EnableObject(_sim.tyerAttachlugNut1)
                       .DisableObject(_sim.lugNut1)
                       .DisableObject(_sim.nut1DropZoneTire)
                       .MakeUngrabbable(_sim.lugNut1)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.NowGrabLugNut2:
                    _sim.lugNut2.SetOriginalParent(_sim._extraCoachPositions[1]);
                    _stepChain
                       .UnhighlightObject(_sim.lugNut2)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.PlaceNut2OnTire:
                    _stepChain
                       .EnableObject(_sim.tyerAttachlugNut2)
                       .DisableObject(_sim.lugNut2)
                       .DisableObject(_sim.nut2DropZoneTire)
                       .MakeUngrabbable(_sim.lugNut2)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.NowGrabLugNut3:
                    _sim.lugNut3.SetOriginalParent(_sim._extraCoachPositions[2]);
                    _stepChain
                       .UnhighlightObject(_sim.lugNut3)
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.PlaceNut3OnTire:
                    _stepChain
                      .EnableObject(_sim.tyerAttachlugNut3)
                      .DisableObject(_sim.lugNut3)
                      .DisableObject(_sim.nut3DropZoneTire)
                      .MakeUngrabbable(_sim.lugNut3)
                      .PlayCoach(_sim.soundData.SfxConfirm)
                      ;
                    break;

                case InternalStep.NowGrabLugNut4:
                    _sim.lugNut4.SetOriginalParent(_sim._extraCoachPositions[3]);
                    _stepChain
                      .UnhighlightObject(_sim.lugNut4)
                      .PlayCoach(_sim.soundData.SfxConfirm)
                      ;
                    break;

                case InternalStep.PlaceNut4OnTire:
                    _stepChain
                      .EnableObject(_sim.tyerAttachlugNut4)
                      .DisableObject(_sim.lugNut4)
                      .DisableObject(_sim.nut4DropZoneTire)
                      .MakeUngrabbable(_sim.lugNut4)
                      .PlayCoach(_sim.soundData.SfxConfirm)
                      ;
                    break;

                case InternalStep.NowGrabLugNut5:
                    _sim.lugNut5.SetOriginalParent(_sim._extraCoachPositions[4]);
                    _stepChain
                      .UnhighlightObject(_sim.lugNut5)
                      .PlayCoach(_sim.soundData.SfxConfirm)
                      ;
                    break;

                case InternalStep.PlaceNut5OnTire:
                    _stepChain
                      .EnableObject(_sim.tyerAttachlugNut5)
                      .DisableObject(_sim.lugNut5)
                      .DisableObject(_sim.nut5DropZoneTire)
                      .MakeUngrabbable(_sim.lugNut5)
                      .PlayCoach(_sim.soundData.SfxConfirm)
                      ;
                    break;

                case InternalStep.GrabWrenchAgain:
                    _stepChain.
                        UnhighlightObject(_sim.wrench)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.WrenchPlaceOnNut1:
                    _stepChain
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.WrenchAnimationTrigger1:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.WrenchPlaceOnNut2:
                    _stepChain
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.WrenchAnimationTrigger2:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.WrenchPlaceOnNut3:
                    _stepChain
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.WrenchAnimationTrigger3:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.WrenchPlaceOnNut4:
                    _stepChain
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.WrenchAnimationTrigger4:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.WrenchPlaceOnNut5:
                    _stepChain
                       .PlayCoach(_sim.soundData.SfxConfirm)
                       ;
                    break;

                case InternalStep.WrenchAnimationTrigger5:
                    _stepChain
                        .Wait(1)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PlaceWrenchOnTabel:
                    _stepChain
                        .UnhighlightObject(_sim.tabelPlane)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.CarLifterDown:
                    _sim.CarLifterDownAnimation();
                    _stepChain
                        .UnhighlightObject(_sim.carUpAndDownHandel)
                        .DisableBehaviour(_sim.carUpAndDownHandel)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;

                case InternalStep.PressTheOffButton:
                    _sim.LifterArmsRotation();
                    _stepChain
                        .UnhighlightObject(_sim.buttonOffHighlight)
                        .DisableObject(_sim.buttonOffObject)
                        .PlayCoach(_sim.soundData.SfxConfirm)
                        ;
                    break;


            }
            args.Delay = _stepChain.TotalLength;
        }

        public override void OnExit(NextAction nextAction)
        {
            base.OnExit(nextAction);
            Debug.Log("========== IntroductionStep Exit ===========");
        }
    }

}
