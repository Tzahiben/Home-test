using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Sela
{

    class Registration
    {
        LinkedList<Car> GQueue = new LinkedList<Car>(); //queues will be implemented using linked lists
        LinkedList<Car> PQueue = new LinkedList<Car>();

        private void ExecuteInForeground() //this function listens in the background, witing for a key to be pressed to show queues
        {
            string a = Console.ReadLine();
            if (a == "P")
            {
                Console.WriteLine("\nGeneral fix queue (by car licensee number): ");
                foreach (Car car in GQueue)
                {
                    Console.WriteLine(car.licenseeNum);
                }
                Console.WriteLine("\nPainting queue (by car licensee number): ");
                foreach (Car car in PQueue)
                {
                    Console.WriteLine(car.licenseeNum);
                }
            }
            ExecuteInForeground();
        }

        public void CarRegistration() //cars registration progress
        {
            Assist assFun = new Assist();
            Painting paint = new Painting(); //initiallize Painting class
            GeneralRepairs genRep = new GeneralRepairs();  //initiallize GeneralRepair class
            Console.WriteLine("Car registration");

            while (true)
            {
                Car car = new Car(); //initiallize Car class, create car object

                Console.WriteLine("Enter car licensee number (type 'D' when done)");
                car.licenseeNum = Console.ReadLine();
                if (car.licenseeNum == "D")
                    break; //proceed to queues

                car.regTime = DateTime.Now.ToString("h:mm:ss tt");

                Console.WriteLine("Please choose treatment");
                Console.WriteLine("G - General fix");
                Console.WriteLine("P - Painting");
                Console.WriteLine("B - Both"); //to enter both queues
                car.treatmentType = Console.ReadLine();

                if (car.treatmentType == "G" || car.treatmentType == "B") //car with b(both treatments) will be added to both queues
                {
                    GQueue.AddLast(car);
                }

                if (car.treatmentType == "P" || car.treatmentType == "B")
                {
                    PQueue.AddLast(car);
                }
            }

            //determin how many time the for loop will run
            int count = 0;
            count = assFun.count(GQueue, PQueue);
            
            var th = new Thread(ExecuteInForeground); //start the listener
            th.IsBackground = true;
            th.Start();

            //GQueue = General fix queue
            //PQueue = Painting queue

            for (int i = 0; i < count; i++)
            {
                if (GQueue.Count > 0)
                {
                    var firstCarInGQueue = GQueue.First.Value; //set the first item in General fix queue

                    if (PQueue.Find(firstCarInGQueue) != null) //taking care of first item in GQueue (Searching PQueue for the first item that is in GQueue in order to handle items on both queues). If the item exist on PQqueue, enter the function
                    {
                        genRep.RepairsQueue(firstCarInGQueue); //Sending first item of GQueue to General Fix
                        var carB = PQueue.Find(firstCarInGQueue); //var carB is the exact item as the first item currently in GQueue, that is located somewhere in PQueue
                        PQueue.Remove(carB);
                        PQueue.AddLast(firstCarInGQueue); //adding first item in GQueue to tje last position of the queue
                        GQueue.Remove(firstCarInGQueue); //removing the first item in GQueue
                    }
                    else //If the item does not exist on PQueue
                    {
                        genRep.RepairsQueue(firstCarInGQueue);
                        GQueue.Remove(firstCarInGQueue);
                        firstCarInGQueue.leavingTime = DateTime.Now.ToString("h:mm:ss tt");

                        Console.Write("\n" + firstCarInGQueue.licenseeNum + " Thank you and goodbye");
                    }
                }

                if (PQueue.Count > 0)
                {
                    var firstCarInPQueue = PQueue.First.Value;

                    if (GQueue.Find(firstCarInPQueue) != null) //taking care of first item in PQueue...
                    {
                        paint.PaintingQueue(firstCarInPQueue);
                        var carB = GQueue.Find(firstCarInPQueue);
                        GQueue.Remove(carB);
                        GQueue.AddLast(firstCarInPQueue);
                        PQueue.Remove(firstCarInPQueue);
                    }
                    else
                    {
                        paint.PaintingQueue(firstCarInPQueue);
                        PQueue.Remove(firstCarInPQueue);
                        firstCarInPQueue.leavingTime = DateTime.Now.ToString("h:mm:ss tt");

                        Console.Write("\n" + firstCarInPQueue.licenseeNum + " Thank you and goodbye");
                    }
                }
            }
            Console.WriteLine("\n\nPress any key...");
            Console.ReadLine();
            th.Abort();
        }
    }
}
