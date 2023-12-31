using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LionhopeGamesTest.Gameplay
{
    public class ItemMovement : MonoBehaviour
    {
        private Tilemap _tilemap;

        private void OnEnable()
        {
            _tilemap ??= FindObjectOfType<Tilemap>();
        }

        public void Teleport(Vector2 position)
        {
            Vector3Int cellPosition = _tilemap.WorldToCell(position);

            if (_tilemap.HasTile(cellPosition))
            {
                transform.DOMove(_tilemap.GetCellCenterWorld(cellPosition), 0.3f);
            }
        }
    }
}