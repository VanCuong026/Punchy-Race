using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _Guard;
    Vector3[] _GuardPosition = {    new Vector3(-7f, 0.05f, 17f), new Vector3(-7f, 0.05f, 19.5f), new Vector3(-7f, 0.05f, 22f), new Vector3(-7f, 0.05f,24.5f), new Vector3(-7f, 0.05f, 27f),
                                    new Vector3(7f, 0.05f, 17f), new Vector3(7f, 0.05f, 19.5f), new Vector3(7f, 0.05f, 22f), new Vector3(7f, 0.05f,24.5f), new Vector3(7f, 0.05f, 27f),
                                    new Vector3(0f, 0.05f, 52.5f), new Vector3(0f, 0.05f, 55.5f), new Vector3(0f, 0.05f, 58.5f), new Vector3(0f, 0.05f,61.5f),
                                    new Vector3(0f, 0.05f, 17f), new Vector3(0f, 0.05f, 19.5f), new Vector3(0f, 0.05f, 22f), new Vector3(0f, 0.05f,24.5f), new Vector3(0f, 0.05f, 27f)};
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 14; i++)
        {
            if (SceneManager.GetActiveScene().name == "GamePlayScene")
            {
                GameObject _gob = Instantiate(_Guard, _GuardPosition[i], Quaternion.Euler(0, -150f, 0));
                if (i == 0) _gob.GetComponent<GuardController>()._Health = 1;
                if (i == 1) _gob.GetComponent<GuardController>()._Health = 2;
                if (i == 2) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 3) _gob.GetComponent<GuardController>()._Health = 4;
                if (i == 4) _gob.GetComponent<GuardController>()._Health = 5;

                if (i == 5) _gob.GetComponent<GuardController>()._Health = 2;
                if (i == 6) _gob.GetComponent<GuardController>()._Health = 2;
                if (i == 7) _gob.GetComponent<GuardController>()._Health = 2;
                if (i == 8) _gob.GetComponent<GuardController>()._Health = 2;
                if (i == 9) _gob.GetComponent<GuardController>()._Health = 2;

                if (i == 10) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 11) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 12) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 13) _gob.GetComponent<GuardController>()._Health = 3;
            }
        }

        for (int i = 0; i < 18; i++) 
        {
            if (SceneManager.GetActiveScene().name == "LV2GamePlayScene")
            {
                GameObject _gob = Instantiate(_Guard, _GuardPosition[i], Quaternion.Euler(0, -150f, 0));
                if (i == 0) _gob.GetComponent<GuardController>()._Health = 2;
                if (i == 1) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 2) _gob.GetComponent<GuardController>()._Health = 4;
                if (i == 3) _gob.GetComponent<GuardController>()._Health = 5;
                if (i == 4) _gob.GetComponent<GuardController>()._Health = 6;

                if (i == 5) _gob.GetComponent<GuardController>()._Health = 6;
                if (i == 6) _gob.GetComponent<GuardController>()._Health = 5;
                if (i == 7) _gob.GetComponent<GuardController>()._Health = 4;
                if (i == 8) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 9) _gob.GetComponent<GuardController>()._Health = 2;

                if (i == 10) _gob.GetComponent<GuardController>()._Health = 4;
                if (i == 11) _gob.GetComponent<GuardController>()._Health = 4;
                if (i == 12) _gob.GetComponent<GuardController>()._Health = 4;
                if (i == 13) _gob.GetComponent<GuardController>()._Health = 4;

                if (i == 14) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 15) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 16) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 17) _gob.GetComponent<GuardController>()._Health = 3;
                if (i == 18) _gob.GetComponent<GuardController>()._Health = 3;
            }
        }
    }
}
