﻿using UnityEngine;
using UnityEngine.EventSystems;


public interface IMyMenuButton
{
    void click();
    void OnPointerClick(PointerEventData eventData);
    void OnPointerEnter(PointerEventData eventData);
    void OnPointerExit(PointerEventData eventData);
    void setState(string tag);

    ButtonClicked getButtonClicked();
    void setButtonClicked(ButtonClicked buttonClicked);
}

public class MyMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IMyMenuButton
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

    public ButtonClicked buttonClicked;
  

    public ButtonClicked getButtonClicked()
    {
        return buttonClicked;
    }

    public void setButtonClicked(ButtonClicked buttonClicked)
    {
        this.buttonClicked = buttonClicked;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        menuButtonGroup.OnTabEnter(this);
        click();

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
        if (currentState == State.selected)
        {
            animator.SetBool("Selected", true);
            if (pressed == true)
            {
                animator.SetBool("Pressed", true);
                currentState = State.pressed;
            }
        }
        else if (currentState == State.pressed)
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

    public void click()
    {
        buttonClicked.ButtonEvent(this);
    }

    public void setState(string tag)
    {
        State newState = (State)System.Enum.Parse(typeof(State), tag);
        currentState = newState;
    }


}
