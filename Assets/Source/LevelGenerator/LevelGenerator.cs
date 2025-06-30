using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Level
{
    public class LevelGenerator : ITickable
    {
        private readonly LevelConfig _levelConfig;
        private readonly Transform _playerTransform;
        private readonly GameObject[] _levelPrefabs;

        private GameObject _lastPlatform;
        private List<GameObject> _platforms = new();

        public LevelGenerator(LevelConfig levelConfig, Transform playerTransform, GameObject[] levelPrefabs)
        {
            _levelConfig = levelConfig;
            _playerTransform = playerTransform;
            _levelPrefabs = levelPrefabs;

            SpawnInitialPlatform();
        }

        public void Tick()
        {
            TrySpawnPlatform();
            TryDespawnPlatform();
        }

        private void TrySpawnPlatform()
        {
            if (!_lastPlatform.TryGetComponent(out PlatformsHolder holder)) return;

            float distance = holder.EndPoint.position.x - _playerTransform.position.x;

            if (distance > _levelConfig.DistanceToSpawn) return;
            
            SpawnPlatform(holder);
        }

        private void SpawnPlatform(PlatformsHolder holder)
        {
            var prefab = GetRandomPlatform();
            var newPlatform = Object.Instantiate(prefab, holder.EndPoint.position, Quaternion.identity);
                
            if (!newPlatform.TryGetComponent(out PlatformsHolder newHolder))
            {
                Object.Destroy(newPlatform);
                return;
            }

            Vector3 offset = newHolder.StartPoint.position - newPlatform.transform.position;
            newPlatform.transform.position -= offset;

            _platforms.Add(newPlatform);
            _lastPlatform = newPlatform;
        }
        
        private void TryDespawnPlatform()
        {
            if (_platforms.Count == 0) return;
            
            GameObject firstPlatform = _platforms[0];

            if (!firstPlatform.TryGetComponent(out PlatformsHolder holder)) return;
            
            float distance = _playerTransform.position.x - holder.EndPoint.position.x;
            if (distance < _levelConfig.DistanceToDespawn) return;
            
            _platforms.Remove(firstPlatform);
            Object.Destroy(firstPlatform);
        }
        
        private void SpawnInitialPlatform()
        {
            var prefab = GetRandomPlatform();
            var instance = Object.Instantiate(prefab);
            
            _platforms.Add(instance);
            _lastPlatform = instance;
        }
        
        private GameObject GetRandomPlatform()
        {
            return _levelPrefabs[Random.Range(0, _levelPrefabs.Length)];
        }
    }
}