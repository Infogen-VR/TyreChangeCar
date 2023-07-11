using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonOn : MonoBehaviour
{
    public UnityEvent onPressGreen;
    public GameObject greenButton;
    public GameObject redButton;
    GameObject presser;
    public bool ispressed;
    internal object onClick;
   

    // Start is called before the first frame update
    void Start()
    {
      
        ispressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!ispressed)
        {
            greenButton.transform.localPosition = new Vector3(0.62f, -1.304795f, -0.03350493f);
            presser = other.gameObject;
            onPressGreen.Invoke();       
            redButton.transform.localPosition = new Vector3(0.9427412f, 1.711663f, -0.03350493f);
            ispressed = true;
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        ispressed = false;
    }*/

}
