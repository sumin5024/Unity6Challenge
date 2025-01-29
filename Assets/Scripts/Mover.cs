using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1f;
    void Update()
    {
        transform.position += Vector3.left * GameManager.instance.CalculateGameSpeed() * Time.deltaTime;
    }
}
