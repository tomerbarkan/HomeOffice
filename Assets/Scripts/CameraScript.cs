using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	private float moveAmount = 30;

    // Start is called before the first frame update
    void Start() {
	    float aspect = (float)Screen.width / Screen.height;
	    float expectedAspect = 16f / 9f;
	    float relativeAspect = aspect / expectedAspect;

	    transform.position = transform.position + transform.forward * (relativeAspect - 1) * moveAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
