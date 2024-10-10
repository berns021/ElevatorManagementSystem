using ElevatorManagementSystem.Interface;

namespace ElevatorManagementSystem.Classes
{
    /// <summary>
    /// Represents an elevator in the elevator management system.
    /// </summary>
    public class Elevator : IElevator
    {
        /// <summary>
        /// Gets the unique identifier for the elevator.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the current floor the elevator is on.
        /// </summary>
        public int CurrentFloor { get; set; }

        /// <summary>
        /// Indicates whether the elevator is idle (i.e., has no pending requests).
        /// </summary>
        internal bool IsIdle => TargetFloors.Count == 0;

        /// <summary>
        /// Gets or sets the total number of floors that the elevator can service.
        /// </summary>
        public int Floors { get; set; }

        /// <summary>
        /// Gets or sets the list of target floors the elevator needs to visit.
        /// </summary>
        public List<int> TargetFloors { get; set; } = new List<int>();

        // Lock object to ensure thread safety when accessing shared resources.
        private object lockObj = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Elevator"/> class with a specified ID and total floors.
        /// </summary>
        /// <param name="id">The unique identifier for the elevator.</param>
        /// <param name="floors">The total number of floors that the elevator can service.</param>
        public Elevator(int id, int floors)
        {
            Id = id;
            Floors = floors;
            CurrentFloor = 1; // Start on the ground floor.
        }

        /// <summary>
        /// Continuously runs the elevator's operation, servicing requests and moving between floors.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                // Lock the elevator's target floors to ensure thread safety.
                lock (lockObj)
                {
                    // Check if there are any target floors to visit.
                    if (TargetFloors.Count > 0)
                    {
                        int nextFloor = TargetFloors[0]; // Get the next target floor.
                        MoveToFloor(nextFloor); // Move to the next floor.
                        TargetFloors.RemoveAt(0); // Remove the completed request from the list.
                        Thread.Sleep(10000); // Wait for passengers to enter/leave.
                    }
                }

                // Brief sleep to reduce CPU usage when idle.
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Adds a new floor request to the elevator's target floors.
        /// </summary>
        /// <param name="floor">The floor number to be added to the request list.</param>
        /// <param name="goingUp">Indicates whether the request is for upward movement.</param>
        public void AddRequest(int floor, bool goingUp)
        {
            lock (lockObj) // Lock the target floors for thread safety.
            {
                // Add the floor to the target list if it's not already present.
                if (!TargetFloors.Contains(floor))
                {
                    TargetFloors.Add(floor);
                    // Sort the target floors based on the requested direction.
                    TargetFloors = goingUp ? TargetFloors.OrderBy(f => f).ToList() : TargetFloors.OrderByDescending(f => f).ToList();
                }
            }
        }

        /// <summary>
        /// Determines if the elevator is moving in the specified direction towards a given floor.
        /// </summary>
        /// <param name="goingUp">Indicates the desired direction (up or down).</param>
        /// <param name="floor">The floor number to check against the elevator's current position.</param>
        /// <returns>True if the elevator is moving in the specified direction, otherwise false.</returns>
        public bool IsGoingInDirection(bool goingUp, int floor)
        {
            // Check if the elevator is moving towards the requested floor based on the desired direction.
            return (goingUp && CurrentFloor <= floor) || (!goingUp && CurrentFloor >= floor);
        }

        /// <summary>
        /// Calculates the distance from the elevator's current floor to the specified floor.
        /// </summary>
        /// <param name="floor">The target floor to calculate the distance to.</param>
        /// <returns>The absolute distance between the current floor and the target floor.</returns>
        public int DistanceTo(int floor)
        {
            // Return the absolute difference between the current floor and the target floor.
            return Math.Abs(CurrentFloor - floor);
        }

        /// <summary>
        /// Moves the elevator to the specified floor.
        /// </summary>
        /// <param name="floor">The target floor the elevator should move to.</param>
        public void MoveToFloor(int floor)
        {
            // Continuously move the elevator until it reaches the target floor.
            while (CurrentFloor != floor)
            {
                Thread.Sleep(10000); // Simulate time taken to move one floor (10 seconds).
                // Update the current floor based on the direction of movement.
                CurrentFloor += CurrentFloor < floor ? 1 : -1;
                // Log the elevator's current position.
                Console.WriteLine($"Elevator {Id} is now on floor {CurrentFloor}");
            }
        }
    }
}
