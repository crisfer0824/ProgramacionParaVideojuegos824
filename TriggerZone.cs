using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone!");

            // Disable the trigger after activation
            gameObject.SetActive(false);
        }
    }
}