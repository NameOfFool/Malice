using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffCheck : MonoBehaviour
{
    public bool NearCliff;
    private void OnTriggerExit2D(Collider2D other)
    {
        NearCliff = true;
    }
}
