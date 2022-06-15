using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    // Current character's rigid body, needs adjustments
    [SerializeField] private Rigidbody _rb;
    private Pickable currentPickable;

    public void PickUp(Pickable pickable)
    {
        if (currentPickable)
        {
            Debug.LogError($"Have already picked up {currentPickable.name}", this);
            return;
        }

        currentPickable = pickable;
        pickable.Rigidbody.constraints = RigidbodyConstraints.None;
        pickable.Joint.connectedBody = _rb;
    }

    public void Release()
    {
        //If have not picked anything of object was destroyed -> nothing to do
        if (!currentPickable) return;

        currentPickable.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        currentPickable.Joint.connectedBody = null;
        currentPickable = null;

    }

 
}
