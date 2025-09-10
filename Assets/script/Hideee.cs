using UnityEngine;
using UnityEngine.Tilemaps;

public class HideTilemap : MonoBehaviour
{
    void Start()
    {
        // ≈Œ›«¡ «·‘ﬂ· ›ﬁÿ
        TilemapRenderer renderer = GetComponent<TilemapRenderer>();
        if (renderer != null)
            renderer.enabled = false;

        // «· √ﬂœ ≈‰ «· ’«œ„ ‘€«·
        TilemapCollider2D collider = GetComponent<TilemapCollider2D>();
        if (collider != null)
            collider.enabled = true;
    }
}