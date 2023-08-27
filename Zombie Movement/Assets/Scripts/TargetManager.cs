using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class TargetManager : Singleton<TargetManager>
    {
        public List<EnemyBehaviour> enemyTransforms = new();

        public EnemyBehaviour closestEnemy;
        private float detectionDistance = 300f;
        private Vector3 _offset;
        private float _currentDistance;
        private float _closestDistance;

        
    
        public void AddEnemy(EnemyBehaviour tr)
        {
            enemyTransforms.Add(tr);
        }

        public void RemoveEnemy(EnemyBehaviour tr)
        {
            enemyTransforms.Remove(tr);
        
        }
   


        public EnemyBehaviour FindClosestTarget(Vector3 enemyPosition)
        {
            if (enemyTransforms.Count != 0)
            {
                _closestDistance = 30f;
                foreach (var t in enemyTransforms)
                {
                    {
                        _offset = enemyPosition - t.gameObject.transform.position;
                        _currentDistance = Vector3.Magnitude(_offset);

                        if (_closestDistance > _currentDistance)
                        {
                            _closestDistance = _currentDistance;
                            closestEnemy = t;
                        }
                    }
                }

                if (_closestDistance < detectionDistance)
                {
                    return closestEnemy;
                }

                //todo: game over ! 
            
                return null;
            }

            return null;
        }

    }
}