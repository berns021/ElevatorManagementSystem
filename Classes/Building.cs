using ElevatorManagementSystem.Interface;

namespace ElevatorManagementSystem.Classes
{
    /// <summary>
    /// Represents a building that manages multiple elevators.
    /// </summary>
    public class Building : IBuilding
    {
        /// <summary>
        /// Gets the total number of floors in the building.
        /// </summary>
        public int Floors { get; }

        /// <summary>
        /// Gets the list of elevators in the building.
        /// </summary>
        public List<Elevator> Elevators { get; }

        // Random number generator for creating random elevator requests.
        private Random random = new Random();

        // List to hold tasks for each elevator's operation.
        private List<Task> elevatorTasks = new List<Task>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class with a specified number of floors and elevators.
        /// </summary>
        /// <param name="floors">The total number of floors in the building.</param>
        /// <param name="elevatorCount">The number of elevators in the building.</param>
        public Building(int floors, int elevatorCount)
        {
            Floors = floors;
            Elevators = new List<Elevator>();

            // Create the specified number of elevators and add them to the list.
            for (int i = 0; i < elevatorCount; i++)
            {
                Elevators.Add(new Elevator(i + 1, floors));
            }
        }

        /// <summary>
        /// Starts the elevator management system, initiating elevator operations and generating random requests.
        /// </summary>
        public void Start()
        {
            // Start each elevator in its own task to run concurrently.
            foreach (var elevator in Elevators)
            {
                elevatorTasks.Add(Task.Run(() => elevator.Run()));
            }

            // Continuously generate random calls for elevator requests.
            while (true)
            {
                GenerateRandomCall();
                // Wait for a random interval between 1 to 15 seconds before the next call.
                Thread.Sleep(random.Next(1000, 15000)); // Random call every 1 to 15 seconds
            }
        }

        /// <summary>
        /// Generates a random elevator call by selecting a floor and direction.
        /// </summary>
        private void GenerateRandomCall()
        {
            // Randomly select a floor within the building.
            int floor = random.Next(1, Floors + 1);
            // Randomly decide the direction (up or down).
            bool goingUp = random.Next(0, 2) == 0;

            // Log the request to the console.
            Console.WriteLine($"Request: {floor} floor, going {(goingUp ? "up" : "down")}");

            // Select the appropriate elevator to service the request.
            Elevator selectedElevator = SelectElevator(floor, goingUp);
            if (selectedElevator != null)
            {
                // Add the floor request to the selected elevator's queue.
                selectedElevator.AddRequest(floor, goingUp);
            }
        }

        /// <summary>
        /// Selects the closest available elevator to service a request based on the desired floor and direction.
        /// </summary>
        /// <param name="floor">The floor number of the request.</param>
        /// <param name="goingUp">Indicates whether the request is for upward movement.</param>
        /// <returns>The selected elevator or null if no suitable elevator is available.</returns>
        public Elevator SelectElevator(int floor, bool goingUp)
        {
            // Simple selection logic: select the closest elevator that is either idle or going in the same direction.
            return Elevators.OrderBy(e => e.DistanceTo(floor))
                            .FirstOrDefault(e => e.IsIdle || e.IsGoingInDirection(goingUp, floor));
        }
    }
}
