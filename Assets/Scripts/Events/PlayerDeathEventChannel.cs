using UnityEngine;

namespace Events
{
    public class PlayerDeathEventChannel : MonoBehaviour
    {
        public event PlayerDeathEventHandler OnPlayerDeath;
        
        public void NotifyPlayerDeath()
        {
            if (OnPlayerDeath != null)
                OnPlayerDeath();
        }
    }

    public delegate void PlayerDeathEventHandler();
}