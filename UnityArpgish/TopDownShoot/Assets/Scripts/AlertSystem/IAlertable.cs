using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlertable 
{
    void RequestAlert(Transform alertersTransform);
    void RequestSilence(Transform alertersTransform);
}
