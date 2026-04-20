using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        // transform translate - movement without physics drection, time
        // all floats need f by it if its the number.
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 8f);
        // when the bullet is high enough, destroy it.
        // if statements check things - if are true, the code in the block works, if false, the code in the block is ignored.
        if (transform.position.y > 6.5f) // transform position 8
        {
            Destroy(gameObject);
        }
    }
}
