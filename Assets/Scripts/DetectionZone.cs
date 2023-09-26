using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemains;
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        detectedColliders.Add(col);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        detectedColliders.Remove(col);

        if (detectedColliders.Count == 0)
        {
            noCollidersRemains.Invoke();
        }
    }
}
