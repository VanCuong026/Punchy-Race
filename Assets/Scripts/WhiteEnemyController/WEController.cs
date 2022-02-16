using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WEController : MonoBehaviour
{
    public static WEController instance;
    private Animator _anim;
    public bool _WEisDead=false, _Punch = false, _PunchingIsDone = true;
    float _TimeCouting;
    int _EnemyHealth;
    public float _WEXPosition, _WEZPosition, _WEYPosition;
    [SerializeField]
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioClip _EnemyPunchAnother;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _WEposition();
        if (_WEisDead == false)
        {
            _PunchOneTime();
        }
        if(_WEisDead == true)
        {
            _Reborn();
        }
    }
    void _WEposition()
    {
        _WEXPosition = transform.position.x;
        _WEZPosition = transform.position.z;
        _WEYPosition = transform.position.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && ((transform.position.z > -15f && transform.position.z < 15f) || (transform.position.z > 29f && transform.position.z < 50f)))
        {
            if (other.GetComponent<PlayerScoreCalculator>()._Health > WhiteEnemyScoreCalculator.instance._Health)
            {
                _WEisDead = true;
                _TimeCouting = 0;
            }
            else if((other.GetComponent<PlayerScoreCalculator>()._Health < WhiteEnemyScoreCalculator.instance._Health))
            {
                _Punch = true;
                _PunchingIsDone = false;
            }
        }

        else if (other.tag == "BlackEnemy"&&((transform.position.z>-15f&& transform.position.z<15f) ||(transform.position.z > 29f && transform.position.z < 51f) || transform.position.z > 64f))
        {
            if (other.GetComponent<BlackEnemyScoreCalculator>()._Health > WhiteEnemyScoreCalculator.instance._Health)
            {
                _WEisDead = true;
                _TimeCouting = 0;
            }
            else if ((other.GetComponent<BlackEnemyScoreCalculator>()._Health < WhiteEnemyScoreCalculator.instance._Health))
            {
                _AudioSource.PlayOneShot(_EnemyPunchAnother);
                _Punch = true;
                _PunchingIsDone = false;
            }
        }

        else if (other.tag == "GateGuard")
        {
            if (other.GetComponent<GuardController>()._Health > WhiteEnemyScoreCalculator.instance._Health)
            {
                _WEisDead = true;
                _TimeCouting = 0;
            }
            else if (other.GetComponent<GuardController>()._Health <= WhiteEnemyScoreCalculator.instance._Health)
            {
                _Punch = true;
                _PunchingIsDone = false;
                _EnemyHealth = other.GetComponent<GuardController>()._Health;
            }
        }
    }

    void _PunchOneTime()
    {
        if (_Punch == true)
        {
            _TimeCouting = 0;
            _anim.SetBool("WhiteEnemyPunch", true);
            _Punch = false;
        }
        _TimeCouting += Time.deltaTime;
        if (_TimeCouting > 1f)
        {
            _TimeCouting = 0;
            _anim.SetBool("WhiteEnemyPunch", false);
            _PunchingIsDone = true;
            WhiteEnemyScoreCalculator.instance._Health -= _EnemyHealth;
            _EnemyHealth = 0;
        }
    }

    void _Reborn()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        _TimeCouting +=Time.deltaTime;
        if (_TimeCouting > 0.4f&& _TimeCouting < 0.5f)
        {
            _anim.SetBool("WhiteEnemyDead", true);
            WhiteEnemyScoreCalculator.instance._Health = 0;
        }
        if (_TimeCouting > 2f&& _TimeCouting<2.2f)
        {
            transform.position = new Vector3(0, -10f, 0);
            _anim.SetBool("WhiteEnemyDead", false);
        }
        if (_TimeCouting > 3f)
        {
            if (SecondMapSpawner1.instance._WEin2Map) transform.position = new Vector3(0, 0f, 40f);
            else transform.position = new Vector3(0, 0f, 0);
            WhiteEnemyScoreCalculator.instance._Health = 1;
            WhiteEnemyScoreCalculator.instance._HealthText.text = "" + WhiteEnemyScoreCalculator.instance._Health;
            WhiteEnemyScoreCalculator.instance._HealthSlider.value = 0;
            WhiteEnemyScoreCalculator.instance._ScoreIsEnough = false;
            WhiteEnemyScoreCalculator.instance._SecondMapScoreIsEnough = false;
            TargetPositionCalculator.instance._GetThePosition = true;
            TargetPositionCalculator.instance._GateChooseIsDone = false;
            TargetPositionCalculator.instance._SecondGateChooseIsDone = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            _TimeCouting = 0;
            _WEisDead = false;
        }

    }
}
