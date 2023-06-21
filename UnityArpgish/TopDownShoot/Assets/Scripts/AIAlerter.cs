using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AIAlerter : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        AIAlertable alertable = other.GetComponent<AIAlertable>();
        if (other.GetComponent<AIAlertable>() != null)
        {
            alertable.RequestAlert(transform);
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AIAlertable alertable = other.GetComponent<AIAlertable>();
        if (other.GetComponent<AIAlertable>() != null)
        {
            alertable.RequestSilence(transform);
        }
    }
}
