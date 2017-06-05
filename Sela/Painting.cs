using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sela
{
    class Painting
    {
        public void PaintingQueue(Car car)
        {
            car.paintStartTime = DateTime.Now.ToString("h:mm:ss tt");
            Console.Write("\nCurrent car on painting: " + car.licenseeNum);
            Thread.Sleep(10000); //painting time
            Console.Write(" ----> Done painting");
            car.paintEndTime = DateTime.Now.ToString("h:mm:ss tt");
        }
    }
}
