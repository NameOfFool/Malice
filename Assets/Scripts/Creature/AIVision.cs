using System.Collections;
using System.Collections.Generic;
using CustomAttributes;
using UnityEngine;

public class AIVision : MonoBehaviour
{
    [SerializeField]private float distance = 10;
    [ReadOnly][SerializeField]private float _height = 0;
    public Color meshColor = Color.red;
    private SpriteRenderer _sR;

    void Start()
    {
        _height = GetComponentInParent<Transform>().position.y;
    }

    void Update()
    {

    }
}
