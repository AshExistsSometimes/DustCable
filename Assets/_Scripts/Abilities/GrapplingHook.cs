using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Public Variables

    public LayerMask GrappleableObjects;

    public Transform GrappleOrigin;
    public Transform PlayerCam;
    public Transform Player;
    [Space]
    public float MaxGrappleDist = 100f;
    [Space]
    public float GrappleSpringiness = 4.5f;
    public float GrappleDamping = 7f;


    // Private Variables

    private LineRenderer _lineRenderer;
    private Vector3 _grapplePoint;
    private SpringJoint _springJoint;


    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (InputHandler.GetKeyDown(InputHandler.GrappleKey))
        {
            GrappleStart();
        }
        else if (InputHandler.GetKeyUp(InputHandler.GrappleKey))
        {
            GrappleEnd();
        }
    }

    private void LateUpdate()
    {
        DrawGrapplingHook();
    }

    void GrappleStart()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerCam.position, PlayerCam.forward, out hit, MaxGrappleDist))
        {
            _grapplePoint = hit.point;

            _springJoint = Player.gameObject.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = _grapplePoint;

            float distanceFromPoint = Vector3.Distance(Player.position, _grapplePoint);

            // Distance Grappling Hook tries to keep from point
            _springJoint.maxDistance = distanceFromPoint * 0.8f;
            _springJoint.minDistance = distanceFromPoint * 0.25f;

            _springJoint.spring = GrappleSpringiness;
            _springJoint.damper = GrappleDamping;
            _springJoint.massScale = 4.5f;

            _lineRenderer.positionCount = 2;
        }
    }

    void DrawGrapplingHook()
    {
        if (!_springJoint) return;

        _lineRenderer.SetPosition(0, GrappleOrigin.position);
        _lineRenderer.SetPosition(1, _grapplePoint);
    }


    void GrappleEnd()
    {
        _lineRenderer.positionCount = 0;
        Destroy(_springJoint);
    }
}
