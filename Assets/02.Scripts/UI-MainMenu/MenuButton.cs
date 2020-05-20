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
      
            animator.SetBool("Pressed", true);
            pressed = true;
            click(eventData);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enable)
        {
            animator.SetBool("Selected", true);
        }
     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (enable)
        {
            animator.SetBool("Selected", false);
        }
      
    }

    public void click(PointerEventData eventData)
    {
        buttonClicked.ButtonEvent(eventData);
    }


    public void setButtonClicked(ButtonClicked buttonClicked)
    {
        this.buttonClicked = buttonClicked;
    }

    public void disableButton()
    {
        enable = false;
        animator.SetBool("Selected", false);
    }





}
