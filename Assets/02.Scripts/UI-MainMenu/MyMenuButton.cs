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

    public State currentState;

    public bool pressed = false;

    private ButtonClicked buttonClicked;


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


    void Start()
    {
        currentState = State.idle;
        buttonClicked = GetComponent<ButtonClicked>();
    }

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
        buttonClicked.ButtonEvent(this);
    }

    public void setState(string tag)
    {
        State newState = (State)System.Enum.Parse(typeof(State), tag);
        currentState = newState;
    }


}
