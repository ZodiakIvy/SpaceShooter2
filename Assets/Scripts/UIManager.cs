using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private bool _plasmashotActive;

    [SerializeField]
    private float _ammo = 15;
  
    [SerializeField]
    private Image _livesDisplayImg;

    [SerializeField]
    private int _lives;
    [SerializeField]
    private int _score;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private Text _bossBattleText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private TMP_Text _ammoCount;
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private TMP_Text _newAmmoText;
    [SerializeField]
    private TMP_Text _scoreText;
    

    //Handle to text
    // Start is called before the first frame update
    void Start()
    {
        _ammoCount.text = "Bullets: " + _ammo;
        _scoreText.text = "Score: " + _score;

        _bossBattleText.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        _newAmmoText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        StartCoroutine(GameOverFlickerRoutine()); 
   
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

    public void UpdateLives(int lives)
    {
        if (lives < 0 || lives > 3)
        {
            return;
        }
        
        _livesDisplayImg.sprite = _livesSprites[lives];

        if (lives == 3)
        {
            _healthText.text = "Health Full";
        }

        if (lives == 2)
        {
            _healthText.text = " ";
        }

        if (lives == 1)
        {
            _healthText.text = "WARNING";
        }
    }
    
}
