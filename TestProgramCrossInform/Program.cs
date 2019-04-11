using System;

namespace TestProgramCrossInform
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter data");

                var firstString = Console.ReadLine();
                var secondString = Console.ReadLine();
                var thirdString = Console.ReadLine();

                var maxNumber = new MaxValueNumber(firstString, secondString, thirdString);

                Console.WriteLine(maxNumber._maxNumber);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
