using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaptoReplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _Button()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void _NextLevelButtonLV2()
    {
        Application.LoadLevel("LV2GamePlayScene");
    }
    public void _NextLevelButtonLV1()
    {
        Application.LoadLevel("GamePlayScene");
    }
}
