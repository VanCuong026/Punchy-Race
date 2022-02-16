using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteEnemyScoreCalculator : MonoBehaviour
{
    public static WhiteEnemyScoreCalculator instance;
    [SerializeField]
    public Slider _HealthSlider;
    [SerializeField]
    public Text _HealthText;
    public int _Health = 1;
    float _HealthBar = 0f;
    public bool _ScoreIsEnough=false,_SecondMapScoreIsEnough=false;
    int _TargetHealth,_SecondMapTargetHealth;
    // Start is called before the first frame update
    void Start()
    {
        _TargetHealth =Random.Range(15,25);
        _SecondMapTargetHealth = Random.Range(10, 20);
        instance = this;
        _HealthText.text = "" + _Health;
        _HealthSlider.maxValue = 1f;
        _HealthSlider.value = _HealthBar;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Health >= _TargetHealth) _ScoreIsEnough = true;
        else _ScoreIsEnough = false;
        if (transform.position.z > 29 && _Health >= _SecondMapTargetHealth) _SecondMapScoreIsEnough = true;
        else _SecondMapScoreIsEnough = false;
        _HealthText.text = "" + _Health;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlueStar")
        {
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
                while (_Counting < 1)
                {
                    int _X = Random.Range(0, 14), _Z = Random.Range(0, 14);
                    if (SecondMapSpawner1.instance._TypeOfitem[_X, _Z] == 2 || SecondMapSpawner1.instance._TypeOfitem[_X, _Z] == 1)
                    {
                        SecondMapSpawner1.instance._TypeOfitem[_X, _Z] = 0;
                    }
                    _Counting++;
                }
            }
        }
    }
}
