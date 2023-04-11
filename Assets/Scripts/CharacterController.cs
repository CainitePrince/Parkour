using UnityEngine;

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

    private const string IS_WALKING = "IsWalking";
    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL = "Horizontal";



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
            _animator.SetTrigger("IsJumping");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _animator.SetTrigger("IsRunning");
            _isRunning = true;
        }

        if (_isRunning)
        {
            float translationX = Input.GetAxis("Horizontal");// * _runningSpeed;
            float translationZ = Input.GetAxis("Vertical");// * _runningSpeed;

            _animator.SetFloat("VelocityX", translationX);
            _animator.SetFloat("VelocityZ", translationZ);

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
                    _animator.SetFloat("Direction", -1.0f);
                }
                else if (translation > 0)
                {
                    _animator.SetFloat("Direction", 1.0f);
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
