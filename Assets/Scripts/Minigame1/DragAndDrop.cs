using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // Ensure z is at the same level as the object
        return mouseWorldPos;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            transform.position = targetPos;
        }
    }
}