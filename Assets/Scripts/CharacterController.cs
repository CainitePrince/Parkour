using UnityEngine;

namespace Parkour
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 10.0f;

        [SerializeField]
        private float _runningSpeed = 20.0f;

        [SerializeField]
        private float _rotationSpeed = 100.0f;

        private Transform _transform;
        private Animator _animator;
        private bool _isRunning;

        // TODO: Create separate input controller
        private const string IS_WALKING = "IsWalking";
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        private const string IS_JUMPING = "IsJumping";
        private const string IS_RUNNING = "IsRunning";
        private const string DIRECTION = "Direction";
        private const string VELOCITY_X = "VelocityX";
        private const string VELOCITY_Z = "VelocityZ";

        private void Start()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger(IS_JUMPING);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _animator.SetTrigger(IS_RUNNING);
                _isRunning = true;
            }

            if (_isRunning)
            {
                float translationX = Input.GetAxis(HORIZONTAL);
                float translationZ = Input.GetAxis(VERTICAL);

                _animator.SetFloat(VELOCITY_X, translationX);
                _animator.SetFloat(VELOCITY_Z, translationZ);

                translationX = translationX * _runningSpeed * deltaTime;
                translationZ = translationZ * _runningSpeed * deltaTime;

                //float turnAmount = Mathf.Atan2(translationX, translationZ);
                //_transform.Rotate(0, turnAmount * _rotationSpeed * deltaTime, 0);

                _transform.Translate(translationX, 0, translationZ);
            }
            else
            {
                float translation = Input.GetAxis(VERTICAL) * _speed * deltaTime;
                float rotation = Input.GetAxis(HORIZONTAL) * _rotationSpeed * deltaTime;

                if (translation != 0.0f || rotation != 0.0f)
                {
                    _animator.SetBool(IS_WALKING, true);

                    if (translation < 0)
                    {
                        _animator.SetFloat(DIRECTION, -1.0f);
                    }
                    else if (translation > 0)
                    {
                        _animator.SetFloat(DIRECTION, 1.0f);
                    }
                }
                else
                {
                    _animator.SetBool(IS_WALKING, false);
                }

                _transform.Translate(0, 0, translation);
                _transform.Rotate(0, rotation, 0);
            }
        }
    }
}
