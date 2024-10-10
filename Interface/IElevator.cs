using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Interface
{
     interface IElevator
    {
        public int Id { get;  }
        public int CurrentFloor { get; set; }
        public int Floors { get; set; }
        public List<int> TargetFloors { get; set; }
        public void Run();
        public void AddRequest(int floor, bool goingUp);
        public bool IsGoingInDirection(bool goingUp, int floor);
        public int DistanceTo(int floor);
        public void MoveToFloor(int floor);
   
    }
}
