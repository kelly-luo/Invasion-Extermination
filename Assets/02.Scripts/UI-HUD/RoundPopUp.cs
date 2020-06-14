using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPopUp : MonoBehaviour
{
    public GameObject gameObject;
    public Animator animator;

    public void playAnimation()
    {
        animator.SetBool("Activate", true);
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
        animator.SetBool("Activate", false);
    }
}
