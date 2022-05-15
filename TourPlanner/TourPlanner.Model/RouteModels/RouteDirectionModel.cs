using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class RouteDirectionModel
    {
        public RouteBoundingBoxModel BoundingBox { get; set; }
        public double Distance { get; set; }
        public TimeSpan FormattedTime { get; set; }
        public string SessionId { get; set; }

    }
    public class RouteBoundingBoxModel
    {
        public PositionModel Ur { get; set; }
        public PositionModel Lr { get; set; }
    }
    public class PositionModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
