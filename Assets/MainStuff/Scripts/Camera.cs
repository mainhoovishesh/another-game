using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 Speed;
    public enum CameraState { none, FollowPosition, LookAtPlayer, both };
    public CameraState cameraState;
    public float smoothSpeed = 0.125f;

    public Vector3 offset;
    private Vector3 nextPosition;
    private void Update()
    {
        Vector3 DesiredPosition = target.position + offset;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, smoothSpeed);
        transform.position = DesiredPosition;
        switch (cameraState)
        {
            case CameraState.none: break;
            case CameraState.FollowPosition:
                FollowPosition();
                break;
            case CameraState.LookAtPlayer:
                LookAtPlayer();
                break;
            case CameraState.both:
                FollowPosition();
                LookAtPlayer();
                break;
        };
    }

    public float fieldOfView { get; internal set; }

    void FollowPosition()
    {
        nextPosition.x = Mathf.Lerp(this.transform.position.x, target.position.x, Speed.x * Time.deltaTime);
        nextPosition.y = Mathf.Lerp(this.transform.position.y, target.position.y, Speed.y * Time.deltaTime);
        nextPosition.z = Mathf.Lerp(this.transform.position.z, target.position.z, Speed.z * Time.deltaTime);
    }
    void LookAtPlayer()
    {
        this.transform.LookAt(target.position);
    }

}
