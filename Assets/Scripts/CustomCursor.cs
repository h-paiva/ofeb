using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // Ícone do cursor personalizado
    public Vector2 hotSpot = Vector2.zero; // Ponto ativo (ajuste conforme necessário)
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            Debug.LogError("Cursor Texture is not assigned!");
        }
    }
}