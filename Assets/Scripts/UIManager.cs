using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private int _score;
    [SerializeField]
    private TMP_Text _ammoCount;
    [SerializeField]
    private float _ammo = 15;
    [SerializeField]
    private bool _plasmashotActive;
    [SerializeField]
    private Image _livesDisplayImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private int _lives;
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private TMP_Text _newAmmoText;
   

    //Handle to text
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        _ammoCount.text = "Bullets: " + _ammo;
        _gameOverText.gameObject.SetActive(false);
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(false);
        _newAmmoText.gameObject.SetActive(false);
   
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

        if (_lives == 3)
        {
            _healthText.text = "Health Full";
        }

        if (_lives == 2)
        {
            _healthText.text = " ";
        }

        if (_lives == 1)
        {
            _healthText.text = "WARNING";
        }
    }
    public void Enemy1Hit(int _score)
    {
       _scoreText.text = "Score: " + _score.ToString();
    }

    public void ShotsFired(float _ammo)
    {
        _ammoCount.text = "Bullets: " + _ammo.ToString();

        if (_ammo <= 0) 
        {
            _ammoCount.text = "I Need More Bullets!";
        }
        else if (_ammo > 0)
        {
            _ammoCount.text = "Bullets: " + _ammo.ToString();
        }
    }
}
