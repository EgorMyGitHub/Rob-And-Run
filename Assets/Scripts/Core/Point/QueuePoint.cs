using System;
using System.Linq;
using Core.Path;
using UnityEngine;

namespace Core.Point
{
    public class QueuePath
    {
        public QueuePath(PatrolPath patrolPath)
        {
            _points = patrolPath.points.Select(i => i.position).ToArray();
        
            _currentIndex = -1;
        }
    
        private Vector3[] _points;
        private int _currentIndex;

        public Vector3 Next()
        {
            if(_points.Length <= 0)
                throw new Exception("Array of points is empty");
        
            _currentIndex++;
        
            if(_currentIndex >= _points.Length)
                _currentIndex = 0;
        
            return _points[_currentIndex];
        }
    }
}
