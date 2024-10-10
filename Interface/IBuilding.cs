using ElevatorManagementSystem.Classes;


namespace ElevatorManagementSystem.Interface
{
     interface IBuilding
    {
        public int Floors { get; }
        public List<Elevator> Elevators { get; }
        public void Start();


    }
}
