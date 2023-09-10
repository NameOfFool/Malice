using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;


    private Vector2 _startingPosition;

    private float _startingZ;

    private Vector2 _canMoveSinceStart => (Vector2)cam.transform.position - _startingPosition;

    private float _zDistanceFromTarget => transform.position.z - followTarget.position.z;

    private float _clippingPlane => (cam.transform.position.z +(_zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    private float _parallaxFactor => Mathf.Abs(_zDistanceFromTarget)/_clippingPlane;

    void Start()
    {
        _startingPosition = transform.position;
        _startingZ = transform.position.z;
    }

    void Update()
    {
        Vector2 newPosition = _startingPosition + _canMoveSinceStart * _parallaxFactor;
        transform.position = new(newPosition.x, newPosition.y, _startingZ);
    }
}
