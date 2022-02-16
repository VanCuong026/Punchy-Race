using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    float _angle = 0;
    int _xAxis, _yAxis;
    float _TimeCouting = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (transform.position.z < 30f)
        {
            _xAxis = FirstMapSpawner.instance._xAxis;
            _yAxis = FirstMapSpawner.instance._yAxis;
            transform.rotation = new Quaternion(-90f, 0, 0, 1f);
            if (FirstMapSpawner.instance._SecondTimeSpawner == true)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                _TimeCouting = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < 30f)
        {
            _TimeCouting += Time.deltaTime;
            _angle += Time.deltaTime * 100;
            transform.rotation = Quaternion.Euler(-90f, _angle, 0);
            if (_TimeCouting > 5f)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.position.z < 30f)
        {
            if (other.tag == "Player" && gameObject.tag == "GreenStar" && gameObject.GetComponent<Renderer>().enabled == true && transform.position.z < 20f)
            {
                FirstMapSpawner.instance._TypeOfitem[_xAxis, _yAxis] = 0;
                FirstMapSpawner.instance._SecondTimeSpawner = true;
                Destroy(gameObject);
            }

            if (other.tag == "WhiteEnemy" && gameObject.tag == "BlueStar" && gameObject.GetComponent<Renderer>().enabled == true && transform.position.z < 20f)
            {
                FirstMapSpawner.instance._TypeOfitem[_xAxis, _yAxis] = 0;
                FirstMapSpawner.instance._SecondTimeSpawner = true;
                Destroy(gameObject);
            }

            if (other.tag == "BlackEnemy" && gameObject.tag == "PurpleStar" && gameObject.GetComponent<Renderer>().enabled == true && transform.position.z < 20f)
            {
                FirstMapSpawner.instance._TypeOfitem[_xAxis, _yAxis] = 0;
                FirstMapSpawner.instance._SecondTimeSpawner = true;
                Destroy(gameObject);
            }
        }
    }
}
