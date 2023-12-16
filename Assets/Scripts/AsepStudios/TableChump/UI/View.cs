using UnityEngine;

namespace AsepStudios.UI
{
    public abstract class View : MonoBehaviour
    {
        public bool IsInitialized { get; private set; }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        public virtual void PassArgs(object args = null)
        {

        }
    }

}