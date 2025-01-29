using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the texture scroll?")]
    public float scrollSpeed;

    [Header("References")]
    public MeshRenderer meshrenderer;

    void Update()
    {
        meshrenderer.material.mainTextureOffset += new Vector2(scrollSpeed * GameManager.instance.CalculateGameSpeed() / 20 * Time.deltaTime, 0);
    }
}
