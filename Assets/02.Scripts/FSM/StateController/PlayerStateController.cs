using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

public class PlayerStateController : MonoBehaviour, IStateController
{
    [field: SerializeField]
    public State CurrentState { get; set; }
    //state of default RemainState value (any value but it have to match with the return value of transition ) 

    [field: SerializeField]
    public State RemainState { get; set; }

    [field: SerializeField]
    public ObjectStats Stats { get; set;}

    [field: SerializeField]
    public Transform Transform { get; set; }

    public float StateTimeElapsed { get; set; }
    // state will change depends on this class (speed) 
    public ICharacterTranslate playerMovement { get; set; }



    public PlayerStateController(ICharacterTranslate playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    void Awake()
    {

    }
    void Start()
    {

    }

    void Update()
    { 
        // adding check active function .


        //update scene
        //CurrentState.UpdateState(this);
    }
    //check attack timer (so player do not "attack" every frames); 
    public bool CheckIsAttackReady(float duration)
    {
        StateTimeElapsed += Time.deltaTime;
        return (StateTimeElapsed >= duration);
    }

    public void OnExitState()
    {
        StateTimeElapsed = 0;
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != RemainState)
        {
            CurrentState = nextState;
            this.OnExitState();
        }
    }

}





