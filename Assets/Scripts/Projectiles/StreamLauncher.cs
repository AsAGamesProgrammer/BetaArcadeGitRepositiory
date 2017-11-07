using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamLauncher : MonoBehaviour {


    [SerializeField]
    private Vector3 StreamDirection = Vector3.forward;

    [SerializeField]
    private Vector3 StreamStartOffset = Vector3.zero;

    [SerializeField]
    private float MaxStreamLength = 10.0f;

    [SerializeField]
    private BoxCollider StreamCollider;

    [SerializeField]
    private Transform StreamGraphics;

    [SerializeField]
    private float StreamWidth = 0.2f;

    [SerializeField]
    private LayerMask RaycastLayers;

    private Vector3 mStreamVector = Vector3.zero;

    private Vector3 pStreamStartPos { get { return this.transform.position + this.transform.TransformDirection(StreamStartOffset); } }


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Update()
    {
        UpdateStreamVector();

        StreamCollider.transform.position = pStreamStartPos + mStreamVector * 0.5f;
        StreamCollider.transform.LookAt(pStreamStartPos);
        SetGlobalScale(StreamCollider.transform, new Vector3(StreamWidth, 100.0f, mStreamVector.magnitude));

        StreamGraphics.position = pStreamStartPos + mStreamVector * 0.5f;
        StreamGraphics.LookAt(pStreamStartPos);
        SetGlobalScale(StreamGraphics, new Vector3(StreamWidth, StreamWidth, mStreamVector.magnitude));
    }

    private void OnDrawGizmos()
    {
        UpdateStreamVector();
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(pStreamStartPos, 0.25f);
        Gizmos.DrawLine(pStreamStartPos, pStreamStartPos + mStreamVector);
    }

    //------------------------------------------Private Functions------------------------------------------

    private void UpdateStreamVector()
    {
        RaycastHit hit;
        if (Physics.Raycast(pStreamStartPos, this.transform.TransformDirection(StreamDirection).normalized, out hit, MaxStreamLength, RaycastLayers))
            mStreamVector = hit.point - pStreamStartPos;
        else
            mStreamVector = this.transform.TransformDirection(StreamDirection).normalized * MaxStreamLength;
    }

    private void SetGlobalScale(Transform trans, Vector3 scale)
    {
        trans.localScale = Vector3.one;
        var lossyScale = trans.lossyScale;
        scale.x /= lossyScale.x;
        scale.y /= lossyScale.y;
        scale.z /= lossyScale.z;
        trans.localScale = scale;
    }
}
