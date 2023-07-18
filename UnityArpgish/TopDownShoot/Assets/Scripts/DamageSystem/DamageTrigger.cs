using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DamageTrigger: MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        Damageable dam = coll.gameObject.GetComponent<Damageable>();
        if (dam != null)
        {
            dam.ApplyDamageRequest(1);
        }
    }
}
