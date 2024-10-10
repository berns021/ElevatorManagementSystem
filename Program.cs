using ElevatorManagementSystem.Classes;

namespace ElevatorManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Building building = new Building(10, 4);
            building.Start();
        }
    }
}
