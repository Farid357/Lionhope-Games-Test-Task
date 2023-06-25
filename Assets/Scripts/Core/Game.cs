using LionhopeGamesTest.Gameplay;
using UnityEngine;

namespace LionhopeGamesTest.Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private FieldFactory _fieldFactory;
        [SerializeField] private Player _player;
        
        private void Awake()
        {
            IField field = _fieldFactory.Create();
            _player.Init(field);
        }
    }
}