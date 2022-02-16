using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMapSpawner : MonoBehaviour
{
    public static FirstMapSpawner instance;
    [SerializeField]
    private GameObject _PlayerEnemyItem;

    [SerializeField]
    private GameObject _BlackEnemyItem;

    [SerializeField]
    private GameObject _WhiteEnemyItem;
    public int[,] _TypeOfitem =new int[20,20];

    float _Yposition = 0.3f;
    public bool _SecondTimeSpawner=false;
    public int _xAxis, _yAxis;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                _TypeOfitem[i, j] = Random.Range(1, 4);
            }
        }
        _ItemSpawer();
    }

    // Update is called once per frame
    void Update()
    {
        _ReSpawner();
    }

    void _ItemSpawer()
    {
        Vector3 temp;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                _xAxis = i;
                _yAxis = j;
                if (_TypeOfitem[i, j] == 1)
                {
                    temp = transform.position;
                    temp.x += i * 1.5f-14f;
                    temp.y = _Yposition;
                    temp.z += j * 1.5f-14f;
                    Instantiate(_PlayerEnemyItem, temp, Quaternion.identity);
                    _TypeOfitem[i, j] = 11;
                }
                if (_TypeOfitem[i, j] == 2)
                {
                    temp = transform.position;
                    temp.x += i * 1.5f-14f;
                    temp.y = _Yposition;
                    temp.z += j *1.5f-14f;
                    Instantiate(_BlackEnemyItem, temp, Quaternion.identity);
                    _TypeOfitem[i, j] = 22;
                }

                if (_TypeOfitem[i, j] == 3)
                {
                    temp = transform.position;
                    temp.x += i * 1.5f-14f;
                    temp.y = _Yposition;
                    temp.z += j * 1.5f-14f;
                    Instantiate(_WhiteEnemyItem, temp, Quaternion.identity);
                    _TypeOfitem[i, j] = 33;
                }
            }
        }
    }

    void _ReSpawner()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (_TypeOfitem[i, j] == 0&& _SecondTimeSpawner==true)
                {
                    _TypeOfitem[i, j] = Random.Range(1, 4);
                    _xAxis = i;
                    _yAxis = j;
                    Vector3 temp;
                    if (_TypeOfitem[i, j] == 1)
                    {
                        temp = transform.position;
                        temp.x += i * 1.5f-14f;
                        temp.y = _Yposition;
                        temp.z += j * 1.5f-14f;
                        Instantiate(_PlayerEnemyItem, temp, Quaternion.identity);
                        _TypeOfitem[i, j] = 11;
                    }
                    if (_TypeOfitem[i, j] == 2)
                    {
                        temp = transform.position;
                        temp.x += i * 1.5f-14f;
                        temp.y = _Yposition;
                        temp.z += j * 1.5f-14f;
                        Instantiate(_BlackEnemyItem, temp, Quaternion.identity);
                        _TypeOfitem[i, j] = 22;
                    }

                    if (_TypeOfitem[i, j] == 3)
                    {
                        temp = transform.position;
                        temp.x += i * 1.5f-14f;
                        temp.y = _Yposition;
                        temp.z += j * 1.5f-14f;
                        Instantiate(_WhiteEnemyItem, temp, Quaternion.identity);
                        _TypeOfitem[i, j] = 33;
                    }
                }
            }
        }
    }
}
