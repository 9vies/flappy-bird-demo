using UnityEngine;

namespace Events
{
    public class ScoreEventChannel : MonoBehaviour
    {
        public event ScoreEventHandler OnScoreChanged;

        public void NotifyScoreChanged()
        {
            if (OnScoreChanged != null) OnScoreChanged();
        }
    }
    
    public delegate void ScoreEventHandler();
}