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

        private const string IS_WALKING = "IsWalking";
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

            bool isJumping = InputController.IsJumping();
            bool isRunning = InputController.IsRunning();

            float translationX = InputController.GetLeftRight();
            float translationZ = InputController.GetForwardBackward();

            // TODO when this gets more complicated replace with state machine
            if (isRunning && (translationX != 0 || translationZ != 0))
            {
                ProcessRun(deltaTime, isJumping, translationX, translationZ);
            }
            else
            {
                ProcessWalk(deltaTime, isJumping, translationX, translationZ);
            }
        }

        private void ProcessRun(float deltaTime, bool isJumping, float translationX, float translationZ)
        {
            // TODO replace with running jump
            if (isJumping)
            {
                _animator.SetTrigger(IS_JUMPING);
            }

            _animator.SetBool(IS_RUNNING, true);

            _animator.SetFloat(VELOCITY_X, translationX);
            _animator.SetFloat(VELOCITY_Z, translationZ);

            translationX = translationX * _runningSpeed * deltaTime;
            translationZ = translationZ * _runningSpeed * deltaTime;

            _transform.Translate(translationX, 0, translationZ);
            float turnAmount = Mathf.Atan2(translationX, translationZ);

            if (turnAmount > 3.14f)
            {
                turnAmount = 0;
            }

            _transform.Rotate(0, turnAmount * _rotationSpeed * deltaTime, 0);
        }

        private void ProcessWalk(float deltaTime, bool isJumping, float translationX, float translationZ)
        {
            _animator.SetBool(IS_RUNNING, false);

            if (isJumping)
            {
                _animator.SetTrigger(IS_JUMPING);
            }

            float translation = translationZ * _speed * deltaTime;
            float rotation = translationX * _rotationSpeed * deltaTime;

            if (translation != 0.0f || rotation != 0.0f)
            {
                _animator.SetBool(IS_WALKING, true);

                if (translation< 0)
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
