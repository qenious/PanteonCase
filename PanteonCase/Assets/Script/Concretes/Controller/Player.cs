using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PanteonCase
{

    public  class Player : MonoBehaviour, IDamageable
    {

        private Movement _movement;
        private Rigidbody _rb;

        public bool _damageTaken { get; set; } = false;


        public void Awake()
        {
            _movement = new Movement(this);
            _rb = GetComponent<Rigidbody>();
        }

        private void Update() => Damage();

        public void FixedUpdate() => _movement.PlayerMovement(50f, 10f, _rb);





        public void Damage()
        {
            if (_damageTaken)
            {
                transform.position = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-1f, 6f));
                _damageTaken = false;
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("obstacle"))
            {
                _damageTaken = true;
            }
        }






















    }
}