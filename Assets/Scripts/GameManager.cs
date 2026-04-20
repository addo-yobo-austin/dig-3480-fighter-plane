using UnityEngine;
using TMPro;

internal enum EnemyType
{
    One,
    Two
}

public enum ClipType
{
    PowerUp,
    PowerDown,
    Detonate,
    Coin
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyOnePrefab;
    [SerializeField] private GameObject _enemyTwoPrefab;
    [SerializeField] private GameObject[] _gliderMenu;
    
    private float _score = 0;
    private bool _isGameOver;
    
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _powerUpSound,
                                       _powerDownSound,
                                       _detonateSound,
                                       _coinSound;
    
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public GameObject cloudPrefab;
    [SerializeField] private TextMeshProUGUI _livesText; 
    [SerializeField] private TMP_Text _scoreText;
    
    // Start is called before the first frame update
    private void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        
        CreateSky();
        InvokeRepeating(nameof(CreateEnemyOne), 1, 2);
        InvokeRepeating(nameof(CreateEnemyTwo), 2, Random.Range(2, 5));
        InvokeRepeating(nameof(CreatePowerup), 3, Random.Range(3, 8));
    }

    private void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            // thing you spawn in, vector position, rotation
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize,verticalScreenSize), 0), Quaternion.identity);
        }
    }

    private void CreateEnemy(EnemyType type)
    {
        GameObject toSpawn = type switch
        {
            EnemyType.One => _enemyOnePrefab,
            EnemyType.Two => _enemyTwoPrefab,
            _ => _enemyOnePrefab
        };
        Enemy enemy = Instantiate(toSpawn, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.identity).GetComponent<Enemy>();
        enemy.Init(this);
    }
    
    private void CreateEnemyOne() => CreateEnemy(EnemyType.One);
    private void CreateEnemyTwo() => CreateEnemy(EnemyType.Two);

    private void UpdateGenericTMPText(TMP_Text target, string text)
    {
        target.text = text;
    }
    
    public void ChangeLivesText(int currentLives)
    {
        UpdateGenericTMPText(_livesText, "Lives: " + currentLives);
    }

    public void UpdateScore(int pointsAdditive)
    {
        _score += pointsAdditive;
        UpdateGenericTMPText(_scoreText, "Score: " + _score);
    }
    
    private void CreatePowerup()
    {
        GameObject powerup = _gliderMenu[Random.Range(0, _gliderMenu.Length)];
        PlayerPickup pickup = Instantiate(powerup, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.identity).GetComponent<PlayerPickup>();
        pickup.Init(this);
    }
    
    public void ManagePowerupText(int powerupType)
    {
       
    }
    
    public void PlaySound(ClipType clipType)
    {
        _audioSource.PlayOneShot(clipType switch
        {
            ClipType.PowerUp => _powerUpSound,
            ClipType.PowerDown => _powerDownSound,
            ClipType.Detonate => _detonateSound,
            ClipType.Coin => _coinSound,
            _ => _powerUpSound
        });
    }
}
