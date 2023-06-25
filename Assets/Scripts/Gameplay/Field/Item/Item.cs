using System;
using DG.Tweening;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    [RequireComponent(typeof(ItemMovement))]
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private ItemMovement _movement;
        
        [field: SerializeField] public ItemData Data { get; private set; }
    
        public Vector2 Position => transform.position;

        private void OnEnable()
        {
            _movement ??= GetComponent<ItemMovement>();
        }

        public void Init(ItemData data)
        {
            Data = data ? data : throw new ArgumentNullException(nameof(data));
            _spriteRenderer.sprite = Data.Sprite;
            Vector3 startScale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(startScale, 0.5f);
        }

        public void Teleport(Vector2 position)
        {
            _movement.Teleport(position);
        }

        public void Disable()
        {
            transform.DOScale(Vector3.one * 0.03f, 0.5f).OnComplete(() => Destroy(gameObject));
        }
    }
}