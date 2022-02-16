using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _Target;
    public GameObject _GameMenu;
    public float _SmoothSpeed=0.125f;
    public Vector3 _Offset;
    float _TimeCounting = 0;
    // Update is called once per frame
    private void Awake()
    {
        Time.timeScale = 0;
    }
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            _GameMenu.SetActive(false);
        }
        if(Time.timeScale != 0)
        {
            if (PlayerController.instance.Victory == false)
            {
                Vector3 desiredPosition = _Target.position + _Offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _SmoothSpeed);
                transform.position = smoothedPosition;
                _TimeCounting = 0;
            }
            else
            {
                _VictoryCameraMove();
            }
        }
        
    }
    void _VictoryCameraMove()
    {
        _TimeCounting+= Time.deltaTime;
        if (_TimeCounting<1.5f)
        {
            Vector3 desiredPosition = new Vector3(0f, 0f, 69f) + _Offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _SmoothSpeed/2);
            transform.position = smoothedPosition;
        }
        else
        {
            transform.LookAt(new Vector3(0, 0, 69f));
            transform.RotateAround(new Vector3(0, 0, 69f), Vector3.up, Time.deltaTime * 20f);
        }
    }
}
