using System;
using System.Threading;

namespace Sela
{
    class GeneralRepairs
    {
        public void RepairsQueue(Car car)
        {
            car.repairStartTime = DateTime.Now.ToString("h:mm:ss tt");
            Console.Write("\nCurrent car on fixing: " + car.licenseeNum);
            Thread.Sleep(10000); //fixing time
            Console.Write(" ----> Done fixing");
            car.repairEndTime = DateTime.Now.ToString("h:mm:ss tt");
        }
    }
}
