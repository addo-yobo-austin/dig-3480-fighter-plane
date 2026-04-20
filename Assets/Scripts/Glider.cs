using UnityEngine;

public class Glider : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;
    public bool goingUp;
    public float speed;
    private GameManager _gameManager;

    [SerializeField] private float _enemyTwoSpeedMultiplier = 1.2f;
    [SerializeField] private float _enemyTwoWaveMagnitude = 2.0f;
    [SerializeField] private float _enemyTwoWaveFrequency = 2.0f;
    
    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
    }
    
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(goingUp ? Vector3.up : Vector3.down * (Time.deltaTime * speed * _enemyTwoSpeedMultiplier));
        if (_enemyType == EnemyType.Two)
        {
            transform.Translate(Vector3.right * (_enemyTwoWaveMagnitude * (Time.deltaTime * Mathf.Sin(Time.time * _enemyTwoWaveFrequency))));
        }
        
        if (transform.position.y < -_gameManager.verticalScreenSize * 1.25f ||
            transform.position.y > _gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(gameObject);
        }
    }
}