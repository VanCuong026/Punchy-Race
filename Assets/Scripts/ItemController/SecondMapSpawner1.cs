using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMapSpawner1 : MonoBehaviour
{
    public static SecondMapSpawner1 instance;
    [SerializeField]
    private GameObject _PlayerEnemyItem;

    [SerializeField]
    private GameObject _BlackEnemyItem;

    [SerializeField]
    private GameObject _WhiteEnemyItem;
    public int[,] _TypeOfitem = new int[14, 14];
    float _Yposition = 0.3f;
    public bool _PlayerSecondTimeSpawner = false, _WESecondTimeSpawner = false, _BESecondTimeSpawner = false;
    public int _xAxis, _yAxis;
    Vector3 _Offset = new Vector3(-10f, 0, 30f);
    public bool _WEin2Map = false, _Playerin2Map = false, _BEin2Map = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                _TypeOfitem[i, j] = Random.Range(1, 4);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_WEin2Map|| _Playerin2Map|| _BEin2Map)
        {
            _ReSpawner();
        }
    }

    public void _ItemSpawer()
    {
        Vector3 temp;
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                _xAxis = i;
                _yAxis = j;
                if (_TypeOfitem[i, j] == 1)
                {
                    if (_Playerin2Map)
                    {
                        temp = transform.position + _Offset;
                        temp.x += i * 1.5f;
                        temp.y = _Yposition;
                        temp.z += j * 1.5f;
                        Instantiate(_PlayerEnemyItem, temp, Quaternion.identity);
                        _TypeOfitem[i, j] = 9;
                    }
                }
                else if (_TypeOfitem[i, j] == 2)
                {
                    if (_BEin2Map)
                    {
                        temp = transform.position + _Offset;
                        temp.x += i * 1.5f;
                        temp.y = _Yposition;
                        temp.z += j * 1.5f;
                        Instantiate(_BlackEnemyItem, temp, Quaternion.identity);
                        _TypeOfitem[i, j] = 9;
                    }
                }
                else if (_TypeOfitem[i, j] == 3)
                {
                    if (_WEin2Map)
                    {
                        temp = transform.position + _Offset;
                        temp.x += i * 1.5f;
                        temp.y = _Yposition;
                        temp.z += j * 1.5f;
                        Instantiate(_WhiteEnemyItem, temp, Quaternion.identity);
                        _TypeOfitem[i, j] = 9;
                    }
                }
            }
        }
    }
    public void _ReSpawner()
    {
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                if (_TypeOfitem[i, j] == 0)// && _SecondTimeSpawner == true)
                {
                    _TypeOfitem[i, j] = Random.Range(1, 4);
                    _xAxis = i;
                    _yAxis = j;
                    Vector3 temp;
                    if (_TypeOfitem[i, j] == 1)
                    {
                        if (_Playerin2Map)
                        {
                            temp = transform.position + _Offset;
                            temp.x += i * 1.5f;
                            temp.y = _Yposition;
                            temp.z += j * 1.5f;
                            Instantiate(_PlayerEnemyItem, temp, Quaternion.identity);
                        }
                    }
                    else if (_TypeOfitem[i, j] == 2)
                    {
                        if (_BEin2Map)
                        {
                            temp = transform.position + _Offset;
                            temp.x += i * 1.5f;
                            temp.y = _Yposition;
                            temp.z += j * 1.5f;
                            Instantiate(_BlackEnemyItem, temp, Quaternion.identity);
                        }
                    }
                    else if (_TypeOfitem[i, j] == 3)
                    {
                        if (_WEin2Map)
                        {
                            temp = transform.position + _Offset;
                            temp.x += i * 1.5f;
                            temp.y = _Yposition;
                            temp.z += j * 1.5f;
                            Instantiate(_WhiteEnemyItem, temp, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}
