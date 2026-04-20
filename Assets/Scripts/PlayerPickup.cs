using UnityEngine;

public enum PickupType
{
    Shield,
    Coin,
    Health
}

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private PickupType _pickupType;
    private GameManager _gameManagerInstance;
    private const int k_coinValue = 1;

    /// <summary>
    /// Inherit knowledge of the gameManager from the legendary source
    /// </summary>
    /// <param name="gameManagerInstance"></param>
    public void Init(GameManager gameManagerInstance)
    {
        _gameManagerInstance = gameManagerInstance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        switch (_pickupType)
        {
            case PickupType.Shield:
                other.GetComponent<PlayerController>().AddShield();
                _gameManagerInstance.PlaySound(ClipType.PowerUp);
                break;
            case PickupType.Coin:
                _gameManagerInstance.UpdateScore(k_coinValue);
                _gameManagerInstance.PlaySound(ClipType.Coin);
                break;
            case  PickupType.Health:
                other.GetComponent<PlayerController>().Heal();
                _gameManagerInstance.PlaySound(ClipType.Heal);
                break;
        }
        
        Destroy(gameObject);
    }
}
