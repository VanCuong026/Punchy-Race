﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthDirection : MonoBehaviour
{
    public Transform _cam;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(_cam.position);
    }
}
