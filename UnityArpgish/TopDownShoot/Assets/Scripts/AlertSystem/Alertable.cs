using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alertable : MonoBehaviour, IAlertable
{
    public List<Transform> alerterList = new List<Transform>();
    public void RequestAlert(Transform alerterTransform)
    {
        if (alerterTransform != null)
        {
            alerterList.Add(alerterTransform);
            return;
        }

        Debug.LogError("Transform is null");
    }
    public void RequestSilence(Transform alerterTransform)
    {
        if (alerterTransform != null)
        {
            alerterList.Remove(alerterTransform);
            return;
        }

        Debug.LogError("Transform is null");
    }

    private void Update()
    {
        for (int i = 0; i < alerterList.Count; i++)
        {
            if (!alerterList[i].gameObject.activeSelf)
            {
                alerterList.RemoveAt(i);
            }
        }
    }

}
