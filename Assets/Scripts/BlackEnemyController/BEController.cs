using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEController : MonoBehaviour
{
    public static BEController instance;
    private Animator _anim;
    public bool _BEisDead = false, _Punch = false, _PunchingIsDone = true;
    float _TimeCouting;
    int _EnemyHealth;
    public float _BEXPosition, _BEZPosition, _BEYPosition;
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
        _BEposition();
        if (_BEisDead == false)
        {
            _PunchOneTime();
        }
        if (_BEisDead == true)
        {
            _Reborn();
        }
    }

    void _BEposition()
    {
        _BEXPosition = transform.position.x;
        _BEZPosition = transform.position.z;
        _BEYPosition = transform.position.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && ((transform.position.z > -15f && transform.position.z < 15f) || (transform.position.z > 29f && transform.position.z < 50f)))
        {
            if (other.GetComponent<PlayerScoreCalculator>()._Health > BlackEnemyScoreCalculator.instance._Health)
            {
                _BEisDead = true;
                _TimeCouting = 0;
            }
            else if ((other.GetComponent<PlayerScoreCalculator>()._Health < BlackEnemyScoreCalculator.instance._Health))
            {
                _Punch = true;
                _PunchingIsDone = false;
            }
        }

        else if (other.tag == "WhiteEnemy" && ((transform.position.z > -15f && transform.position.z < 15f) || (transform.position.z > 29f && transform.position.z < 51f)|| transform.position.z > 64f))
        {
            if (other.GetComponent<WhiteEnemyScoreCalculator>()._Health > BlackEnemyScoreCalculator.instance._Health)
            {
                _BEisDead = true;
                _TimeCouting = 0;
            }
            else if ((other.GetComponent<WhiteEnemyScoreCalculator>()._Health < BlackEnemyScoreCalculator.instance._Health))
            {
                _AudioSource.PlayOneShot(_EnemyPunchAnother);
                _Punch = true;
                _PunchingIsDone = false;
            }
        }

        else if (other.tag == "GateGuard")
        {
            if (other.GetComponent<GuardController>()._Health > BlackEnemyScoreCalculator.instance._Health)
            {
                _BEisDead = true;
                _TimeCouting = 0;
            }
            else if (other.GetComponent<GuardController>()._Health <= BlackEnemyScoreCalculator.instance._Health)
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
            _anim.SetBool("Punch", true);
            _Punch = false;
        }
        _TimeCouting += Time.deltaTime;
        if (_TimeCouting > 1f)
        {
            _TimeCouting = 0;
            _anim.SetBool("Punch", false);
            _PunchingIsDone = true;
            BlackEnemyScoreCalculator.instance._Health -= _EnemyHealth;
            _EnemyHealth = 0;
        }
    }

    void _Reborn()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        _TimeCouting += Time.deltaTime;
        if (_TimeCouting > 0.4f && _TimeCouting < 0.5f)
        {
            _anim.SetBool("Die", true);
            BlackEnemyScoreCalculator.instance._Health = 0;
        }
        if (_TimeCouting > 2f && _TimeCouting < 2.2f)
        {
            transform.position = new Vector3(0, -10f, 0);
            _anim.SetBool("Die", false);
        }
        if (_TimeCouting > 3f)
        {
            if (SecondMapSpawner1.instance._BEin2Map) transform.position = new Vector3(0, 0f, 40f);
            else transform.position = new Vector3(0, 0f, 0);
            BlackEnemyScoreCalculator.instance._Health = 1;
            BlackEnemyScoreCalculator.instance._HealthText.text = "" + BlackEnemyScoreCalculator.instance._Health;
            BlackEnemyScoreCalculator.instance._HealthSlider.value = 0;
            BlackEnemyScoreCalculator.instance._ScoreIsEnough = false;
            BlackEnemyScoreCalculator.instance._SecondMapScoreIsEnough = false;
            BETargetPosition.instance._GetThePosition = true;
            BETargetPosition.instance._GateChooseIsDone = false;
            BETargetPosition.instance._SecondGateChooseIsDone = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            _TimeCouting = 0;
            _BEisDead = false;
        }

    }
}
