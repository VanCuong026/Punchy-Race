using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Animator _anim;
    public bool _Punch = false,_PunchingIsDone=true,_PlayerIsDead=false;
    float _TimeCouting=5f;
    public float _PlayerXPosition,_PlayerZPosition, _PlayerYPosition;
    int _EnemyHealth;
    public bool Victory = false;
    [SerializeField]
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioClip _PLayerPunchSound,_BeingPunch, _BeingPunchbyGuard;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _PlayerPosition();
        _PunchOneTime();
        if (_PlayerIsDead)
        {
            _anim.SetBool("PlayerDead", true);
            this.GetComponent<SphereCollider>().enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
            _PlayerReborn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WhiteEnemy" && ((transform.position.z > -15f && transform.position.z < 15f) || (transform.position.z > 29f && transform.position.z < 51f)))
        {
            if (other.gameObject.GetComponent<WhiteEnemyScoreCalculator>()._Health > PlayerScoreCalculator.instance._Health)
            {
                _AudioSource.PlayOneShot(_BeingPunch);
                _PlayerIsDead = true;
                _TimeCouting = 0;
            }
            else if(other.gameObject.GetComponent<WhiteEnemyScoreCalculator>()._Health < PlayerScoreCalculator.instance._Health && other.gameObject.GetComponent<WEController>()._WEisDead == false)
            {
                _AudioSource.PlayOneShot(_PLayerPunchSound);
                _Punch = true;
                _PunchingIsDone = false;
            }
        }

        if (other.tag == "BlackEnemy" && ((transform.position.z > -15f && transform.position.z < 15f) || (transform.position.z > 29f && transform.position.z < 51f)))
        {
            if (other.gameObject.GetComponent<BlackEnemyScoreCalculator>()._Health > PlayerScoreCalculator.instance._Health)
            {
                _AudioSource.PlayOneShot(_BeingPunch);
                _PlayerIsDead = true;
                _TimeCouting = 0;
            }
            else if(other.gameObject.GetComponent<BlackEnemyScoreCalculator>()._Health < PlayerScoreCalculator.instance._Health)// && other.gameObject.GetComponent<BEController>()._BEisDead == false)
            {
                _AudioSource.PlayOneShot(_PLayerPunchSound);
                _Punch = true;
                _PunchingIsDone = false;
            }
        }

        if (other.tag == "GateGuard")
        {
            if (other.GetComponent<GuardController>()._Health > PlayerScoreCalculator.instance._Health)
            {
                _AudioSource.PlayOneShot(_BeingPunchbyGuard);
                _PlayerIsDead = true;
                _TimeCouting = 0;
            }
            else if (other.GetComponent<GuardController>()._Health <= PlayerScoreCalculator.instance._Health)
            {
                _AudioSource.PlayOneShot(_PLayerPunchSound);
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
            _anim.SetBool("PlayerPunch", true);
            _Punch = false;
        }

        _TimeCouting += Time.deltaTime;
        if (_TimeCouting >1f&& _PunchingIsDone == false)
        {
            _TimeCouting = 0;
            _anim.SetBool("PlayerPunch", false);
            _PunchingIsDone = true;
            PlayerScoreCalculator.instance._Health -= _EnemyHealth;
            _EnemyHealth = 0;
        }
    }

    void _PlayerPosition()
    {
        _PlayerXPosition = transform.position.x;
        _PlayerZPosition = transform.position.z;
        _PlayerYPosition = transform.position.y;
    }

    void _PlayerReborn()
    {
        _TimeCouting += Time.deltaTime;
        if (_TimeCouting > 3f)
        {
            if (transform.position.z < 30)
            {
                transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                transform.position = new Vector3(0, 0, 40f);
            }
            
            _anim.SetBool("PlayerDead", false);
            PlayerScoreCalculator.instance._Health = 1;
            PlayerScoreCalculator.instance._HealthBar = 0f;
        }
        if (_TimeCouting > 4f)
        {
            this.GetComponent<SphereCollider>().enabled = true;
            this.GetComponent<CapsuleCollider>().enabled = true;
            _PlayerIsDead = false;
        }
    }
}
