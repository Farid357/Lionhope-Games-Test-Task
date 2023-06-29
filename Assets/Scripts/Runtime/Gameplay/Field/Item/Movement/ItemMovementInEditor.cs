using UnityEngine;
using UnityEngine.Tilemaps;

namespace LionhopeGamesTest.Gameplay
{
#if UNITY_EDITOR

    [ExecuteInEditMode]
    public sealed class ItemMovementInEditor : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;

        private Vector3 _lastPosition;

        private void Update()
        {
            if (_tilemap == null)
            {
                Debug.LogWarning("Assign Tilemap in the inspector!");
                return;
            }

            if (_lastPosition == transform.position)
                return;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

            if (hit.collider != null)
            {
                Vector3Int cellPosition = _tilemap.WorldToCell(hit.point);

                if (_tilemap.HasTile(cellPosition))
                {
                    transform.position = _tilemap.GetCellCenterWorld(cellPosition);;
                    _lastPosition = transform.position;
                }
            }
        }
    }
#endif
}