using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab; 
    private GameManager _gameManager;

    /// <summary>
    /// Inherit knowledge of the gameManager from the legendary source
    /// </summary>
    /// <param name="gameManager"></param>
    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    private void OnTriggerEnter2D(Collider2D whatWasHit)
    {
        print($"Hit: {whatWasHit}");

        if (whatWasHit.CompareTag("Player"))
        {
            whatWasHit.GetComponent<PlayerController>().LoseALife(); 
            DestroyFunc();
        }
        else if (whatWasHit.CompareTag("Weapons"))
        {
            Destroy(whatWasHit.gameObject);
            DestroyFunc();
        }

        return;

        void DestroyFunc()
        {
            Explosion exp = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<Explosion>();
            exp.Detonate(_gameManager);
            Destroy(gameObject);
        }
    }
}
