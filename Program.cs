/* A simple C# solution to the consumer and producer problem described in William Stalling's Operating Systems book using Semaphores
 * By Roham Harandi Fasih
 * 
 * 
 * 
 * The consumer in this example waits for the producer to produce a number then the number is consumed.
 * 
 * 
 * Uses .NET 7 
*/


namespace Producer_Consumer
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
           
            ProducerConsumer proCun = new ProducerConsumer();
            Thread t1 = new Thread(new ThreadStart(proCun.producer));   //create two new threads
            Thread t2 = new Thread(new ThreadStart(proCun.consumer));

            t1.Start();
            t2.Start();

            Thread.Sleep(100);  //sleep the main thread for 0.1 seconds thus executing the consumer producer application for 0.1 seconds
            Environment.Exit(0); //terminate the application

            
            
        } 
    }


    public class ProducerConsumer 
    {
        private static Semaphore? _produce;
        private static Semaphore? _consume; //Semaphores 

        int[] buffer = new int[7];    //int array representing buffer
        int insert = 0, extract = 0;  //inserting and extracting from the "buffer" array -> in and out
        int n;                        //number of elements available/produced


        public ProducerConsumer() { _produce = new Semaphore(1, 1);
                                    _consume = new Semaphore(0, 1);}              //create new instances of semaphores in constructor
        public void producer() 
        {
            int i; //number to be produced

            Random random = new Random();

            while (true) 
            {
              
                _produce.WaitOne();
                i = random.Next();
                Console.WriteLine(i + " Produced");

                Thread.BeginCriticalRegion();
                Console.WriteLine("PRODUCER CRITICAL REGION");

                buffer[insert] = i;
                Console.WriteLine(i + " Appended");
                n++;
                Console.WriteLine(n + " elements available");
                insert++;

             

                if (insert % buffer.Length == 0)           //loop back to the beginning of the array if length is exceeded
                {
                    insert = 0;
                   
                }

                Console.WriteLine("Next insertion index is " + insert);

                Console.WriteLine("End of producer critical region");
                Thread.EndCriticalRegion();    //informs the host of the system that critical region has ended
                _produce.Release();
                _consume.Release();

            }
        }
        public void consumer() 
        {
           
            while (true) 
            {
                _consume.WaitOne();
                Thread.BeginCriticalRegion();
                _produce.WaitOne();
                Console.WriteLine("CONSUMER CRITICAL REGION");

                Console.WriteLine(buffer[extract] + " consumed");
                buffer[extract] = 0;        //consume
               
                n--;
                Console.WriteLine(n + " elements now");
                extract++;

                if (extract % buffer.Length == 0)
                    extract = 0;


                Console.WriteLine("end of consumer critical region");
                Thread.EndCriticalRegion();
                _produce.Release();

            }


        }
        
    }
}