/*RoundPopUp Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles displaying the round completion
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  14.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  System support packages
 */
using System.Collections;
//UnityEngine support package
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
    // Waits for 3 seconds to hide the round completed banner
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
