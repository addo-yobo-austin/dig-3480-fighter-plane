using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject enemyOnePrefab;
    
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public GameObject cloudPrefab;
    public TextMeshProUGUI livesText; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        
        CreateSky();
        
        InvokeRepeating("CreateEnemyOne", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            //thing you spawn in, vector position, rotation
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize,verticalScreenSize), 0), Quaternion.identity);
        }
    }
    
    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.identity);
    }
    public void ChangeLivesText (int currentLives){
        livesText.text = "lives " + currentLives;
      
    }
}
