using SVR.Interactable;
using SVR.Workflow;
using SVR.Workflow.TriangleFactory.Scripts.Mechanics;
using System.Collections;
using UnityEngine;

public class CarJackHandle : MonoBehaviour
{
    [SerializeField] private CustomRotatorV2 customRotater;

    [SerializeField] private Transform carPivot;
    private bool canLowerCar;

    [SerializeField] StepsTireChange step;

    private void Start()
    {
        customRotater.ClockwiseLimitReachedEvent += StartLowerCar;      
    }

    private void OnDestroy()
    {
        customRotater.ClockwiseLimitReachedEvent -= StartLowerCar;        
    }

    private void StartLowerCar(CustomRotatorV2 rotater)
    {
        Debug.Log("in here");
        if (!canLowerCar)
        {
            canLowerCar = true;
            StartCoroutine(nameof(LowerCar));
        }
    }

    private IEnumerator LowerCar()
    {
        var carPivotAngle = carPivot.localEulerAngles;
        while (carPivotAngle.x > 0)
        {
            yield return new WaitForSeconds(.01f);
            carPivotAngle.x -= 0.02f;
            carPivot.localEulerAngles = carPivotAngle;
        }
        canLowerCar = false;
        step.CompleteCondition();
    }
}