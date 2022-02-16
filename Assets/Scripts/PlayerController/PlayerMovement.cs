using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _PlayerSpeed = 3f;
    [SerializeField]
    private float _RotationSpeed = 720f;
    private Animator anim;
    Vector3 _MovementDirection;
    Quaternion _LastRotation;
    [SerializeField]
    private GameObject _CheerGuard;
    bool _FirstStep=false, _SecondStep=false;
    Vector2 _moveTouchStartPosition;
    Vector2 _moveInput;
    [SerializeField]
    private GameObject _GameOverPanel, _GameWinPanel,_TaptoRestartImage,_TaptoRestartButton,_NextLevelButton,_NextLevelImage;
    [SerializeField]
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioClip _RunningSound;
    float _RunningSoundTimeCounting=0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _MoveController();
        float _HorizontalInput = _moveInput.x;
        float _VerticalInput = _moveInput.y;
        if (_FirstStep == true)
        {
            if (transform.position.z < 30f&& PlayerController.instance._PlayerIsDead == false)
            {
                _VerticalInput = 1f;
                _HorizontalInput = 0;
            }
            else
            {
                SecondMapSpawner1.instance._Playerin2Map = true;
                _VerticalInput = 0f;
                _HorizontalInput = 0;
                _FirstStep = false;
            }
        }
        if(transform.position.z>30f)
        {
            SecondMapSpawner1.instance._Playerin2Map = true;
            SecondMapSpawner1.instance._ItemSpawer();
        }
        if (_SecondStep == true)
        {
            if (transform.position.z < 69f && PlayerController.instance._PlayerIsDead == false)
            {
                _VerticalInput = 1f;
                _HorizontalInput = 0;
            }
            else
            {
                _VerticalInput = 0f;
                _HorizontalInput = 0;
                _SecondStep = false;
            }
        }
        _MovementDirection = new Vector3(_HorizontalInput, 0, _VerticalInput);
        float inputMagnitude = Mathf.Clamp01(_MovementDirection.magnitude); //Vector3.magnitude: Tinh do dai cua Vector3. //Mathf.Clamp01: Gioi han gia tri tu 0 den 1.
        _MovementDirection.Normalize();  //Chuyen do dai Vector ve 1.
        if (PlayerController.instance._PunchingIsDone == true&& PlayerController.instance._PlayerIsDead==false&& _MovementDirection!=Vector3.zero&& (PlayerController.instance.Victory == false || (PlayerController.instance.Victory == true && transform.position.z > 65f&& transform.position.z < 68.5f)))
        {
            transform.Translate(_MovementDirection * _PlayerSpeed * inputMagnitude * Time.deltaTime, Space.World);
            Quaternion toRotation = Quaternion.LookRotation(_MovementDirection, Vector3.up);
            if (_MovementDirection != Vector3.zero)
            {
                anim.SetBool("PlayerStartRun", true);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _RotationSpeed * Time.deltaTime);
                _LastRotation = transform.rotation;
                _RunningSoundTimeCounting += Time.deltaTime;
                if (_RunningSoundTimeCounting > 0.3f) {
                    _AudioSource.PlayOneShot(_RunningSound);
                    _RunningSoundTimeCounting = 0;
                }
            }
        }
        else
        {
            anim.SetBool("PlayerStartRun", false);
            transform.rotation = _LastRotation;
        }

        if(PlayerController.instance.Victory == true&&transform.position.z<65f)
        {
            _GameOverPanel.SetActive(true);
            _TaptoRestartImage.SetActive(true);
            _TaptoRestartButton.SetActive(true);
        }
        else if(PlayerController.instance.Victory == true && transform.position.z > 66f)
        {
            _GameWinPanel.SetActive(true);
            _TaptoRestartImage.SetActive(true);
            _TaptoRestartButton.SetActive(true);
            _NextLevelImage.SetActive(true);
            _NextLevelButton.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            _MovementDirection = new Vector3(0, 0, 0);
        }

        if (other.tag == "FirstBridge")
        {
            _FirstStep = true;
        }

        if (other.tag == "SecondBridge")
        {
            _SecondStep = true;
        }

        if (other.tag == "Victory")
        {
            _CheerGuard.SetActive(true);
            PlayerController.instance.Victory = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Victory")
        {
            anim.SetBool("Victory", true);
        }
    }

    void _MoveController()
    {
        if(PlayerController.instance.Victory == false)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch _Touch = Input.GetTouch(i);
                switch (_Touch.phase)
                {
                    case TouchPhase.Began:
                        if (_Touch.position.y < Screen.height / 2) _moveTouchStartPosition = _Touch.position;
                        break;
                    case TouchPhase.Ended:
                        if (_Touch.position.y < Screen.height / 2) _moveInput = Vector2.zero;
                        break;
                    case TouchPhase.Canceled:
                    case TouchPhase.Moved:
                        if (_Touch.position.y < Screen.height / 2) _moveInput = _Touch.position - _moveTouchStartPosition;
                        break;
                }
            }
        }
    }
}
