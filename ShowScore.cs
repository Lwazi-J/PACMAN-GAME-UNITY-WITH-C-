using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowScore : MonoBehaviour
{
    int score = 0;
    public Text winScore;
    
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("YOUR SCORE IS: ");
    }

    // Update is called once per frame
    void Update()
    {
        winScore.text = "YOUR SCORE IS: "+ score;
    }
}
