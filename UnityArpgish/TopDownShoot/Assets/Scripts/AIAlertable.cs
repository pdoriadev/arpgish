using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIAlertable 
{
    void RequestAlert(Transform alertersTransform);
    void RequestSilence(Transform alertersTransform);
}
