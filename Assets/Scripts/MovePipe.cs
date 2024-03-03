using UnityEngine;

/// <summary>
/// Class that moves the pipe to the left at a set speed.
/// </summary>
public class MovePipe : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.localPosition += Vector3.left * speed * Time.deltaTime;
    }
}
