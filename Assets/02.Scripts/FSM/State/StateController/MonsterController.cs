using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEGame.FiniteStateMachine;

public class MonsterController : MonoBehaviour, IStateController
{
    #region state
    [field: SerializeField]
    public State CurrentState { get; set; }
    //state of default RemainState value (any value but it have to match with the return value of transition ) 
    [field: SerializeField]
    public State RemainState { get; set; }
    #endregion
    #region Animation
    public Animator Animator { get; set; }
    #endregion

    public ObjectStats Stats { get; set; }

    public Transform ObjectTransform { get; set; }

    public ICharacterTranslate MonsterTranslate {get; set;}

    void Awake()
    {
        this.Stats = new MonsterStats();
        this.ObjectTransform = gameObject.transform;
        this.Animator = GetComponent<Animator>();

        this.MonsterTranslate = new MonsterTranslate(ObjectTransform);
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        CurrentState.UpdateState(this);
    }


    public void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
        }
    }

    public void TakeDamage(float Damage)
    {
        Stats.HP= Damage;
    }

    public void OnDeath()
    {

    }


}
