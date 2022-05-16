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

        public RouteDirectionModel()
        {
            BoundingBox = new RouteBoundingBoxModel();
            Distance = 0;
            FormattedTime = TimeSpan.Zero;
            SessionId = "";
        }

    }
    public class RouteBoundingBoxModel
    {
        public PositionModel Ul { get; set; }
        public PositionModel Lr { get; set; }

        public RouteBoundingBoxModel()
        {
            Ul = new PositionModel();
            Lr = new PositionModel();
        }
    }
    public class PositionModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public PositionModel()
        {
            Lat = 0;
            Lng = 0;
        }
    }
}
