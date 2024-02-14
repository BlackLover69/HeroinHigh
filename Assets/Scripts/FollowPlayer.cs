using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // Reference na hr��e
    public GameObject area; // Reference na objekt oblasti, ve kter� se hr�� m��e pohybovat

    private Vector3 offset; // Odsazen� kamery od hr��e
    private Collider2D playerCollider; // Collider hr��e

    void Start()
    {
        // Z�skejte referenci na hr���v hern� objekt
        GameObject playerObject = GameObject.Find("Player");

        // P�idejte BoxCollider2D, pokud hr�� nem� Collider2D
        if (playerObject != null && playerObject.GetComponent<Collider2D>() == null)
        {
            playerObject.AddComponent<BoxCollider2D>();
        }

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
            Debug.LogError("Chyb� hr��!");
        }
    }

    void LateUpdate()
    {
        if (player != null && playerCollider != null)
        {
            // Nastav� pozici kamery tak, aby byla n�sledov�na hr��em s offsetem
            Vector3 targetPosition = player.transform.position + offset;

            // Z�sk� rozm�ry kamery
            float cameraHeight = 2f * Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            // Omezen� pozice hr��e tak, aby z�stal uprost�ed obrazovky
            float clampedX = Mathf.Clamp(targetPosition.x, transform.position.x - cameraWidth / 2f, transform.position.x + cameraWidth / 2f);
            float clampedY = Mathf.Clamp(targetPosition.y, transform.position.y - cameraHeight / 2f, transform.position.y + cameraHeight / 2f);
            targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);

            // Aktualizace pozice kamery
            transform.position = targetPosition;
        }
    }
}
