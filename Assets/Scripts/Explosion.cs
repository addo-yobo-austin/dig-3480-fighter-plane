using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GameManager _gameManagerInstance;
    
    public void Detonate(GameManager gameManagerInstance)
    {
        gameManagerInstance.PlaySound(ClipType.Detonate);
        Destroy(gameObject, 3.14f);
    }
}
