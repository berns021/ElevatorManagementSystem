using ElevatorManagementSystem.Interface;


namespace ElevatorManagementSystem.Classes
{
    public class Elevator : IElevator
    {
        public int Id { get;  }
        public int CurrentFloor { get;  set; }
        internal bool IsIdle => TargetFloors.Count == 0;
        public int Floors { get; set; }
        public List<int> TargetFloors { get; set; } = new List<int>();
        private object lockObj = new object();

        public Elevator(int id, int floors)
        {
            Id = id;
            Floors = floors;
            CurrentFloor = 1;
        }

        public void Run()
        {
            while (true)
            {
                lock (lockObj)
                {
                    if (TargetFloors.Count > 0)
                    {
                        int nextFloor = TargetFloors[0];
                        MoveToFloor(nextFloor);
                        TargetFloors.RemoveAt(0);
                        Thread.Sleep(10000); // Wait for passengers to enter/leave
                    }
                }

                Thread.Sleep(1000);
            }
        }

        public void AddRequest(int floor, bool goingUp)
        {
            lock (lockObj)
            {
                if (!TargetFloors.Contains(floor))
                {
                    TargetFloors.Add(floor);
                    TargetFloors = goingUp ? TargetFloors.OrderBy(f => f).ToList() : TargetFloors.OrderByDescending(f => f).ToList();
                }
            }
        }

        public bool IsGoingInDirection(bool goingUp, int floor)
        {
            return (goingUp && CurrentFloor <= floor) || (!goingUp && CurrentFloor >= floor);
        }

        public int DistanceTo(int floor)
        {
            return Math.Abs(CurrentFloor - floor);
        }

        public void MoveToFloor(int floor)
        {
            while (CurrentFloor != floor)
            {
                Thread.Sleep(10000); // 10 seconds to move one floor
                CurrentFloor += CurrentFloor < floor ? 1 : -1;
                Console.WriteLine($"Elevator {Id} is now on floor {CurrentFloor}");
            }
        }
    }
}

