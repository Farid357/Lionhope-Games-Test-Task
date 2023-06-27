using System;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public sealed class PlayerInput : IPlayerInput
    {
        private readonly Camera _camera;

        public PlayerInput(Camera camera)
        {
            _camera = camera ? camera : throw new ArgumentNullException(nameof(camera));
        }

        public Vector2 MouseWorldPosition => _camera.ScreenToWorldPoint(Input.mousePosition);
      
        public bool LeftMouseButtonDown => Input.GetMouseButtonDown(0);
        
        public bool LeftMouseButtonHeldOn => Input.GetMouseButton(0);
        
        public bool LeftMouseButtonUp => Input.GetMouseButtonUp(0);
    }
}