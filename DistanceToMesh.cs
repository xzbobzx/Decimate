using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToMesh : MonoBehaviour {

    public Transform cameraTransform;
    public MeshFilter thisMesh;
    public MeshRenderer thisRenderer;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("Gameobject Distance: " + Vector3.Distance(thisRenderer.transform.position, cameraTransform.transform.position) + "\n" +
            "Mesh Center Distance: " + Vector3.Distance(thisRenderer.bounds.center, cameraTransform.transform.position) + "\n" +
            "Bound Closest Distance: " + Vector3.Distance(thisRenderer.bounds.ClosestPoint(cameraTransform.transform.position), cameraTransform.transform.position) + "");
	}
}
