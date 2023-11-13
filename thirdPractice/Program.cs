using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thirdPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("введите желаемое количесвто элементов массива:   ");

            int count = Convert.ToInt16(Console.ReadLine());
            string[] array = new string[count];
            
            for (int i = 0; i < count; i++)
            {
                Console.Write("введите" + i + "й" + "элемент массива:    ");
                array[i] = Console.ReadLine(); 
            }

            int[] newArray = new int[count];

            for(int i = 0; i < count;i++)
            {
                int convertedElement;
                double convertedElementD;
                bool testInt = int.TryParse(array[i], out convertedElement);
                bool testDouble = double.TryParse(array[i],out convertedElementD);

                if (testInt && convertedElement < 0)
                {
                    newArray[i] = convertedElement;
                }

                else if(testInt && convertedElement > 0)
                {
                    int counter = 1;
                    int factorial = 1;
                    while(counter <= convertedElement)
                    {
                        factorial = factorial * counter;
                        
                    }

                    newArray[i] = factorial;
                }
                
                else if (testDouble)
                {
                    newArray[i] = (int)Math.Round(convertedElementD, 2) * 100 % 10;
                }
            }
            Console.WriteLine(string.Join("", array));
            Console.WriteLine(string.Join("", newArray));
        }
    }
}
