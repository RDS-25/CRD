using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapClickMove : MonoBehaviour , IPointerClickHandler
{
    public Camera miniMapCam;
    public GameObject mainCamera;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        Vector2 cursor = new Vector2(0, 0);

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RawImage>().rectTransform,
            eventData.pressPosition, eventData.pressEventCamera, out cursor))
        {
            Texture texture = GetComponent<RawImage>().texture;
            Rect rect = GetComponent<RawImage>().rectTransform.rect;

            float coordX = Mathf.Clamp(0, (((cursor.x - rect.x) * texture.width) / rect.width), texture.width);
            float coordY = Mathf.Clamp(0, (((cursor.y - rect.y) * texture.height) / rect.height), texture.height);

            float calX = coordX / texture.width;
            float calY = coordY / texture.height;

            Vector2 clickPosition = new Vector2(calX, calY);

            MoveMainCamera(clickPosition);
        }
    }

    private void MoveMainCamera(Vector2 clickPosition)
    {
        Ray mapRay = miniMapCam.ScreenPointToRay(new Vector2(clickPosition.x * miniMapCam.pixelWidth,
            clickPosition.y * miniMapCam.pixelHeight));

        RaycastHit hit;

        if (Physics.Raycast(mapRay, out hit, Mathf.Infinity))
        {
            Vector3 newPosition = new Vector3(hit.point.x,18 , hit.point.z);
            mainCamera.transform.position = newPosition;
        }
    }
}




