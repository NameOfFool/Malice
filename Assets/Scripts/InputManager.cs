using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private InputManager()
    {

    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}
