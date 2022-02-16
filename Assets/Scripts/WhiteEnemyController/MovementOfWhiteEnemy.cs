using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOfWhiteEnemy : MonoBehaviour
{
    [SerializeField]
    private float _WESpeed = 3f;
    [SerializeField]
    private float _WERotationSpeed = 720f;
    [SerializeField]
    private GameObject _CheerGuard;
    private Animator anim;
    public int[,] _TypeOfitem1;
    public int[,] _TypeOfitem2 = new int[20, 20];
    Vector3 _MovementDirection;
    bool _FirstStep = false, _SecondStep = false;
    Vector3 _LastPosition;
    float _StopTimeCounting=0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (TargetPositionCalculator.instance._GetThePosition == false)
        {
            _MovementDirection.x = TargetPositionCalculator.instance._NearestPoint.x - transform.position.x;
            _MovementDirection.z = TargetPositionCalculator.instance._NearestPoint.z - transform.position.z;
            if (transform.position.z > 30f)
            {
                SecondMapSpawner1.instance._WEin2Map = true;
                SecondMapSpawner1.instance._ItemSpawer();
            }
            if (_FirstStep) //Neu vuot qua z=15.5 thi tiep tuc tien den z=30
            {
                if (transform.position.z < 31f && WEController.instance._WEisDead == false)
                {
                    TargetPositionCalculator.instance._NearestPoint = new Vector3(TargetPositionCalculator.instance._NearestPoint.x, 0, 31f);
                    _MovementDirection.x = TargetPositionCalculator.instance._NearestPoint.x - transform.position.x;
                    _MovementDirection.z = TargetPositionCalculator.instance._NearestPoint.z - transform.position.z;
                }
                else
                {
                    _MovementDirection = Vector3.zero;
                    _FirstStep = false;
                }
            }
            if (_SecondStep)
            {
                if (transform.position.z < 69f && WEController.instance._WEisDead == false)
                {
                    TargetPositionCalculator.instance._NearestPoint = new Vector3(0, 0, 69f);
                    _MovementDirection.x = TargetPositionCalculator.instance._NearestPoint.x - transform.position.x;
                    _MovementDirection.z = TargetPositionCalculator.instance._NearestPoint.z - transform.position.z;
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
        if (WEController.instance._PunchingIsDone == true&& WEController.instance._WEisDead==false&& (PlayerController.instance.Victory == false ||(PlayerController.instance.Victory == true && transform.position.z > 65f&& transform.position.z < 68.5f)))
        {
            transform.Translate(_MovementDirection * _WESpeed/* * inputMagnitude*/ * Time.deltaTime, Space.World);
            if (_MovementDirection != Vector3.zero)
            {
                anim.SetBool("WhiteEnemyRun", true);
                Quaternion toRotation = Quaternion.LookRotation(_MovementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _WERotationSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("WhiteEnemyRun", false);
            }
        }else anim.SetBool("WhiteEnemyRun", false);
        if ((Mathf.Abs(TargetPositionCalculator.instance._NearestPoint.x - transform.position.x) < 0.2f) && Mathf.Abs(TargetPositionCalculator.instance._NearestPoint.z - transform.position.z) < 0.2f)
        {
            TargetPositionCalculator.instance._GetThePosition = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FirstBridge"&& WhiteEnemyScoreCalculator.instance._ScoreIsEnough)
        {
            _FirstStep = true;
        }
        if (other.tag == "Wall")
        {
            _MovementDirection = new Vector3(0, 0, 0);
        }
        if (other.tag == "SecondBridge" && WhiteEnemyScoreCalculator.instance._SecondMapScoreIsEnough)
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
            anim.SetBool("Victory", true) ;
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
        if (_StopTimeCounting > 2f||(transform.position.z>30f&& transform.position.x>10f)|| (transform.position.z > 30f && transform.position.x < -10f)|| (transform.position.x > 15f) || (transform.position.x < -15f))
        {
            if (transform.position.z > 30f)
            {
                TargetPositionCalculator.instance._NearestPoint = new Vector3(0, 0, 50f);
            }
            else
            {
                int _ChooseTheGate = Random.Range(0, 100);
                if (_ChooseTheGate >= 50)
                {
                    TargetPositionCalculator.instance._NearestPoint = new Vector3(-7f, 0, 14f);
                }
                else
                {
                    TargetPositionCalculator.instance._NearestPoint = new Vector3(7f, 0, 14f);
                }
                _MovementDirection.x = Mathf.Lerp(0, TargetPositionCalculator.instance._NearestPoint.x - transform.position.x, Time.deltaTime / 100);
                _MovementDirection.z = Mathf.Lerp(0, TargetPositionCalculator.instance._NearestPoint.z - transform.position.z, Time.deltaTime / 100);
            }
        }
        _LastPosition = transform.position;
    }
}
