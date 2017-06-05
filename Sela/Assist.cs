using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sela
{
    class Assist
    {
        public int count(LinkedList<Car> GQueue, LinkedList<Car> PQueue)
        {
            int count = 0;
            if (GQueue.Count > PQueue.Count)
                count = GQueue.Count;
            else
                count = PQueue.Count;
            return count;
        }
    }
}
