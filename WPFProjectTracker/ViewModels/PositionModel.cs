using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProjectTracker.ViewModels
{
    public class PositionModel
    {
        public int Id { get; set; }
        public string positionName { get; set; }
        
        public string departmentName { get; set; }
        public int departmentId { get; set; }
    }
}
