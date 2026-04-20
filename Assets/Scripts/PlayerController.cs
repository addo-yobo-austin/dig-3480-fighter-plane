using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // how to define a variable
    // 1. access modifier: public or private
    // 2. data type: int, float, bool, string
    // 3. variable name: camelCase
    // 4. value: optional
    public int lives; 
    public GameManager gameManager; 
    public GameObject explosionPrefab; 
    private float _playerSpeed = 6f;
    private float _horizontalInput;
    private float _verticalInput;

    private Camera _mainCam;
    private float _playerHalfSize = 0.5f;

    public GameObject bulletPrefab;

    [SerializeField] private GameObject _playerShieldObj;
    private bool _hasShield;
    
    private void Start()
    {
        lives = 3; 
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        gameManager.ChangeLivesText(lives); 
        //This function is called at the start of the game
        
        _mainCam =  Camera.main;
        if (!_mainCam)
        {
            Debug.LogError("No Main Camera");
        }
    }
    
    private void Update()
    {
        // This function is called every frame; 60 frames/second
        // Framerate dependent upon performance of the game. Not necessarily 60 fps, hence Time.deltaTime normalization
        Movement();
        Shooting();
    }

    private void SetShield(bool state)
    {
        _hasShield = state;
        _playerShieldObj.SetActive(state);
    }
    
    public void AddShield()
    {
        SetShield(true);
        gameManager.PlaySound(1);
    }

    private bool CheckShield()
    {
        if (!_hasShield) return false;
        SetShield(false);
        gameManager.PlaySound(2);
        return true;
    }
    
    public void LoseALife()
    {
        if (CheckShield()) return;
        
        lives--; 
        gameManager.ChangeLivesText(lives);
        if (lives > 0) return;
        
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }

    private void Shooting()
    {
        // if the player presses the SPACE key, create a projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
    
    private void Movement()
    {
        // Read the input from the player
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        // Move the player
        transform.Translate(new Vector3(_horizontalInput, _verticalInput, 0) * (Time.deltaTime * _playerSpeed));

        // Finds the current aspect ratio and uses that as a border
        float verticalScreenSize = _mainCam.orthographicSize;
        float horizontalScreenSize = _mainCam.aspect * verticalScreenSize;
        
        // Player leaves the screen horizontally
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        // Player leaves the screen vertically
        if (transform.position.y > verticalScreenSize || transform.position.y <= -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
        // Player cannot move past the center or bottom of the screen.
        float cameraYPosition = _mainCam.transform.position.y;

        // Makes it so that it looks at the border of the cube and not the center.
        float bottomScreenLimit = cameraYPosition - verticalScreenSize + _playerHalfSize;
        float centerScreenLimit = cameraYPosition + -cameraYPosition;
        
        float yLimit = Mathf.Clamp(transform.position.y, bottomScreenLimit, centerScreenLimit);
        transform.position = new Vector3(transform.position.x, yLimit, 0);
    }
}
