using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }
}

