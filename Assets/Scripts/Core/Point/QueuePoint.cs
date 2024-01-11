using System;

namespace Core.Point
{
    public struct QueuePoint
    {
        public QueuePoint(BasePoint basePoint)
        {
            _points = basePoint.points;
        
            currentIndex = -1;
        }
    
        private Point[] _points;
        private int currentIndex;

        public Point Next()
        {
            if(_points.Length <= 0)
                throw new Exception("Array of points is empty");
        
            currentIndex++;
        
            if(currentIndex >= _points.Length)
                currentIndex = 0;
        
            return _points[currentIndex];
        }
    }
}
