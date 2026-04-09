using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab; 
    private GameManager _gameManager;
    
    private void OnTriggerEnter2D(Collider2D whatWasHit)
    {
        print($"Hit: {whatWasHit}");
        
        if (whatWasHit.CompareTag("Player"))
        {
            whatWasHit.GetComponent<PlayerController>().LoseALife(); 
            Instantiate(explosionPrefab, transform.position, Quaternion.identity); 
            Destroy(gameObject);
        }
        else if (whatWasHit.CompareTag("Weapons"))
        {
            Destroy(whatWasHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity); 
            // add score to the game manager if I have reference 
            Destroy(gameObject); 
        }
    }
}
