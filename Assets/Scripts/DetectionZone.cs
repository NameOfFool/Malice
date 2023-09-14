using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    // private Collider2D collider;
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    void Awake()
    {
        // collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        detectedColliders.Add(col);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        detectedColliders.Remove(col);
    }
}
