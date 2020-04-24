using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    public enum State
    {
        selected,
        deselected,
        pressed,
        idle
    }

    [SerializeField] protected MyMenuButtonGroup menuButtonGroup;
    public Animator animator;
    //[SerializeField] AnimatorFunctions animatorFunctions;

    public State currentState;

    public bool pressed =false;

    public bool clicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        menuButtonGroup.OnTabEnter(this);
        ButtonClicked();
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuButtonGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuButtonGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == State.selected)
        {
            animator.SetBool("Selected", true);
            if(pressed == true)
            {
                animator.SetBool("Pressed", true);
                currentState = State.pressed;
            }
        }else if(currentState == State.pressed)
        {
            animator.SetBool("Pressed", false);
            pressed = false;
            currentState = State.selected;
        }
        if (currentState == State.deselected)
        {
            animator.SetBool("Selected", false);
        }
       
    }

    public void ButtonClicked()
    {
        clicked = !clicked;
    }

    public void setState(string tag)
    {
        State newState = (State)System.Enum.Parse(typeof(State), tag);
        currentState = newState;
    }


}
