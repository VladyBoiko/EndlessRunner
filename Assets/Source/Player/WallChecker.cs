using System;
using UnityEngine;
using VContainer;

namespace Player
{
    public class WallChecker : MonoBehaviour
    {
        [Inject]
        private PlayerStateModel _playerStateModel;
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _playerStateModel.SetIsOnWall(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _playerStateModel.SetIsOnWall(false);
        }
    }
}
