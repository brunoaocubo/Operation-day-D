using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] 
    private float _forceSpeed;
    void Start()
    {
		transform.position = transform.parent.position;
		rb.AddForce(new Vector3(0, 0, _forceSpeed), ForceMode.Impulse);
	}

	private void OnEnable()
	{
		rb = GetComponent<Rigidbody>();

	}

	private void OnDisable()
	{
		this.transform.position = transform.parent.position;
	}

	void Update()
    {


    }
}
