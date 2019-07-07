using UnityEngine;

public class Closer : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - Mathf.RoundToInt(transform.position.x)) < 0.03f)
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), transform.position.y);
    }
}
