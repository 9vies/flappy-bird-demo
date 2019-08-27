using System;
using UnityEngine;

namespace Sensors
{
    public class PlayerSensor : MonoBehaviour
    {
        public event SensorEventHandler OnHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var sensor = other.gameObject.GetComponent<PlayerStimuli>();
            if (sensor != null)
            {
                var parent = other.transform.parent;
                var gameObject = parent != null ? parent.gameObject : other.gameObject;
                if (OnHit != null)
                    OnHit(gameObject);
                
            }
        }
    }
}