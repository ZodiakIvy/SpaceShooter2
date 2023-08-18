using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private int _score = 0;
    
    //Handle to text
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enemy1Hit()
    {
        _score = _score + 10;
    }
}
