using UnityEngine;
using UnityEngine.Tilemaps;

namespace LionhopeGamesTest.Gameplay
{
    [ExecuteInEditMode]
    public class ItemMovement : MonoBehaviour
    {
        private Tilemap _tilemap;

        private void OnEnable()
        {
            _tilemap ??= FindObjectOfType<Tilemap>();
        }

        private void Update()
        {
            if (Application.isPlaying)
                return;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            
            if (hit.collider != null)
            {
                Vector3Int cellPosition = _tilemap.WorldToCell(hit.point);

                if (_tilemap.HasTile(cellPosition))
                    transform.position = _tilemap.GetCellCenterWorld(cellPosition);
            }
        }
        
        public void Teleport(Vector2 position)
        {
            Vector3Int cellPosition = _tilemap.WorldToCell(position);

            if (_tilemap.HasTile(cellPosition))
                transform.position = _tilemap.GetCellCenterWorld(cellPosition);
        }
    }
}