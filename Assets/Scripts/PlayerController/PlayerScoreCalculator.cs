using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreCalculator : MonoBehaviour
{
    public static PlayerScoreCalculator instance;
    [SerializeField]
    private Slider _HealthSlider;
    [SerializeField]
    private Text _HealthText;
    public int _Health = 1;
    public float _HealthBar = 0f;
    [SerializeField]
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioClip _GetItemSound;
    float _TimeCounting = 0;
    bool _AudioPlayOneTime = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _HealthText.text = "" + _Health;
        _HealthSlider.maxValue = 1f;
        _HealthSlider.value = _HealthBar;
    }

    // Update is called once per frame
    void Update()
    {
        if (_AudioPlayOneTime)
        {
            _AudioPlay();
        }
        _HealthText.text = "" + _Health;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GreenStar"&&Time.timeScale!=0)
        {
            _AudioPlayOneTime = true;
            _HealthBar += 0.1f;
            if (_HealthBar > 1.1f)
            {
                _HealthBar = 0;
                _Health++;
                _HealthText.text = "" + _Health;
            }
            _HealthSlider.value = _HealthBar;
            if (transform.position.z > 30f)
            {
                int _Counting = 0;
                while (_Counting <1)
                {
                    int _X = Random.Range(0, 14), _Z = Random.Range(0, 14);
                    if (SecondMapSpawner1.instance._TypeOfitem[_X, _Z] == 2|| SecondMapSpawner1.instance._TypeOfitem[_X, _Z] == 3)
                    {
                        SecondMapSpawner1.instance._TypeOfitem[_X, _Z] = 0;
                    }
                    _Counting++;
                }
            }
        }
    }

    void _AudioPlay()
    {
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > 0.1f)
        {
            _TimeCounting = 0;
            _AudioPlayOneTime = false;
            _AudioSource.PlayOneShot(_GetItemSound);
        }
        
    }
}
