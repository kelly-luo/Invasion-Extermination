using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
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
        SceneManager.LoadScene("MainMenuV2", LoadSceneMode.Single);
    }
}
