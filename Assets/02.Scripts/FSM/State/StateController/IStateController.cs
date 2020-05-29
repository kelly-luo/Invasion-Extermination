using UnityEngine;

namespace IEGame.FiniteStateMachine
{
    public interface IStateController
    {

        //stats of something that inherit from ObjectStats


        void TransitionToState(State nextState);

        void EquipWeapon(GameObject Weapon);

        void TakeDamage(float Damage);

        void Attack();

        void OnDeath();
    }
}
