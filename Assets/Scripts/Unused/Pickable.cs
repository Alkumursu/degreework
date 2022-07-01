using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FixedJoint))]
public class Pickable : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;

    [SerializeField] private FixedJoint _fixedJoint;
    public FixedJoint Joint => _fixedJoint;

    private void Awake()
    {
        if(!_fixedJoint && ! TryGetComponent<FixedJoint>(out _fixedJoint)) _fixedJoint = gameObject.AddComponent<FixedJoint>();
        if (!_rigidbody && !TryGetComponent<Rigidbody>(out _rigidbody)) _rigidbody = gameObject.AddComponent<Rigidbody>();

        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
