using System.Collections.Generic;
using System.Linq;
using LionhopeGamesTest.Gameplay;
using UnityEngine;

namespace LionhopeGamesTest.Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private FieldFactory _fieldFactory;
        [SerializeField] private Player _player;
        [SerializeField] private ItemsFactory _itemsFactory;
        [SerializeField] private Camera _camera;
        
        private void Awake()
        {
            IField field = _fieldFactory.Create(width: 10);
            IPlayerInput playerInput = new PlayerInput(_camera);
            IReadOnlyList<ItemData> itemsData = Resources.LoadAll("Data", typeof(ItemData)).Cast<ItemData>().ToList();
            
            _itemsFactory.Init(itemsData);
            _player.Init(field, playerInput);
        }
    }
}