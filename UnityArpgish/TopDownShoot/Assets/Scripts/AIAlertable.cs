using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIAlertable 
{
    bool RequestAlert();
    bool RequestSilence();
}
