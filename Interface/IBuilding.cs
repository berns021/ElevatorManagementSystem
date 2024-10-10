using ElevatorManagementSystem.Classes;

namespace ElevatorManagementSystem.Interface
{
    /// <summary>
    /// Defines the contract for a building that manages elevators.
    /// </summary>
    public interface IBuilding
    {
        /// <summary>
        /// Gets the total number of floors in the building.
        /// </summary>
        int Floors { get; }

        /// <summary>
        /// Gets the list of elevators in the building.
        /// </summary>
        List<Elevator> Elevators { get; }

        /// <summary>
        /// Starts the elevator management system, initiating operations for all elevators.
        /// </summary>
        void Start();
    }
}
