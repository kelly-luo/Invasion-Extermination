/*Credits Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class plays the credits, and then loads the main menu
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  11.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Animator animator;

    //
    // Start()
    // ~~~~~~~~~~~
    // Start Credit animations.
    //
    void Start()
    {
        Destroy(GameObject.Find("GameManager"));
        if (animator != null) animator.SetBool("Animation", true);
        StartCoroutine(this.timerWait());
    }


    //
    // timerWait()
    // ~~~~~~~~~~~
    // Waits for 15 seconds and then load main menu
    //
    // returns      IEnumerator for Coroutine
    //
    private IEnumerator timerWait()
    {
        yield return new WaitForSeconds(15f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenuV2", LoadSceneMode.Single);
    }
}
