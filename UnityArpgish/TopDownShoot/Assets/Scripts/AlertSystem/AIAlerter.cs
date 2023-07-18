using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AIAlerter : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        IAlertable alertable = other.GetComponent<IAlertable>();
        if (other.GetComponent<IAlertable>() != null)
        {
            alertable.RequestAlert(transform);
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IAlertable alertable = other.GetComponent<IAlertable>();
        if (other.GetComponent<IAlertable>() != null)
        {
            alertable.RequestSilence(transform);
        }
    }
}
