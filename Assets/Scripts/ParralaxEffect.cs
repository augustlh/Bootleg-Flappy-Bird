using UnityEngine;

/// <summary>
/// This simulates the parallax effect by moving the texture offset at a set speed.
/// </summary>
public class ParralaxEffect : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private float _scrollSpeed;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.mainTextureOffset += new Vector2(_scrollSpeed * Time.deltaTime, 0);
        }
    }
}