using UnityEngine;
using TMPro;

internal enum EnemyType
{
    One,
    Two
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyOnePrefab;
    [SerializeField] private GameObject _enemyTwoPrefab;
    [SerializeField] private GameObject[] _gliderMenu;
    
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public GameObject cloudPrefab;
    public TextMeshProUGUI livesText; 
    public GameObject gameOverMenu;
    private bool gameOver;
    public float score;
    public GameObject powerupPrefab;
    public GameObject audioPlayer;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    
    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        
        CreateSky();
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 2, Random.Range(2, 5));
        InvokeRepeating("CreatePowerup", 3, Random.Range(3, 8));
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
        Instantiate(toSpawn, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.identity);
    }
    
    private void CreateEnemyOne() => CreateEnemy(EnemyType.One);
    private void CreateEnemyTwo() => CreateEnemy(EnemyType.Two);
    
    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
    
    private void CreatePowerup()
    {
        GameObject powerup = _gliderMenu[Random.Range(0, _gliderMenu.Length)];
        // Instantiate(powerup, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-verticalScreenSize * .8f, verticalScreenSize * .8f), 0), Quaternion.identity); 
        Instantiate(powerup, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.identity);
    }
    
    public void ManagePowerupText(int powerupType)
    {
       
    }
    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
        }
    }
}
