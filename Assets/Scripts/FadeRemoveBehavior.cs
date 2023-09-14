using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    private SpriteRenderer sr;
    private GameObject objToRemove;
    Color startColor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        sr = animator.GetComponent<SpriteRenderer>();
        startColor = sr.color;
        objToRemove = animator.gameObject;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed +=Time.deltaTime;
        float newalpha = startColor.a * (1 - (timeElapsed / fadeTime));
        sr.color = new Color(startColor.r, startColor.g, startColor.b, newalpha);

        if(timeElapsed > fadeTime)
        {
            Destroy(objToRemove);
        }
    }
}
