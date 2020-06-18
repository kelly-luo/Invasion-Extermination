using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPopUp : MonoBehaviour
{
    public Animator animator;
    public bool playing;

    public void playAnimation()
    {
        playing = true;
        if(animator!= null)animator.SetBool("Activate", true);
        StartCoroutine(this.timerWait());
    }


    //
    // timerWait()
    // ~~~~~~~~~~~
    // Waits for 3 seconds and then disables the save completed/incompleted text
    //
    // returns      IEnumerator for Coroutine
    //
    private IEnumerator timerWait()
    {
        yield return new WaitForSeconds(3f);
        if (animator != null) animator.SetBool("Activate", false);
        playing = false;
    }
}
