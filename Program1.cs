/* A solution to the producer consumer problem described in William Stallings' Operating Systems book using no semaphores
 * this example shows the busy waiting cycle which happens if no semaphores are being used
 * 
 * By Roham Harandi Fasih

*/



namespace Producer_Consumer_No_Semaphore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumer proCun = new ProducerConsumer();

            Thread t1 = new Thread(new ThreadStart(proCun.producer));
            Thread t2 = new Thread(new ThreadStart(proCun.consumer));
            

            t1.Start();
            t2.Start();

            Thread.Sleep(5000);   //execute application for 500 seconds before terminating it

            Environment.Exit(0);
            

        }
    }


    public class ProducerConsumer
    {
        int[] buffer = new int[7];    //array acting as a buffer with 7 empty spaces
        int i; // item to be produced
        int insert = 0, extract = 0; //in and out
        Random random = new Random(); // creating a new randomizer
        public ProducerConsumer() { }

        public void producer()
        {
            Console.WriteLine("Starting producer");
            
            while (true)
            {
               
                i = random.Next(100);    //produce a new random number with the maximum value of 100

                while (((insert + 1) % buffer.Length) == extract)
                {
                    Console.WriteLine("Waiting on consumer ");
                    Console.WriteLine();
                    Thread.Sleep(2);
                }


               
                buffer[insert] = i;         //add new random 
                Console.WriteLine("Produced " + buffer[insert]);
                insert = (insert + 1) % buffer.Length;            //move the insert number by one unit
                Console.WriteLine();   //empty line

                Console.WriteLine("Elements in the buffer: " + buffer[0] + " " + buffer[1] + " " + buffer[2] + " " + buffer[3] + " " + buffer[4] 
                    + " " + buffer[5] + " " + buffer[6]);

                Console.WriteLine();
                Thread.Sleep(400);


            }
        }

        public void consumer()
        {
            Console.WriteLine("Starting Consumer");
            Thread.Sleep(2);
            while (true)
            {
               
               while (insert == extract)  //buffer is empty
                {
                   Console.WriteLine("Waiting on producer... buffer is empty");
                   Thread.Sleep(1);
                }
                Console.WriteLine("Consumed " + buffer[extract]);
                Console.WriteLine();

            
                buffer[extract] = 0;
                Console.WriteLine("Elements in the buffer: " + buffer[0] + " " + buffer[1] + " " + buffer[2] + " " + buffer[3] + " " + buffer[4]
                   + " " + buffer[5] + " " + buffer[6]);

                extract = (extract + 1) % buffer.Length;   //move the out index by one... the % is because we want to go back to the beginning of the buffer

                Thread.Sleep(500);
            }
        }
    }
}