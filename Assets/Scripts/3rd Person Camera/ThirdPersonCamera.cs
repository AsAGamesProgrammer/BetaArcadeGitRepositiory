using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    [SerializeField]
    private Transform PlayerTransform;

    [SerializeField]
    [Range(1.0f, 20.0f)]
    private float FollowDistance = 10.0f;

    [Header("Camera Focus Point")]

    [SerializeField]
    [Tooltip("The offset of the camera focus point (relative to the player transform).")]
    private Vector3 FocusPointOffset = Vector3.zero;

    [SerializeField]
    [Tooltip("Visualise the the focus point as a magenta sphere and the camera position as a blue sphere.")]
    private bool VisualiseFocusPoint = false;

    [Header("Camera Angle Limits")]

    [SerializeField]
    [Range(40.0f, 90.0f)]
    private float MaxVerticalAngle = 70.0f;

    [SerializeField]
    [Range(0.0f, 40.0f)]
    private float MinVerticalAngle = 30.0f;

    [Header("Input")]

    //[SerializeField]
    //[Range(1.0f, 300.0f)]
    private float MouseXSensitivity = 200.0f;

    //[SerializeField]
    //[Range(1.0f, 300.0f)]
    private float MouseYSensitivity = 160.0f;

    [SerializeField]
    private bool InvertY = false;

    private Vector3 mCameraOffsetRot = Vector3.zero;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        mCameraOffsetRot.y = transform.eulerAngles.y;
    }

    private void Update()
    {
        UpdatePosition();
        UpdateLookDirection();
    }

    private void OnDrawGizmos()
    {
        if(VisualiseFocusPoint)
        {
            // Drawing a blue sphere to represent the camera position.
            Gizmos.color = Color.blue;
            Vector3 cameraPos = this.transform.position;
            if (!Application.isPlaying)
            {
                var cameraOffset = new Vector3(0.0f, 0.0f, -FollowDistance);
                var cameraRot = Quaternion.Euler(new Vector3(Mathf.Clamp(0.0f, MinVerticalAngle, MaxVerticalAngle), 0.0f, 0.0f));
                cameraPos = PlayerTransform.position + cameraRot * cameraOffset;
            }
            Gizmos.DrawSphere(cameraPos, 0.4f);

            // Drawing a magenta sphere to represent the focus point.
            Gizmos.color = Color.magenta;
            var focusPoint = GetFocusPoint();
            Gizmos.DrawSphere(focusPoint, 0.15f);

            // Drawing a magenta line from the intial camera pos to the focus point.
            Gizmos.DrawLine(cameraPos, focusPoint);
        }
    }


    //------------------------------------------Private Functions------------------------------------------

    private void UpdateLookDirection()
    {
        // Rotating the camera to look at the focus point.
        this.transform.LookAt(GetFocusPoint());
    }

    private void UpdatePosition()
    {
        // Getting player input for the cameras position.
        mCameraOffsetRot.y += Input.GetAxis("Alt Horizontal") * MouseXSensitivity * Time.deltaTime;
        mCameraOffsetRot.x -= Input.GetAxis("Alt Vertical") * MouseYSensitivity * (InvertY ? -1.0f : 1.0f) * Time.deltaTime;
        mCameraOffsetRot.x = Mathf.Clamp(mCameraOffsetRot.x, MinVerticalAngle, MaxVerticalAngle);

        // Updating the camera position, and following the player.
        this.transform.position = PlayerTransform.position + Quaternion.Euler(mCameraOffsetRot) * new Vector3(0.0f, 0.0f, -FollowDistance);
    }

    private Vector3 GetFocusPoint()
    {
        // Calculating the 2d (horizontal) vector from the camera to the player.
        var camToCharacterVec = PlayerTransform.position - this.transform.position;
        camToCharacterVec.y = 0.0f;

        // Calculating the rotation to apply to the focus point offset.
        var targetRot = Quaternion.LookRotation(camToCharacterVec);

        // Calculating the final focus point vector 3.
        return PlayerTransform.position + targetRot * FocusPointOffset;
    }
}
