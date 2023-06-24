using LionhopeGamesTest.Gameplay;
using UnityEngine;

namespace LionhopeGamesTest.Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private FieldFactory _fieldFactory;

        private void Awake()
        {
            IField field = _fieldFactory.Create();
        }
    }
}