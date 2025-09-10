using UnityEngine;
using UnityEngine.Tilemaps;

public class HideTilemap : MonoBehaviour
{
    void Start()
    {
        // ����� ����� ���
        TilemapRenderer renderer = GetComponent<TilemapRenderer>();
        if (renderer != null)
            renderer.enabled = false;

        // ������ �� ������� ����
        TilemapCollider2D collider = GetComponent<TilemapCollider2D>();
        if (collider != null)
            collider.enabled = true;
    }
}