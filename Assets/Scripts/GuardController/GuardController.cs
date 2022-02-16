using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardController : MonoBehaviour
{
    public static GuardController instance;
    public int _Health;
    private Animator _anim;
    [SerializeField]
    private Text _Text;
    float _TimeCounting=0;
    public bool _GuardIsDead = false;
    bool _PunchOneTime=false;
    Vector3 _Position;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _anim = GetComponent<Animator>();
        _Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _Text.text = "" + _Health;
        if (_GuardIsDead == true)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            _GuardReborn();
        }

        if (_PunchOneTime)
        {
            GuardPunch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerScoreCalculator>()._Health >= _Health)
            {
                _TimeCounting = 0;
                _GuardIsDead = true;
            }
            else if (other.GetComponent<PlayerScoreCalculator>()._Health < _Health)
            {
                _TimeCounting = 0;
                _anim.SetBool("Punch", true);
                _PunchOneTime = true;
            }
        }

        else if (other.tag == "BlackEnemy")
        {
            if (other.GetComponent<BlackEnemyScoreCalculator>()._Health >= _Health)
            {
                _TimeCounting = 0;
                _GuardIsDead = true;
            }
            else if (other.GetComponent<BlackEnemyScoreCalculator>()._Health < _Health)
            {
                _TimeCounting = 0;
                _anim.SetBool("Punch", true);
                _PunchOneTime = true;
            }
        }

        else if (other.tag == "WhiteEnemy")
        {
            if (other.GetComponent<WhiteEnemyScoreCalculator>()._Health >= _Health)
            {
                _TimeCounting = 0;
                _GuardIsDead = true;
            }
            else if (other.GetComponent<WhiteEnemyScoreCalculator>()._Health < _Health)
            {
                _TimeCounting = 0;
                _anim.SetBool("Punch", true);
                _PunchOneTime = true;
            }
        }
    }

    void _GuardReborn()
    {
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > 0.3f && _TimeCounting < 2f)
        {
            _anim.SetBool("Dead", true);
            _Text.text = "";
        }
        if (_TimeCounting > 2f&& _TimeCounting<2.2f)
        {
            transform.position = new Vector3(0, -5f, 0);
            _anim.SetBool("Dead", false);
        }

        if(_TimeCounting > 7f&& PlayerController.instance.Victory == false)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            transform.position = _Position;
            _GuardIsDead = false;
        }
    }

    void GuardPunch()
    {
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > 0.2f)
        {
            _anim.SetBool("Punch", false);
        }
    }
}
