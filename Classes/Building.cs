
using ElevatorManagementSystem.Interface;

namespace ElevatorManagementSystem.Classes
{
    internal class Building: IBuilding
    {
        public int Floors { get; }
        public List<Elevator> Elevators { get;  }
        private Random random = new Random();
        private List<Task> elevatorTasks = new List<Task>();

        public Building(int floors, int elevatorCount)
        {
            Floors = floors;
            Elevators = new List<Elevator>();
            for (int i = 0; i < elevatorCount; i++)
            {
                Elevators.Add(new Elevator(i + 1, floors));
            }
        }

        public void Start()
        {
            foreach (var elevator in Elevators)
            {
                elevatorTasks.Add(Task.Run(() => elevator.Run()));
            }

            while (true)
            {
                GenerateRandomCall();
                Thread.Sleep(random.Next(5000, 15000)); // Random call every 5 to 15 seconds
            }
        }

        private void GenerateRandomCall()
        {
            int floor = random.Next(1, Floors + 1);
            bool goingUp = random.Next(0, 2) == 0;

            Console.WriteLine($"Request: {floor} floor, going {(goingUp ? "up" : "down")}");

            Elevator selectedElevator = SelectElevator(floor, goingUp);
            if (selectedElevator != null)
            {
                selectedElevator.AddRequest(floor, goingUp);
            }
        }

        private Elevator SelectElevator(int floor, bool goingUp)
        {
            // Simple selection logic: select the closest elevator that is going in the same direction or is idle
            return Elevators.OrderBy(e => e.DistanceTo(floor))
                            .FirstOrDefault(e => e.IsIdle || e.IsGoingInDirection(goingUp, floor));
        }
    }
}
