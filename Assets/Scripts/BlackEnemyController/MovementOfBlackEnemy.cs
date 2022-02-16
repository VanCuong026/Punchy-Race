using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOfBlackEnemy : MonoBehaviour
{
    [SerializeField]
    private float _BESpeed = 3f;
    [SerializeField]
    private float _BERotationSpeed = 720f;
    [SerializeField]
    private GameObject _CheerGuard;
    private Animator anim;
    public int[,] _TypeOfitem1;
    public int[,] _TypeOfitem2 = new int[20, 20];
    Vector3 _MovementDirection;
    bool _FirstStep = false, _SecondStep = false;
    Vector3 _LastPosition;
    float _StopTimeCounting = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (BETargetPosition.instance._GetThePosition == false)
        {
            _MovementDirection.x = BETargetPosition.instance._NearestPoint.x - transform.position.x;
            _MovementDirection.z = BETargetPosition.instance._NearestPoint.z - transform.position.z;
            if (transform.position.z > 30f)
            {
                SecondMapSpawner1.instance._BEin2Map = true;
                SecondMapSpawner1.instance._ItemSpawer();
            }
            if (_FirstStep) //Neu vuot qua z=15.5 thi tiep tuc tien den z=30
            {
                if (transform.position.z < 31f && BEController.instance._BEisDead == false)
                {
                    BETargetPosition.instance._NearestPoint = new Vector3(BETargetPosition.instance._NearestPoint.x, 0, 31f);
                    _MovementDirection.x = BETargetPosition.instance._NearestPoint.x - transform.position.x;
                    _MovementDirection.z = BETargetPosition.instance._NearestPoint.z - transform.position.z;
                }
                else
                {
                    _MovementDirection = Vector3.zero;
                    _FirstStep = false;
                }
            }
            if (_SecondStep)
            {
                if (transform.position.z < 69f && BEController.instance._BEisDead == false)
                {
                    BETargetPosition.instance._NearestPoint = new Vector3(0, 0,69f);
                    _MovementDirection.x = BETargetPosition.instance._NearestPoint.x - transform.position.x;
                    _MovementDirection.z = BETargetPosition.instance._NearestPoint.z - transform.position.z;
                    //_MovementDirection.x = Mathf.Lerp(0, 0 - transform.position.x, Time.deltaTime / 100);
                    //_MovementDirection.z = Mathf.Lerp(0, 69f - transform.position.z, Time.deltaTime / 100);
                }
                else
                {
                    _MovementDirection = Vector3.zero;
                    _SecondStep = false;
                }
            }
        }
        //_DontStop();
        float inputMagnitude = Mathf.Clamp01(_MovementDirection.magnitude); //Vector3.magnitude: Tinh do dai cua Vector3. //Mathf.Clamp01: Gioi han gia tri tu 0 den 1.
        _MovementDirection.Normalize();  //Chuyen do dai Vector ve 1.
        if (BEController.instance._PunchingIsDone == true && BEController.instance._BEisDead == false && (PlayerController.instance.Victory == false || (PlayerController.instance.Victory == true && transform.position.z > 65f&&transform.position.z < 68.5f)))
        {
            transform.Translate(_MovementDirection * _BESpeed/* * inputMagnitude*/ * Time.deltaTime, Space.World);
            if (_MovementDirection != Vector3.zero)
            {
                anim.SetBool("Run", true);
                Quaternion toRotation = Quaternion.LookRotation(_MovementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _BERotationSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }else anim.SetBool("Run", false);
        if ((Mathf.Abs(BETargetPosition.instance._NearestPoint.x - transform.position.x) < 0.2f) && (Mathf.Abs(BETargetPosition.instance._NearestPoint.z - transform.position.z) < 0.2f))
        {
            BETargetPosition.instance._GetThePosition = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FirstBridge"&& BlackEnemyScoreCalculator.instance._ScoreIsEnough==true)
        {
            _FirstStep = true;
        }
        if (other.tag == "Wall")
        {
            _MovementDirection = new Vector3(0, 0, 0);
        }
        if (other.tag == "SecondBridge" && BlackEnemyScoreCalculator.instance._SecondMapScoreIsEnough == true)
        {
            _SecondStep = true;
        }
        if (other.tag == "Victory")
        {
            _CheerGuard.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Victory")
        {
            anim.SetBool("Victory", true);
            PlayerController.instance.Victory = true;
        }
    }

    void _DontStop()
    {
        if (_LastPosition == transform.position)
        {
            _StopTimeCounting += Time.deltaTime;
        }
        else _StopTimeCounting = 0;
        if (_StopTimeCounting > 2f || (transform.position.z > 30f && transform.position.x > 10f) || (transform.position.z > 30f && transform.position.x < -10f) || (transform.position.x > 15f) || (transform.position.x < -15f))
        {
            if (transform.position.z > 30f)
            {
                BETargetPosition.instance._NearestPoint = new Vector3(0, 0, 50f);
            }
            else
            {
                int _ChooseTheGate = Random.Range(0, 100);
                if (_ChooseTheGate >= 50)
                {
                    BETargetPosition.instance._NearestPoint = new Vector3(-7f, 0, 14f);
                }
                else
                {
                    BETargetPosition.instance._NearestPoint = new Vector3(7f, 0, 14f);
                }
            }
            _MovementDirection.x = Mathf.Lerp(0, BETargetPosition.instance._NearestPoint.x - transform.position.x, Time.deltaTime / 100);
            _MovementDirection.z = Mathf.Lerp(0, BETargetPosition.instance._NearestPoint.z - transform.position.z, Time.deltaTime / 100);
        }
        _LastPosition = transform.position;
    }
}
