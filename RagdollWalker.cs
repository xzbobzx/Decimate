using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollWalker : MonoBehaviour {

    public Rigidbody headRB;
    public Transform headTarget;
    public float headForce;
    public float headDamper;

    public Rigidbody handL_RB;
    public Transform handL_Position;
    public Transform handL_Target;
    public float handL_Force;
    public float handL_Damper;

    public Rigidbody handR_RB;
    public Transform handR_Position;
    public Transform handR_Target;
    public float handR_Force;
    public float handR_Damper;

    public Rigidbody footL_RB;
    public Transform footL_Target;
    public float footL_Force;
    public float footL_Damper;

    public Rigidbody footR_RB;
    public Transform footR_Target;
    public float footR_Force;
    public float footR_Damper;

    void FixedUpdate()
    {
        headRB.AddForce((( Vector3.ClampMagnitude(headTarget.position - headRB.position, 0.1f) * 10 * headForce) - headRB.velocity * headDamper) * Time.deltaTime);

        handL_RB.AddForceAtPosition(((Vector3.ClampMagnitude(handL_Target.position - handL_RB.position, 0.1f) * 10 * handL_Force) - handL_RB.velocity * handL_Damper) * Time.deltaTime, handL_Position.position);
        handR_RB.AddForceAtPosition(((Vector3.ClampMagnitude(handR_Target.position - handR_RB.position, 0.1f) * 10 * handR_Force) - handR_RB.velocity * handR_Damper) * Time.deltaTime, handR_Position.position);

        footL_RB.AddForce(((Vector3.ClampMagnitude(footL_Target.position - footL_RB.position, 0.1f) * 10 * footL_Force) - footL_RB.velocity * footL_Damper) * Time.deltaTime);
        footR_RB.AddForce(((Vector3.ClampMagnitude(footR_Target.position - footR_RB.position, 0.1f) * 10 * footR_Force) - footR_RB.velocity * footR_Damper) * Time.deltaTime);
    }
}
