using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PanteonCase
{

    public class Player : MonoBehaviour,  IRespawnable
    {
        public delegate void PaintingWall();
        public static event PaintingWall paintWall;

        private PlayerMovement _movement;
        private Rigidbody _rb;
        private Animator _anim;

        private float moveBorder = 9f;
        private float _yDeathPos = -8f;
        private float _horizontalSpeed = 10f;

        [SerializeField] private GameObject _finishWall;

       

        
        public bool _damageTaken { get; set; } = false;
        public float xBorder => moveBorder;



        public void Awake()
        {
   
            _rb = GetComponent<Rigidbody>();
            _movement = new PlayerMovement(this);
            _anim = GetComponent<Animator>();

        }

        private void Update() => WallIsPainting();



        public void FixedUpdate()
        {
            CharacterRespawn(_yDeathPos);
            _movement.PlayerMove(60f, _horizontalSpeed);
            FinishMovement();
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "finishStructure")
            {
                PlayerWin();
            }
        }



        public void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("rotatingObstacle"))
            {
               RotatingObstacleForce();
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("obstacle"))
            {
                Respawning();
            }
        }



        private void WallIsPainting()
        {
            if (_movement.isFinish && paintWall != null)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    paintWall();
                }
;
            }
        }

        public void RotatingObstacleForce()

        {
            _rb.AddForce(Vector3.right * Time.deltaTime * 1200);
        }

        public void CharacterRespawn(float yDeathPos)
        {
            float _yPos = transform.position.y;
            if (_yPos < yDeathPos)
            {
                Respawning();
            }
        }



        private void PlayerWin()
        {
            _finishWall.SetActive(true);
            _anim.enabled = false;
            _movement.isFinish = true;
        }

        private void FinishMovement()
        {
            if (_movement.isFinish)
            {

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4f, 3f),transform.position.y,transform.position.z);
            }
        }

        public void Respawning()
        {
            transform.position = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-1f, 6f));
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        

    }
}
