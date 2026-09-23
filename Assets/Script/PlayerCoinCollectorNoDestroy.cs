using UnityEngine;
public class PlayerCoinCollectorNoDestroy : MonoBehaviour
{
    [SerializeField] private int coinCount = 0;
    public int CoinCount => coinCount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            Debug.Log("Coin collected! Total: " + coinCount);
            Destroy(other.gameObject);

            // The coin is NOT destroyed in this version.
        }
    }
}