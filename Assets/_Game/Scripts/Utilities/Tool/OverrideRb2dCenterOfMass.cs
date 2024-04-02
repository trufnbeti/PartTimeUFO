using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class OverrideRb2dCenterOfMass : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private void Start()
        {
            _rigidbody2D.centerOfMass = _rigidbody2D.transform.InverseTransformPoint(transform.position);
        }
    }
}