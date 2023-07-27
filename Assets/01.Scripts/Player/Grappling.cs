using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [SerializeField]
    private LayerMask grabbableLayer;
	[SerializeField]
	private Transform grappleStart;
	private LineRenderer lineRenderer;

	private bool grappling = false;
	private Vector3 grapplePoint;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			StartGrapple();
		}
		if (Input.GetMouseButtonUp(0))
		{
			StopGrapple();
		}

		if (grappling)
			OnGrappling();
	}

	private void StartGrapple()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit, 100f, grabbableLayer);

		if (hit.collider == null)
		{
			StopGrapple();
			return;
		}

		grapplePoint = hit.point;
		print(hit.collider.name);
		grappling = true;
		lineRenderer.enabled = true;
		lineRenderer.SetPosition(1, grapplePoint);
	}

	private void StopGrapple()
	{
		grappling = false;
		grapplePoint = Vector3.zero;
		lineRenderer.enabled = false;
	}

	private void OnGrappling()
	{
		lineRenderer.SetPosition(0, grappleStart.position);
	}
}
