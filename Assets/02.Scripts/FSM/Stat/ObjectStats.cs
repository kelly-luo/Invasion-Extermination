using UnityEngine;

namespace IEGame.FiniteStateMachine
{
    public abstract class ObjectStats : ScriptableObject
    {
        [field: SerializeField]
        public float HP { get; set; }
    }
}