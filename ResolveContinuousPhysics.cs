// What this file does is it will give rigidbodies with continuous collision detection proper collisions, when Unity itself fails.
// It might occur that a rigidbody hits a slanted angle at a high speed, and will bounce, but not rotate properly.
// This script fixes that.
// It also takes into account any physics materials applies to the rigidbody's collider.

// It kind of works with dynamic rigidbodies, but they're not 100% supported and I can't guarantee proper resolving in such case.
// Copright Yannic Geurts 2017, you're free to use this for whatever as long as you give credits.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ResolveContinuousPhysics : MonoBehaviour {

    Rigidbody rb;

    //public Transform arrow;
    //public bool debug;

    private Vector3 onCollisionRotVel;
    private Vector3 afterCollisionRotVel;

    private Vector3 localV3;
    private Vector3 globalV3;
    private float force;
    private Collision collision;
    private float angle;

    private Vector3 previousVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        previousVelocity = rb.velocity;
    }

    private void OnCollisionEnter( Collision col )
    {
        onCollisionRotVel = rb.angularVelocity;

        collision = col;

        localV3 = transform.InverseTransformPoint(collision.contacts[0].point);
        globalV3 = transform.TransformPoint(-localV3);

        force = collision.impulse.magnitude / 2;

        angle = Vector3.Angle(collision.contacts[0].normal, previousVelocity);
        angle = angle - 90;
        angle = angle / 90;

        /*
        if( debug )
        {
            Debug.Log(angle);

            Instantiate(arrow, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            Instantiate(arrow, globalV3, Quaternion.LookRotation(-collision.contacts[0].normal));

            Instantiate(arrow, rb.position, Quaternion.LookRotation(collision.contacts[0].normal));
            Instantiate(arrow, rb.position, Quaternion.LookRotation(previousVelocity));
        }
        */

        StartCoroutine("AfterCollision");
    }

    IEnumerator AfterCollision()
    {
        yield return new WaitForFixedUpdate();

        afterCollisionRotVel = rb.angularVelocity;

        if( (onCollisionRotVel - afterCollisionRotVel).magnitude < 1f )
        {
            float weight = 1;

            if( collision.rigidbody != null )
            {
                if( rb.mass > collision.rigidbody.mass )
                {
                    weight = collision.rigidbody.mass / rb.mass;
                }
            }

            rb.AddForceAtPosition(collision.contacts[0].normal * force * weight, collision.contacts[0].point, ForceMode.Impulse);
            rb.AddForceAtPosition(-collision.contacts[0].normal * force * weight, globalV3, ForceMode.Impulse);

            float friction;
            Collider thisCollider = collision.contacts[0].thisCollider;

            if( thisCollider.material.frictionCombine == PhysicMaterialCombine.Average )
            {
                friction = (thisCollider.material.dynamicFriction + collision.contacts[0].otherCollider.material.dynamicFriction) / 2;
            }
            else if( thisCollider.material.frictionCombine == PhysicMaterialCombine.Minimum )
            {
                friction = Mathf.Min(thisCollider.material.dynamicFriction, collision.contacts[0].otherCollider.material.dynamicFriction);
            }
            else if( thisCollider.material.frictionCombine == PhysicMaterialCombine.Maximum )
            {
                friction = Mathf.Max(thisCollider.material.dynamicFriction, collision.contacts[0].otherCollider.material.dynamicFriction);
            }
            else
            {
                friction = thisCollider.material.dynamicFriction * collision.contacts[0].otherCollider.material.dynamicFriction;
            }

            rb.AddForce(-rb.velocity * friction * angle, ForceMode.VelocityChange);

            /*
            if( debug )
            {
                Debug.Log("BAM");
            }
            */
        }

        /*
        if( debug )
        {
            Debug.Log((onCollisionRotVel - afterCollisionRotVel) + " thus: " + afterCollisionRotVel.magnitude);
        }
        */
    }
}
