using UnityEngine;
using System.Collections;

public class SimpleMoveConstantly : MonoBehaviour {
	[Header("Moves the object by these settings per second")]
	public float moveX;
	public float moveY;
	public float moveZ;
	public float rotateX;
	public float rotateY;
	public float rotateZ;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		this.transform.Translate (moveX * Time.deltaTime, moveY * Time.deltaTime, moveZ * Time.deltaTime);
		this.transform.Rotate(new Vector3(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime));
	}
}