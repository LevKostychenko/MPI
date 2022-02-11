using MPI;
using System;

namespace MPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                var comm = Communicator.world;

                if (comm.Rank == 0)
                {
                    comm.Send("Hello", 1, 0);

                    var message = comm.Receive<string>(Communicator.anySource, 0);
                    Console.WriteLine($"Rank {comm.Rank} received message: {message}.");
                }
                else
                {
                    var message = comm.Receive<string>(comm.Rank - 1, 0);
                    Console.WriteLine($"Rank {comm.Rank} received message: {message}.");

                    comm.Send($"{message}, {comm.Rank}", (comm.Rank + 1) % comm.Size, 0);
                }
            }
        }
    }
}
