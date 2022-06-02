using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner
{
    public interface IWindowFactory
    {
        public void CreateNewWindow();
        public void CreateNewWindow(Tour tourAttributes);
        public void CreateLogWindow(int tourID);
    }
}
