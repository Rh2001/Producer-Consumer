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

            Thread.Sleep(500);   //execute application for 500 seconds before terminating it
            Environment.Exit(0);

        }
    }


    public class ProducerConsumer 
    {
        int[] buffer = new int[7];    //array acting as a buffer with 7 empty spaces
        int i; // item to be produced
        int insert=0, extract=0; //in and out
        Random random = new Random(); // creating a new randomizer
        public ProducerConsumer() { }

        public void producer() 
        {
            while (true) 
            {
                i = random.Next();    //produce a new random number
                while ((insert + 1) % buffer.Length == extract)
                {
                    Console.WriteLine("Buffer is full ... sleeping");
                    Thread.Sleep(5);
                }
                buffer[insert] = i;         //add new random 
                Console.WriteLine("Produced " + buffer[insert]);
                insert = (insert + 1) % buffer.Length;    //move the insert number by one unit
                Console.WriteLine("Next index to produce is " + insert);
            }
        }

        public void consumer() 
        {
          
            while (true) 
            {
                while (insert == extract)  //buffer is empty
                {
                    Console.WriteLine("Buffer is empty! sleeping");
                    Thread.Sleep(5);
                }
                Console.WriteLine("Consumed " + buffer[extract]);
                buffer[extract] = 0;
                extract = (extract + 1) % buffer.Length;   //move the out index by one... the % is because we want to go back to the beginning of the buffer
               
             
            }
        }
    }
}