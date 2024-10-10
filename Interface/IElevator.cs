using System;
using System.Collections.Generic;

namespace ElevatorManagementSystem.Interface
{
    /// <summary>
    /// Defines the contract for an elevator within the elevator management system.
    /// </summary>
    public interface IElevator
    {
        /// <summary>
        /// Gets the unique identifier for the elevator.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets or sets the current floor the elevator is on.
        /// </summary>
        int CurrentFloor { get; set; }

        /// <summary>
        /// Gets or sets the total number of floors that the elevator can service.
        /// </summary>
        int Floors { get; set; }

        /// <summary>
        /// Gets or sets the list of target floors the elevator needs to visit.
        /// </summary>
        List<int> TargetFloors { get; set; }

        /// <summary>
        /// Continuously runs the elevator's operation, servicing requests and moving between floors.
        /// </summary>
        void Run();

        /// <summary>
        /// Adds a new floor request to the elevator's target floors.
        /// </summary>
        /// <param name="floor">The floor number to be added to the request list.</param>
        /// <param name="goingUp">Indicates whether the request is for upward movement.</param>
        void AddRequest(int floor, bool goingUp);

        /// <summary>
        /// Determines if the elevator is moving in the specified direction towards a given floor.
        /// </summary>
        /// <param name="goingUp">Indicates the desired direction (up or down).</param>
        /// <param name="floor">The floor number to check against the elevator's current position.</param>
        /// <returns>True if the elevator is moving in the specified direction, otherwise false.</returns>
        bool IsGoingInDirection(bool goingUp, int floor);

        /// <summary>
        /// Calculates the distance from the elevator's current floor to the specified floor.
        /// </summary>
        /// <param name="floor">The target floor to calculate the distance to.</param>
        /// <returns>The absolute distance between the current floor and the target floor.</returns>
        int DistanceTo(int floor);

        /// <summary>
        /// Moves the elevator to the specified floor.
        /// </summary>
        /// <param name="floor">The target floor the elevator should move to.</param>
        void MoveToFloor(int floor);
    }
}
