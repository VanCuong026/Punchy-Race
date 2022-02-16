using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMapitemRotate : MonoBehaviour
{
    float _angle = 0;
    int _xAxis, _yAxis;
    float _TimeCouting=10f;
    bool _SetValue1Time = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (transform.position.z >= 28f)
        {
            _xAxis = SecondMapSpawner1.instance._xAxis;
            _yAxis = SecondMapSpawner1.instance._yAxis;
            transform.rotation = new Quaternion(-90f, 0, 0, 1f);
            if (SecondMapSpawner1.instance._PlayerSecondTimeSpawner == true&& gameObject.tag == "GreenStar")
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                _TimeCouting = 0f;
            }else if(SecondMapSpawner1.instance._WESecondTimeSpawner == true && gameObject.tag == "BlueStar")
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                _TimeCouting = 0f;
            }
            else if (SecondMapSpawner1.instance._BESecondTimeSpawner == true && gameObject.tag == "PurpleStar")
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                _TimeCouting = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= 28f)
        {
            _TimeCouting += Time.deltaTime;
            _angle += Time.deltaTime * 100;
            transform.rotation = Quaternion.Euler(-90f, _angle, 0);
            if (_TimeCouting > 5f && _SetValue1Time == false)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                gameObject.GetComponent<BoxCollider>().enabled = true;
                if (gameObject.tag == "BlueStar") SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] = 33;
                if (gameObject.tag == "GreenStar") SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] = 11;
                if (gameObject.tag == "PurpleStar") SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] = 22;
                _TimeCouting = 0;
                _SetValue1Time = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(transform.position.z >= 28f)
        {
            if (other.tag == "Player" && gameObject.tag == "GreenStar" && gameObject.GetComponent<Renderer>().enabled == true && transform.position.z > 20f)
            {
                if (SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] != 0) SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] = 0;
                SecondMapSpawner1.instance._PlayerSecondTimeSpawner = true;
                Destroy(gameObject);
            }

            if (other.tag == "WhiteEnemy" && gameObject.tag == "BlueStar" && gameObject.GetComponent<Renderer>().enabled == true && transform.position.z > 20f)
            {
                if (SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] != 0) SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] = 0;
                SecondMapSpawner1.instance._WESecondTimeSpawner = true;
                Destroy(gameObject);
            }

            if (other.tag == "BlackEnemy" && gameObject.tag == "PurpleStar" && gameObject.GetComponent<Renderer>().enabled == true && transform.position.z > 20f)
            {
                if (SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] != 0) SecondMapSpawner1.instance._TypeOfitem[_xAxis, _yAxis] = 0;
                SecondMapSpawner1.instance._BESecondTimeSpawner = true;
                Destroy(gameObject);
            }
        }
    }
}
