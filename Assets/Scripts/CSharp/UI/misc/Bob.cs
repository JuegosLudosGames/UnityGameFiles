using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{

	public float range = 1;
	public float speed = 1;

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(transform.position.x, (range * Mathf.Sin(speed * (Time.time * Mathf.Rad2Deg))) + transform.position.y, transform.position.z);
		//Debug.Log(transform.position);
	}
}
