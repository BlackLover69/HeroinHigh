using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // Reference na hr��e
    public EdgeCollider2D edgeCollider; // EdgeCollider2D oblasti, ve kter� se hr�� m��e pohybovat

    private Vector3 offset; // Odsazen� kamery od hr��e
    private Collider2D playerCollider; // Collider hr��e

    void Start()
    {
        if (player != null)
        {
            // Vypo��t� offset kamery od hr��e
            offset = transform.position - player.transform.position;

            // Z�sk� Collider2D z hr��e
            playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider == null)
            {
                Debug.LogError("Chyb� Collider2D na hr��i!");
            }
        }
        else
        {
            Debug.LogError("Hr�� nen� p�i�azen!");
        }

        if (edgeCollider == null)
        {
            Debug.LogError("EdgeCollider nen� p�i�azen!");
        }
    }

    void LateUpdate()
    {
        if (player != null && playerCollider != null && edgeCollider != null)
        {
            // Nastav� pozici kamery tak, aby byla n�sledov�na hr��em s offsetem
            Vector3 targetPosition = player.transform.position + offset;

            // Z�sk�n� rozm�r� oblasti, ve kter� se m��e hr�� pohybovat
            float areaWidth = edgeCollider.bounds.size.x;
            float areaHeight = edgeCollider.bounds.size.y;

            // Z�sk�n� rozm�r� kamery
            float cameraHeight = Camera.main.orthographicSize * 2;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            // Omezen� pozice hr��e tak, aby z�stal ve st�edu oblasti
            float clampedX = Mathf.Clamp(targetPosition.x, edgeCollider.bounds.min.x + cameraWidth / 2, edgeCollider.bounds.max.x - cameraWidth / 2);
            float clampedY = Mathf.Clamp(targetPosition.y, edgeCollider.bounds.min.y + cameraHeight / 2, edgeCollider.bounds.max.y - cameraHeight / 2);

            // Aktualizace pozice kamery
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}

