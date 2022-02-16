using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPositionCalculator : MonoBehaviour
{
    public static TargetPositionCalculator instance;
    public bool _GetThePosition = true;
    float _StarDistance;
    public int _XNearist, _ZNearist;
    public Vector3 _NearestPoint;
    public bool _GateChooseIsDone = false, _SecondGateChooseIsDone = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (_GetThePosition==true&& _GateChooseIsDone == false&&transform.position.z<29f)
        {
            _StarDistance = 10000f;
            Vector3 _position = transform.position;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (FirstMapSpawner.instance._TypeOfitem[i, j] == 33)
                    {
                        float _XDistance = i * 1.5f - 14f - _position.x;
                        float _ZDistance = j * 1.5f - 14f - _position.z;
                        if (Mathf.Sqrt((Mathf.Pow(_XDistance, 2)) + (Mathf.Pow(_ZDistance, 2))) < _StarDistance)
                        {
                            _StarDistance = Mathf.Sqrt((Mathf.Pow(_XDistance, 2)) + (Mathf.Pow(_ZDistance, 2)));
                            _XNearist = i;
                            _ZNearist = j;
                        }
                    }
                }
            }
            _NearestPoint = new Vector3(_XNearist * 1.5f - 14f, 0, _ZNearist * 1.5f - 14f);
            if (GameObject.Find("Player") != null)
            {
                float _WEtoPlayerXDistance = transform.position.x - PlayerController.instance._PlayerXPosition;
                float _WEtoPlayerZDistance = transform.position.z - PlayerController.instance._PlayerZPosition;
                if ((Mathf.Sqrt((Mathf.Pow(_WEtoPlayerXDistance, 2)) + (Mathf.Pow(_WEtoPlayerZDistance, 2))) < 1f) && PlayerController.instance._PlayerIsDead == false && (WhiteEnemyScoreCalculator.instance._Health > PlayerScoreCalculator.instance._Health)) //Neu gan Player ma Score cao hon thi Punch Player
                {
                    _NearestPoint = new Vector3(PlayerController.instance._PlayerXPosition, PlayerController.instance._PlayerYPosition, PlayerController.instance._PlayerZPosition);
                }
            }
            if (GameObject.Find("BlackEnemy") != null)
            {
                float _WEtoBEXDistance = transform.position.x - BEController.instance._BEXPosition;
                float _WEtoBEZDistance = transform.position.z - BEController.instance._BEZPosition;
                if ((Mathf.Sqrt((Mathf.Pow(_WEtoBEXDistance, 2)) + (Mathf.Pow(_WEtoBEZDistance, 2))) < 1f) && BEController.instance._BEisDead == false && (WhiteEnemyScoreCalculator.instance._Health > BlackEnemyScoreCalculator.instance._Health)) //Neu gan Player ma Score cao hon thi Punch Player
                {
                    _NearestPoint = new Vector3(BEController.instance._BEXPosition, BEController.instance._BEYPosition, BEController.instance._BEZPosition);
                }
            }
            if (10000f - _StarDistance < 1f ||transform.position.x > 15f||transform.position.x<-15f)
            {
                _NearestPoint = new Vector3(0, 0, 0);
            }
            else if(WhiteEnemyScoreCalculator.instance._ScoreIsEnough == true && transform.position.z < 3f && _GateChooseIsDone == false)
            {
                int _ChooseTheGate = Random.Range(0, 100);
                if (_ChooseTheGate >= 50)
                {
                    _NearestPoint = new Vector3(-7f, 0, 15f);
                }
                else
                {
                    _NearestPoint = new Vector3(7f, 0, 15f);
                }
                _GateChooseIsDone = true;
            }
            FirstMapSpawner.instance._TypeOfitem[_XNearist, _ZNearist] = 4;
            _GetThePosition = false;
        }

        if(transform.position.z >= 29f&& _GetThePosition == true&& _SecondGateChooseIsDone==false)
        {
            _StarDistance = 10000f;
            Vector3 _position = transform.position;
            int _StarCounting = 0;
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    if (SecondMapSpawner1.instance._TypeOfitem[i, j] == 33)
                    {
                        _StarCounting++;
                        float _XDistance = i * 1.5f - 10f - _position.x;
                        float _ZDistance = j * 1.5f + 30f - _position.z;
                        if (Mathf.Sqrt((Mathf.Pow(_XDistance, 2)) + (Mathf.Pow(_ZDistance, 2))) < _StarDistance)
                        {
                            _StarDistance = Mathf.Sqrt((Mathf.Pow(_XDistance, 2)) + (Mathf.Pow(_ZDistance, 2)));
                            _XNearist = i;
                            _ZNearist = j;
                        }
                    }
                }
            }
            _NearestPoint = new Vector3(_XNearist * 1.5f - 10f, 0, _ZNearist * 1.5f + 30f);
            if (GameObject.Find("Player") != null)
            {
                float _WEtoPlayerXDistance = transform.position.x - PlayerController.instance._PlayerXPosition;
                float _WEtoPlayerZDistance = transform.position.z - PlayerController.instance._PlayerZPosition;
                if ((Mathf.Sqrt((Mathf.Pow(_WEtoPlayerXDistance, 2)) + (Mathf.Pow(_WEtoPlayerZDistance, 2))) < 1f) && PlayerController.instance._PlayerIsDead == false && (WhiteEnemyScoreCalculator.instance._Health > PlayerScoreCalculator.instance._Health)) //Neu gan Player ma Score cao hon thi Punch Player
                {
                    _NearestPoint = new Vector3(PlayerController.instance._PlayerXPosition, PlayerController.instance._PlayerYPosition, PlayerController.instance._PlayerZPosition);
                }
            }
            if (GameObject.Find("BlackEnemy") != null)
            {
                float _WEtoBEXDistance = transform.position.x - BEController.instance._BEXPosition;
                float _WEtoBEZDistance = transform.position.z - BEController.instance._BEZPosition;
                if ((Mathf.Sqrt((Mathf.Pow(_WEtoBEXDistance, 2)) + (Mathf.Pow(_WEtoBEZDistance, 2))) < 1f) && BEController.instance._BEisDead == false && (WhiteEnemyScoreCalculator.instance._Health > BlackEnemyScoreCalculator.instance._Health)) //Neu gan Player ma Score cao hon thi Punch Player
                {
                    _NearestPoint = new Vector3(BEController.instance._BEXPosition, BEController.instance._BEYPosition, BEController.instance._BEZPosition);
                }
            }
            if (10000f-_StarDistance<1f||transform.position.x>10f||transform.position.x<-10f)
            {
                _NearestPoint = new Vector3(0, 0, 40f);
            }else if(_SecondGateChooseIsDone == false&&WhiteEnemyScoreCalculator.instance._SecondMapScoreIsEnough == true && transform.position.z < 42f && transform.position.x > -3f && transform.position.x < 3f)
            {
                _NearestPoint = new Vector3(0, 0, 52f);
                _SecondGateChooseIsDone = true;
            }
            FirstMapSpawner.instance._TypeOfitem[_XNearist, _ZNearist] = 4;
            _GetThePosition = false;
        }
    }
}
