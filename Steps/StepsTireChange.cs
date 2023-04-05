using SVR.Chaining;
using SVR.Workflow.TriangleFactory;
using SVR.Workflow.TriangleFactory.Scripts.Mechanics;
using System;
using Transfr.Platform.Fresh;
using UnityEngine;

namespace SVR.Workflow
{
    [CreateAssetMenu(fileName = "AnimatedStep", menuName = "simulations/TireChange/Steps")]
    public class StepsTireChange : CustomStep<TireChangeSim>
    {
        //public SVRChaining SVRChaining = new SVRChaining();

        enum InternalStep
        {
            None = -1,
            IntroAndGrabCarJack,
            PlaceitUnderCar,
            PushJackLever,
            GrabWrenchFromTable,
            PlaceWrenchNut1,
            RotateWrenchCounterClockwiseNut1,
            PlaceWrenchNut2,
            RotateWrenchCounterClockwiseNut2,
            PlaceWrenchNut3,
            RotateWrenchCounterClockwiseNut3,
            PlaceWrenchNut4,
            RotateWrenchCounterClockwiseNut4,
            PlaceWrenchBack,
            RemoveNut1,
            PlaceNut1OnTable,
            RemoveNut2,
            PlaceNut2OnTable,
            RemoveNut3,
            PlaceNut3OnTable,
            RemoveNut4,
            PlaceNut4OnTable,
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
            GrabWrenchAgain,
            PlaceWrenchNut1_Final,
            RotateWrenchClockwiseNut1,
            PlaceWrenchNut2_Final,
            RotateWrenchClockwiseNut2,
            PlaceWrenchNut3_Final,
            RotateWrenchClockwiseNut3,
            PlaceWrenchNut4_Final,
            RotateWrenchClockwiseNut4,
            PlaceWrenchBack_Final,
            RotateCarJackToLowerCar,
            GrabCarJack_Final,
            PlaceCarJackHighlightedPos,
            Congrats
        }

        int num = 0;
        public override void OnEnter(ObjectReferences objectRefs, NextAction nextAction = null)
        {
            Debug.Log("Entering: [MainStep]IntroductionAnimated");
            //UI.ScoreSheet.Instance.SetLevelName("Soups and Sauces");

            base.OnEnter(objectRefs);

            //Init stuff here.
            _internalStepHandler.Initialize(Enum.GetValues(typeof(InternalStep)), _conditionalCompletion);
        }

        protected override void OnInternalStepEnter(InternalStepsHandler sender, ref InternalStepsHandler.EnterStepArgs args)
        {
            _stepChain?.Kill();
            _stepChain = ChainManager.Get(_conditionalCompletion);
            //_sim._gameDoor.SetActive(false);

            switch ((InternalStep)args.EnteredStepIndex)
            {
                case InternalStep.IntroAndGrabCarJack:
                    _stepChain
                        .Wait(1f)
                        .PlayCoach(_sim.soundData.Intro)
                        .Wait(.5f)
                        .HighlightObject(_sim.carJack)
                        .PlayCoach(_sim.soundData.GrabCarJack)
                        .PlayRepeatingReminder(_sim, _sim.soundData.GrabCarJack, 4f)
                        .MakeGrabbable(_sim.carJack)
                        .AddGrabCondition(_sim.carJack);
                    break;

                case InternalStep.PlaceitUnderCar:
                    _stepChain
                        .EnableObject(_sim.jackDropZoneUnderCar.gameObject)
                        .AddSnappedInDropZoneCondition(_sim.jackDropZoneUnderCar)
                        .PlayCoach(_sim.soundData.PlaceitUnderCar)
                        .PlayRepeatingReminder(_sim, _sim.soundData.PlaceitUnderCar, 4f);
                    break;

                case InternalStep.PushJackLever:
                    _stepChain
                    .EnableBehaviour(_sim.carJackHandlePushRotator)
                    .PlayCoach(_sim.soundData.PushJackLever)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PushJackLever, 4f);
                    break;

                case InternalStep.GrabWrenchFromTable:
                    _stepChain
                    .HighlightObject(_sim.wrench)
                    .MakeGrabbable(_sim.wrench)
                    .AddGrabCondition(_sim.wrench)
                    .PlayCoach(_sim.soundData.GrabWrenchFromTable)
                    .PlayRepeatingReminder(_sim, _sim.soundData.GrabWrenchFromTable, 4f);
                    break;

                case InternalStep.PlaceWrenchNut1:
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneNut1.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut1)
                    .PlayCoach(_sim.soundData.PlaceWrenchNut1)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut1, 4f);
                    break;

                case InternalStep.RotateWrenchCounterClockwiseNut1:
                    _stepChain
                    .EnableObject(_sim.nut1LooseRotator.gameObject)
                    .AddRotationCondition(_sim.nut1LooseRotator, RotationCriteria.RotationLimitReached.Clockwise)
                    .PlayCoach(_sim.soundData.RotateWrenchCounterClockwise)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateWrenchCounterClockwise, 4f);
                    break;

                case InternalStep.PlaceWrenchNut2:
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneNut2.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut2)
                    .PlayCoach(_sim.soundData.PlaceWrenchNut2)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut2, 4f);
                    break;

                case InternalStep.RotateWrenchCounterClockwiseNut2:
                    _stepChain
                    .EnableObject(_sim.nut2LooseRotator.gameObject)
                    .AddRotationCondition(_sim.nut2LooseRotator, RotationCriteria.RotationLimitReached.Clockwise)
                    .PlayCoach(_sim.soundData.RotateWrenchCounterClockwise)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateWrenchCounterClockwise, 4f);
                    break;

                case InternalStep.PlaceWrenchNut3:
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneNut3.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut3)
                    .PlayCoach(_sim.soundData.PlaceWrenchNut3)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut3, 4f);
                    break;

                case InternalStep.RotateWrenchCounterClockwiseNut3:
                    _stepChain
                    .EnableObject(_sim.nut3LooseRotator.gameObject)
                    .AddRotationCondition(_sim.nut3LooseRotator, RotationCriteria.RotationLimitReached.Clockwise)
                    .PlayCoach(_sim.soundData.RotateWrenchCounterClockwise)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateWrenchCounterClockwise, 4f);
                    break;

                case InternalStep.PlaceWrenchNut4:
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneNut4.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut4)
                    .PlayCoach(_sim.soundData.PlaceWrenchNut4)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut4, 4f);
                    break;

                case InternalStep.RotateWrenchCounterClockwiseNut4:
                    _stepChain
                    .EnableObject(_sim.nut4LooseRotator.gameObject)
                    .AddRotationCondition(_sim.nut4LooseRotator, RotationCriteria.RotationLimitReached.Clockwise)
                    .PlayCoach(_sim.soundData.RotateWrenchCounterClockwise)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateWrenchCounterClockwise, 4f);
                    break;

                case InternalStep.PlaceWrenchBack:
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneTable.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneTable)
                    .PlayCoach(_sim.soundData.PlaceWrenchBack)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchBack, 4f);
                    break;

                case InternalStep.RemoveNut1:
                    _stepChain
                    .MakeGrabbable(_sim.lugNut1)
                    .HighlightObject(_sim.lugNut1)
                    .AddGrabCondition(_sim.lugNut1)
                    .PlayCoach(_sim.soundData.RemoveNut1)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RemoveNut1, 4f);
                    break;

                case InternalStep.PlaceNut1OnTable:
                    _stepChain
                    .EnableObject(_sim.nut1DropZoneTable.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.nut1DropZoneTable)
                    .PlayCoach(_sim.soundData.PlaceNutOnTable)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTable, 4f);
                    break;

                case InternalStep.RemoveNut2:
                    _stepChain
                    .MakeGrabbable(_sim.lugNut2)
                    .HighlightObject(_sim.lugNut2)
                    .AddGrabCondition(_sim.lugNut2)
                    .PlayCoach(_sim.soundData.RemoveNut2)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RemoveNut2, 4f);
                    break;

                case InternalStep.PlaceNut2OnTable:
                    _stepChain
                    .EnableObject(_sim.nut2DropZoneTable.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.nut2DropZoneTable)
                    .PlayCoach(_sim.soundData.PlaceNutOnTable)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTable, 4f);
                    break;

                case InternalStep.RemoveNut3:
                    _stepChain
                    .MakeGrabbable(_sim.lugNut3)
                    .HighlightObject(_sim.lugNut3)
                    .AddGrabCondition(_sim.lugNut3)
                    .PlayCoach(_sim.soundData.RemoveNut3)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RemoveNut3, 4f);
                    break;

                case InternalStep.PlaceNut3OnTable:
                    _stepChain
                    .EnableObject(_sim.nut3DropZoneTable.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.nut3DropZoneTable)
                    .PlayCoach(_sim.soundData.PlaceNutOnTable)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTable, 4f);
                    break;

                case InternalStep.RemoveNut4:
                    _stepChain
                    .MakeGrabbable(_sim.lugNut4)
                    .HighlightObject(_sim.lugNut4)
                    .AddGrabCondition(_sim.lugNut4)
                    .PlayCoach(_sim.soundData.RemoveNut4)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RemoveNut4, 4f);
                    break;

                case InternalStep.PlaceNut4OnTable:
                    _stepChain
                    .EnableObject(_sim.nut4DropZoneTable.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.nut4DropZoneTable)
                    .PlayCoach(_sim.soundData.PlaceNutOnTable)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTable, 4f);
                    break;

                case InternalStep.RemoveTireFromAxle:
                    _sim.tireDamagedVisualOnly.enabled = false;
                    _stepChain
                    .EnableObject(_sim.tireDamaged)
                    .HighlightObject(_sim.tireDamaged.gameObject)
                    .MakeGrabbable(_sim.tireDamaged)
                    .AddGrabCondition(_sim.tireDamaged)
                    .PlayCoach(_sim.soundData.RemoveTireFromAxle)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RemoveTireFromAxle, 4f);
                    break;

                case InternalStep.PlaceTireOnGround:
                    _stepChain
                    .EnableObject(_sim.oldTireDropZone)
                    .AddSnappedInDropZoneCondition(_sim.oldTireDropZone)
                    .PlayCoach(_sim.soundData.PlaceCarJackHighlightedPos)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceCarJackHighlightedPos, 4f);
                    break;

                case InternalStep.GrabNewTire:
                    _stepChain
                    .HighlightObject(_sim.tireGood)
                    .MakeGrabbable(_sim.tireGood)
                    .AddGrabCondition(_sim.tireGood)
                    .PlayCoach(_sim.soundData.GrabNewTire)
                    .PlayRepeatingReminder(_sim, _sim.soundData.GrabNewTire, 4f);
                    break;

                case InternalStep.PlaceItOnAxle:
                    _stepChain
                   .EnableObject(_sim.newTireDropZone)
                    .AddSnappedInDropZoneCondition(_sim.newTireDropZone)
                    .PlayCoach(_sim.soundData.PlaceItOnAxle)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceItOnAxle, 4f);
                    break;

                case InternalStep.NowGrabLugNut1:
                    _stepChain
                    .HighlightObject(_sim.lugNut1)
                    .MakeGrabbable(_sim.lugNut1)
                    .AddGrabCondition(_sim.lugNut1)
                    .PlayCoach(_sim.soundData.NowGrabLugNut1)
                    .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut1, 4f);
                    break;

                case InternalStep.PlaceNut1OnTire:
                    _stepChain
                    .EnableObject(_sim.nut1DropZoneTire)
                    .AddSnappedInDropZoneCondition(_sim.nut1DropZoneTire)
                    .PlayCoach(_sim.soundData.PlaceNutOnTire)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 4f);
                    break;

                case InternalStep.NowGrabLugNut2:
                    _stepChain
                    .HighlightObject(_sim.lugNut2)
                    .MakeGrabbable(_sim.lugNut2)
                    .AddGrabCondition(_sim.lugNut2)
                    .PlayCoach(_sim.soundData.NowGrabLugNut2)
                    .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut2, 4f);
                    break;

                case InternalStep.PlaceNut2OnTire:
                    _stepChain
                    .EnableObject(_sim.nut2DropZoneTire)
                    .AddSnappedInDropZoneCondition(_sim.nut2DropZoneTire)
                    .PlayCoach(_sim.soundData.PlaceNutOnTire)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 4f);
                    break;

                case InternalStep.NowGrabLugNut3:
                    _stepChain
                    .HighlightObject(_sim.lugNut3)
                    .MakeGrabbable(_sim.lugNut3)
                    .AddGrabCondition(_sim.lugNut3)
                    .PlayCoach(_sim.soundData.NowGrabLugNut3)
                    .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut3, 4f);
                    break;

                case InternalStep.PlaceNut3OnTire:
                    _stepChain
                    .EnableObject(_sim.nut3DropZoneTire)
                    .AddSnappedInDropZoneCondition(_sim.nut3DropZoneTire)
                    .PlayCoach(_sim.soundData.PlaceNutOnTire)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 4f);
                    break;

                case InternalStep.NowGrabLugNut4:
                    _stepChain
                    .HighlightObject(_sim.lugNut4)
                    .MakeGrabbable(_sim.lugNut4)
                    .AddGrabCondition(_sim.lugNut4)
                    .PlayCoach(_sim.soundData.NowGrabLugNut4)
                    .PlayRepeatingReminder(_sim, _sim.soundData.NowGrabLugNut4, 4f);
                    break;

                case InternalStep.PlaceNut4OnTire:
                    _stepChain
                    .EnableObject(_sim.nut4DropZoneTire)
                    .AddSnappedInDropZoneCondition(_sim.nut4DropZoneTire)
                    .PlayCoach(_sim.soundData.PlaceNutOnTire)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceNutOnTire, 4f);
                    break;

                case InternalStep.GrabWrenchAgain:
                    _stepChain
                    .HighlightObject(_sim.wrench)
                    .MakeGrabbable(_sim.wrench)
                    .AddGrabCondition(_sim.wrench)
                    .PlayCoach(_sim.soundData.GrabWrenchAgain)
                    .PlayRepeatingReminder(_sim, _sim.soundData.GrabWrenchAgain, 4f);
                    break;

                case InternalStep.PlaceWrenchNut1_Final:
                    RefreshDropZonePreview(_sim.wrenchDropZoneNut1);
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneNut1)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut1)
                    .PlayCoach(_sim.soundData.PlaceWrenchNut1)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut1, 4f);
                    break;

                case InternalStep.RotateWrenchClockwiseNut1:
                    _stepChain
                    .EnableObject(_sim.nut1TightenRotator.gameObject)
                    .AddRotationCondition(_sim.nut1TightenRotator, RotationCriteria.RotationLimitReached.CounterClockwise)
                    .PlayCoach(_sim.soundData.RotateWrenchClockWise)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateWrenchClockWise, 4f);
                    break;

                case InternalStep.PlaceWrenchNut2_Final:
                    RefreshDropZonePreview(_sim.wrenchDropZoneNut2);
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneNut2.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut2)
                    .PlayCoach(_sim.soundData.PlaceWrenchNut2)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut2, 4f);
                    break;

                case InternalStep.RotateWrenchClockwiseNut2:
                    _stepChain
                    .EnableObject(_sim.nut2TightenRotator.gameObject)
                    .AddRotationCondition(_sim.nut2TightenRotator, RotationCriteria.RotationLimitReached.CounterClockwise)
                    .PlayCoach(_sim.soundData.RotateWrenchClockWise)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateWrenchClockWise, 4f);
                    break;

                case InternalStep.PlaceWrenchNut3_Final:
                    RefreshDropZonePreview(_sim.wrenchDropZoneNut3);
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneNut3.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut3)
                    .PlayCoach(_sim.soundData.PlaceWrenchNut3)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut3, 4f);
                    break;

                case InternalStep.RotateWrenchClockwiseNut3:
                    _stepChain
                    .EnableObject(_sim.nut3TightenRotator.gameObject)
                    .AddRotationCondition(_sim.nut3TightenRotator, RotationCriteria.RotationLimitReached.CounterClockwise)
                    .PlayCoach(_sim.soundData.RotateWrenchClockWise)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateWrenchClockWise, 4f);
                    break;

                case InternalStep.PlaceWrenchNut4_Final:
                    RefreshDropZonePreview(_sim.wrenchDropZoneNut4);
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneNut4.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneNut4)
                    .PlayCoach(_sim.soundData.PlaceWrenchNut4)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchNut4, 4f);
                    break;

                case InternalStep.RotateWrenchClockwiseNut4:
                    _stepChain
                    .EnableObject(_sim.nut4TightenRotator.gameObject)
                    .AddRotationCondition(_sim.nut4TightenRotator, RotationCriteria.RotationLimitReached.CounterClockwise)
                    .PlayCoach(_sim.soundData.RotateWrenchClockWise)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateWrenchClockWise, 4f);
                    break;

                case InternalStep.PlaceWrenchBack_Final:
                    _stepChain
                    .EnableObject(_sim.wrenchDropZoneTable.gameObject)
                    .AddSnappedInDropZoneCondition(_sim.wrenchDropZoneTable)
                    .PlayCoach(_sim.soundData.PlaceWrenchBack)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceWrenchBack, 4f);
                    break;

                case InternalStep.RotateCarJackToLowerCar:
                    _stepChain
                    .EnableBehaviour(_sim.carJackLowerRotator)
                    .PlayCoach(_sim.soundData.RotateCarJackToLowerCar)
                    .PlayRepeatingReminder(_sim, _sim.soundData.RotateCarJackToLowerCar, 4f);
                    break;

                case InternalStep.GrabCarJack_Final:
                    _stepChain
                        .HighlightObject(_sim.carJack)
                        .MakeGrabbable(_sim.carJack)
                        .AddGrabCondition(_sim.carJack)
                        .PlayCoach(_sim.soundData.GrabCarJack)
                        .PlayRepeatingReminder(_sim, _sim.soundData.GrabCarJack, 4f);
                    break;

                case InternalStep.PlaceCarJackHighlightedPos:
                    _stepChain
                    .EnableObject(_sim.jackDropZoneOriginalPos)
                    .AddSnappedInDropZoneCondition(_sim.jackDropZoneOriginalPos)
                    .PlayCoach(_sim.soundData.PlaceCarJackHighlightedPos)
                    .PlayRepeatingReminder(_sim, _sim.soundData.PlaceCarJackHighlightedPos, 4f);
                    break;

                case InternalStep.Congrats:
                    _stepChain
                   .PlayCoach(_sim.soundData.Congrats);
                    break;
            }
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

            _stepChain.PlayCoach(_sim.soundData.SfxConfirm);

            switch ((InternalStep)args.CompletedStepIndex)
            {

                case InternalStep.IntroAndGrabCarJack:
                    _stepChain.UnhighlightObject(_sim.carJack);
                    break;

                case InternalStep.PlaceitUnderCar:
                    _stepChain
                        .DisableObject(_sim.jackDropZoneUnderCar)
                        .MakeUngrabbable(_sim.carJack);
                    break;

                case InternalStep.PushJackLever:
                    Debug.Log("In pushjack lever complete");
                    _stepChain.DisableBehaviour(_sim.carJackHandlePushRotator);
                    break;

                case InternalStep.GrabWrenchFromTable:
                    _stepChain.UnhighlightObject(_sim.wrench);
                    break;

                case InternalStep.PlaceWrenchNut1:
                    _stepChain.DisableObject(_sim.wrench);
                    break;

                case InternalStep.RotateWrenchCounterClockwiseNut1:
                    _stepChain
                        .EnableObject(_sim.wrench)
                        .DisableObject(_sim.wrenchDropZoneNut1)
                        .DisableObject(_sim.nut1LooseRotator.gameObject);
                    break;

                case InternalStep.PlaceWrenchNut2:
                    _stepChain.DisableObject(_sim.wrench);
                    break;

                case InternalStep.RotateWrenchCounterClockwiseNut2:
                    _stepChain
                       .EnableObject(_sim.wrench)
                       .DisableObject(_sim.wrenchDropZoneNut2)
                       .DisableObject(_sim.nut2LooseRotator.gameObject);
                    break;

                case InternalStep.PlaceWrenchNut3:
                    _stepChain.DisableObject(_sim.wrench);
                    break;

                case InternalStep.RotateWrenchCounterClockwiseNut3:
                    _stepChain
                        .EnableObject(_sim.wrench)
                        .DisableObject(_sim.wrenchDropZoneNut3)
                        .DisableObject(_sim.nut3LooseRotator.gameObject);
                    break;

                case InternalStep.PlaceWrenchNut4:
                    _stepChain.DisableObject(_sim.wrench);
                    break;

                case InternalStep.RotateWrenchCounterClockwiseNut4:
                    _stepChain
                        .EnableObject(_sim.wrench)
                        .DisableObject(_sim.wrenchDropZoneNut4)
                        .DisableObject(_sim.nut4LooseRotator.gameObject);
                    break;

                case InternalStep.PlaceWrenchBack:
                    _stepChain
                        .MakeUngrabbable(_sim.wrench)
                        .DisableObject(_sim.wrenchDropZoneTable);
                    break;

                case InternalStep.RemoveNut1:
                    _stepChain.UnhighlightObject(_sim.lugNut1);
                    break;

                case InternalStep.PlaceNut1OnTable:
                    _stepChain
                        .MakeUngrabbable(_sim.lugNut1)
                        .DisableObject(_sim.nut1DropZoneTable);
                    break;

                case InternalStep.RemoveNut2:
                    _stepChain.UnhighlightObject(_sim.lugNut2);
                    break;

                case InternalStep.PlaceNut2OnTable:
                    _stepChain
                        .MakeUngrabbable(_sim.lugNut2)
                        .DisableObject(_sim.nut2DropZoneTable);
                    break;

                case InternalStep.RemoveNut3:
                    _stepChain.UnhighlightObject(_sim.lugNut3);
                    break;

                case InternalStep.PlaceNut3OnTable:
                    _stepChain
                        .MakeUngrabbable(_sim.lugNut3)
                        .DisableObject(_sim.nut3DropZoneTable);
                    break;

                case InternalStep.RemoveNut4:
                    _stepChain.UnhighlightObject(_sim.lugNut4);
                    break;

                case InternalStep.PlaceNut4OnTable:
                    _stepChain
                        .MakeUngrabbable(_sim.lugNut4)
                        .DisableObject(_sim.nut4DropZoneTable);
                    break;

                case InternalStep.RemoveTireFromAxle:
                    _stepChain
                       .UnhighlightObject(_sim.tireDamaged);
                    break;

                case InternalStep.PlaceTireOnGround:
                    _stepChain
                      .DisableObject(_sim.oldTireDropZone)
                      .MakeUngrabbable(_sim.tireDamaged);
                    break;

                case InternalStep.GrabNewTire:
                    _stepChain
                       .UnhighlightObject(_sim.tireGood);
                    break;

                case InternalStep.PlaceItOnAxle:
                    _sim.tireDamagedVisualOnly.enabled = true;
                    _stepChain
                       .DisableObject(_sim.newTireDropZone)
                       .DisableObject(_sim.tireGood);
                    break;

                case InternalStep.NowGrabLugNut1:
                    _stepChain
                      .UnhighlightObject(_sim.lugNut1);
                    //.MakeUngrabbable(_sim.lugNut1);
                    break;

                case InternalStep.PlaceNut1OnTire:
                    _stepChain
                      .UnhighlightObject(_sim.nut1DropZoneTire)
                      .MakeUngrabbable(_sim.lugNut1);
                    break;

                case InternalStep.NowGrabLugNut2:
                    _stepChain
                      .UnhighlightObject(_sim.lugNut2);
                    //.MakeUngrabbable(_sim.lugNut2);
                    break;

                case InternalStep.PlaceNut2OnTire:
                    _stepChain
                      .UnhighlightObject(_sim.nut2DropZoneTire)
                      .MakeUngrabbable(_sim.lugNut2);
                    break;

                case InternalStep.NowGrabLugNut3:
                    _stepChain
                      .UnhighlightObject(_sim.lugNut3);
                    //.MakeUngrabbable(_sim.lugNut3);
                    break;

                case InternalStep.PlaceNut3OnTire:
                    _stepChain
                      .UnhighlightObject(_sim.nut3DropZoneTire)
                      .MakeUngrabbable(_sim.lugNut3);
                    break;

                case InternalStep.NowGrabLugNut4:
                    _stepChain
                      .UnhighlightObject(_sim.lugNut4);
                    //.MakeUngrabbable(_sim.lugNut4);
                    break;

                case InternalStep.PlaceNut4OnTire:
                    _stepChain
                      .UnhighlightObject(_sim.nut4DropZoneTire)
                      .MakeUngrabbable(_sim.lugNut4);
                    break;

                case InternalStep.GrabWrenchAgain:
                    _stepChain.UnhighlightObject(_sim.wrench);
                    break;

                case InternalStep.PlaceWrenchNut1_Final:
                    _stepChain.DisableObject(_sim.wrench);
                    break;

                case InternalStep.RotateWrenchClockwiseNut1:
                    _stepChain
                        .EnableObject(_sim.wrench)
                        .DisableObject(_sim.wrenchDropZoneNut1)
                        .DisableObject(_sim.nut1TightenRotator.gameObject);
                    break;

                case InternalStep.PlaceWrenchNut2_Final:
                    _stepChain.DisableObject(_sim.wrench);
                    break;

                case InternalStep.RotateWrenchClockwiseNut2:
                    _stepChain
                       .EnableObject(_sim.wrench)
                       .DisableObject(_sim.wrenchDropZoneNut2)
                       .DisableObject(_sim.nut2TightenRotator.gameObject);
                    break;

                case InternalStep.PlaceWrenchNut3_Final:
                    _stepChain.DisableObject(_sim.wrench);
                    break;

                case InternalStep.RotateWrenchClockwiseNut3:
                    _stepChain
                        .EnableObject(_sim.wrench)
                        .DisableObject(_sim.wrenchDropZoneNut3)
                        .DisableObject(_sim.nut3TightenRotator.gameObject);
                    break;

                case InternalStep.PlaceWrenchNut4_Final:
                    _stepChain.DisableObject(_sim.wrench);
                    break;

                case InternalStep.RotateWrenchClockwiseNut4:
                    _stepChain
                        .EnableObject(_sim.wrench)
                        .DisableObject(_sim.wrenchDropZoneNut4)
                        .DisableObject(_sim.nut4TightenRotator.gameObject);
                    break;

                case InternalStep.PlaceWrenchBack_Final:
                    _stepChain
                        .MakeUngrabbable(_sim.wrench)
                        .DisableObject(_sim.wrenchDropZoneTable);
                    break;

                case InternalStep.RotateCarJackToLowerCar:
                    _stepChain
                    .DisableBehaviour(_sim.carJackLowerRotator);
                    break;

                case InternalStep.GrabCarJack_Final:
                    _stepChain.UnhighlightObject(_sim.carJack);
                    break;

                case InternalStep.PlaceCarJackHighlightedPos:
                    _stepChain
                        .DisableObject(_sim.jackDropZoneOriginalPos)
                        .MakeUngrabbable(_sim.carJack);
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
