using Tafang.Combat;
using Tafang.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tafang.Building
{
    [RequireComponent(typeof(Collider2D))]
    public class BuildSpot : MonoBehaviour
    {
        [SerializeField] private Tower towerPrefab;
        [SerializeField] private int buildCost = 50;
        [SerializeField] private Vector3 buildOffset;
        [SerializeField] private bool disableSpotAfterBuild = true;
        [SerializeField] private bool blockWhenPointerOverUI = false;
        [SerializeField] private bool verboseLog = true;

        private bool isBuilt;

        private void OnMouseDown()
        {
            if (verboseLog)
            {
                Debug.Log("[BuildSpot] Click detected on " + gameObject.name, this);
            }

            if (isBuilt || towerPrefab == null)
            {
                if (verboseLog)
                {
                    Debug.LogWarning("[BuildSpot] Blocked: already built or towerPrefab is null.", this);
                }
                return;
            }

            if (blockWhenPointerOverUI && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                if (verboseLog)
                {
                    Debug.LogWarning("[BuildSpot] Blocked: pointer is over UI.", this);
                }
                return;
            }

            if (GameManager.Instance == null)
            {
                if (verboseLog)
                {
                    Debug.LogError("[BuildSpot] Blocked: GameManager.Instance is null.", this);
                }
                return;
            }

            if (!GameManager.Instance.SpendGold(buildCost))
            {
                if (verboseLog)
                {
                    Debug.LogWarning("[BuildSpot] Blocked: not enough gold or game over.", this);
                }
                return;
            }

            Instantiate(towerPrefab, transform.position + buildOffset, Quaternion.identity);
            if (verboseLog)
            {
                Debug.Log("[BuildSpot] Tower instantiated.", this);
            }
            isBuilt = true;

            if (disableSpotAfterBuild)
            {
                Collider2D col = GetComponent<Collider2D>();
                if (col != null)
                {
                    col.enabled = false;
                }
            }
        }
    }
}
