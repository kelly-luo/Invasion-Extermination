using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    
    public enum State
    {
        selected,
        deselected,
        pressed,
        idle
    }

    public Animator animator;

    public State currentState;

    public bool pressed = false;

    public ButtonClicked buttonClicked;

    public bool enable { get; set; } = true;


    void Start()
    {
        currentState = State.idle;
        buttonClicked = GetComponent<ButtonClicked>();
    }

    void Update()
    {

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pressed") && pressed)
        {

            animator.SetBool("Pressed", false);
            pressed = false;
        }
    }

    public ButtonClicked getButtonClicked()
    {
        return buttonClicked;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (enable)
        {
            if (animator != null) animator.SetBool("Pressed", true);
            pressed = true;
            click(eventData);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enable)
        {
            if (animator != null) animator.SetBool("Selected", true);
            Hover(eventData);
        }
     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (enable)
        {
            if (animator != null) animator.SetBool("Selected", false);
            HoverExit(eventData);
        }
      
    }

    public void click(PointerEventData eventData)
    {
        if (enable)
        {
            buttonClicked.ButtonEvent(eventData);
        }
    
    }

    public void Hover(PointerEventData eventData)
    {
        if (enable)
        {
            buttonClicked.ButtonHover(eventData);
        }

    }

    public void HoverExit(PointerEventData eventData)
    {
        if (enable)
        {
            buttonClicked.ButtonHoverExit(eventData);
        }

    }


    public void setButtonClicked(ButtonClicked buttonClicked)
    {
        this.buttonClicked = buttonClicked;
    }

    public void disableButton()
    {
        enable = false;
        if(animator != null) animator.SetBool("Selected", false);
    }
    public void enableButton()
    {
        enable = true;
        if (animator != null) animator.SetBool("Selected", false);
    }





}
