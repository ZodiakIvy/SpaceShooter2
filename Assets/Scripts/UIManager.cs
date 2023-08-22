using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _lives;
    [SerializeField]
    private Image _livesDisplayImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    //Handle to text
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        _gameOverText.gameObject.SetActive(false);
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.15f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.15f);
        }
    }

    public void UpdateLives(int _lives) 
    { 
        _livesDisplayImg.sprite = _livesSprites[_lives]; 
    }
    public void Enemy1Hit(int _score)
    {
       _scoreText.text = "Score: " + _score.ToString();
    }
}
