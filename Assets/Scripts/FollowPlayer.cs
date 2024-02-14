using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // Reference na hr��e
    public GameObject area; // Reference na objekt oblasti, ve kter� se hr�� m��e pohybovat

    private Vector3 offset; // Odsazen� kamery od hr��e
    private Collider2D playerCollider; // Collider hr��e
    private Collider2D areaCollider; // Collider oblasti

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

        if (area != null)
        {
            // Z�sk� Collider2D z oblasti
            areaCollider = area.GetComponent<Collider2D>();
            if (areaCollider == null)
            {
                Debug.LogError("Chyb� Collider2D na objektu oblasti!");
            }
        }
        else
        {
            Debug.LogError("Objekt oblasti nen� p�i�azen!");
        }
    }

    void LateUpdate()
    {
        if (player != null && playerCollider != null && area != null && areaCollider != null)
        {
            // Nastav� pozici kamery tak, aby byla n�sledov�na hr��em s offsetem
            Vector3 targetPosition = player.transform.position + offset;

            // Omezen� pozice kamery tak, aby z�stala uvnit� okraj� oblasti
            Vector3 clampedPosition = new Vector3(
                Mathf.Clamp(targetPosition.x, areaCollider.bounds.min.x, areaCollider.bounds.max.x),
                Mathf.Clamp(targetPosition.y, areaCollider.bounds.min.y, areaCollider.bounds.max.y),
                transform.position.z // Zachov�v� st�vaj�c� z pozice z
            );

            // Aktualizace pozice kamery
            transform.position = clampedPosition;
        }
    }
}
