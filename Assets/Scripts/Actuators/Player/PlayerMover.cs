using System;
using UnityEngine;

namespace Game
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Vector2 flapForce = Vector2.up * 5;

        private Rigidbody2D _rigidBody2D;
        private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }

        public void Flap()
        {
            _rigidBody2D.velocity = flapForce;
        }
    }
}