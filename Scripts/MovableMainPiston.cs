using SVR.Interactable;
using SVR.Workflow;
using SVR.Workflow.TriangleFactory;
using System.Collections;
using UnityEngine;

public class MovableMainPiston : MonoBehaviour
{
    [SerializeField] private CustomRotatorV2 customRotater;
    [SerializeField] private CustomRotatorV2 customRotaterJackHandle;

    private Vector3 initialPosPiston;
    [Header("Piston variables")]
    [SerializeField] private float verticalIncrements;
    [SerializeField] private float maxVerticalLimit = 0.17f;
    private bool canLiftPiston;
    [SerializeField] private Transform piston;

    [Header("Car variables")]
    [SerializeField] private Transform carPivot;
    [SerializeField] private float carTiltIncrements;
    [SerializeField] private float maxTiltAngleCar = 6f;

    [SerializeField] StepsTireChange step;

    private void Start()
    {
        initialPosPiston = piston.localPosition;
        customRotater.ClockwiseLimitReachedEvent += PistonDown;
        customRotater.CounterClockwiseLimitReachedEvent += PistonUp;
        customRotaterJackHandle.ClockwiseLimitReachedEvent += PushPistonThreadDown;
    }

    private void OnDestroy()
    {
        customRotater.ClockwiseLimitReachedEvent -= PistonDown;
        customRotater.CounterClockwiseLimitReachedEvent -= PistonUp;
        customRotaterJackHandle.ClockwiseLimitReachedEvent -= PushPistonThreadDown;
    }

    private void PistonUp(CustomRotatorV2 rotater)
    {
        if (!canLiftPiston)
        {
            canLiftPiston = true;
            var pos = piston.localPosition;
            pos.y += verticalIncrements;
            pos.y = Mathf.Clamp(pos.y, 0, maxVerticalLimit);
            piston.localPosition = pos;

            var localRotation = carPivot.localEulerAngles;
            localRotation.x += carTiltIncrements;
            localRotation.x = Mathf.Clamp(localRotation.x, 0, maxTiltAngleCar);
            carPivot.localEulerAngles = localRotation;

            //if (pos.y >= maxVerticalLimit)
            if (Mathf.Approximately(pos.y,maxVerticalLimit))
                step.CompleteCondition();
        }
    }

    public void PistonUp()
    {
        if (!canLiftPiston)
        {
            canLiftPiston = true;
            var pos = piston.localPosition;
            pos.y += verticalIncrements;
            pos.y = Mathf.Clamp(pos.y, 0, maxVerticalLimit);
            piston.localPosition = pos;

            var localRotation = carPivot.localEulerAngles;
            localRotation.x += carTiltIncrements;
            localRotation.x = Mathf.Clamp(localRotation.x, 0, maxTiltAngleCar);
            carPivot.localEulerAngles = localRotation;
        }
    }

    private void PistonDown(CustomRotatorV2 rotater)
    {
        //Debug.Log("Limit reached");
        canLiftPiston = false;
    }

    public void PushPistonThreadDown(CustomRotatorV2 rotater)
    {
        customRotaterJackHandle.enabled = false;
        StartCoroutine(PushThreadDown());
    }

    private IEnumerator PushThreadDown()
    {
        var pistonThreadYPos = piston.localPosition;
        while (pistonThreadYPos.y > 0)
        {
            yield return new WaitForSeconds(.01f);
            pistonThreadYPos.y -= 0.0005f;
            piston.localPosition = pistonThreadYPos;
        }
        //step.CompleteCondition();
    }
}
