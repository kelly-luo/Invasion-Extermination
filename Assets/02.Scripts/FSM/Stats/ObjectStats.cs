using UnityEngine;

namespace IEGame.FiniteStateMachine
{
    [System.Serializable]
    public abstract class ObjectStats : ScriptableObject
    {
        [field: SerializeField]
        public float HP { get; set; }
    }
}