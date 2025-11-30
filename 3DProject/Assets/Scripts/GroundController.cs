using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    
    [SerializeField]
    private float _groundDistanceTolerance;

    //will let us see which layers to check
    [SerializeField]
    private LayerMask _groundLayerMask;

    public bool IsGrounded { get; private set;}

    private CapsuleCollider _capsuleCollider;

    //? makes it nullable 
    public float? DistanceToGround { get; private set; }

    private void Awake()
    {
        //used to detect the ground
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        //calculate radius of the sphere we're casting
        float sphereCastRadius = _capsuleCollider.radius - 0.1f;


        //origin of sphere cast
        Vector3 sphereCastOrigin = transform.position + new Vector3(0, _capsuleCollider.radius, 0);

        //execute sphere cast
        bool isGroundedBelow = Physics.SphereCast(
            sphereCastOrigin,
            sphereCastRadius,
            Vector3.down,
            out RaycastHit hitInfo,
            1000,
            _groundLayerMask,
            QueryTriggerInteraction.Ignore);

        //check if player is grounded
        if (isGroundedBelow)
        {
            DistanceToGround = transform.position.y - hitInfo.point.y;
        }

        //if there is not ground set to null
        else
        {
            DistanceToGround = null;
        }

        //check that its grounded and less than tolerance value 
        IsGrounded = isGroundedBelow && DistanceToGround <= _groundDistanceTolerance;
    }
}
