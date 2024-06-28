using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCircle : MonoBehaviour
{
    private bool isDragging = false;
    private Camera mainCamera;
    private RaycastHit2D[] hits = new RaycastHit2D[1];
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerRaycast(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -mainCamera.transform.position.z;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
    }
    private void OnPointerRaycast(Vector3 screenPosition)
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        int hitCount = Physics2D.RaycastNonAlloc(mouseWorldPosition, mainCamera.transform.forward, hits, Mathf.Infinity, LayerMask.GetMask("circle"));
        for(int i = 0; i < hitCount; i++)
        {
            if (hits[i].collider != null && hits[i].collider.gameObject == gameObject)
            {
                isDragging = true;
                break;
            }
        }
       
    }
}
