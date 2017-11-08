using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderForRusselInvestments
{
    static class InterviewAnswers
    {
        // RECURSION METHOD I AM SAVING FOR LATER.
        // Write a C# function that demonstrates an understanding of recursion.  Walk us through the first three iterations.
        // This recursive method accepts an integer as a parameter and return its largest single digit. 

        // If i called GetLargestDigitInAnInteger(476)

        //   |   Iteration 1                             |    Iteration 2         |      Iteration 3        |
        //  -|-------------------------------------------|------------------------|-------------------------|-
        //   |   x = 476                                 |                        |                         |
        //   |   check if x is a negative number: no     |      x is now 47       |      x = 4              |
        //   |   check if X < 10 : no                    |      x > 10            |      x < 10             |
        //   |   remove digit 6 store in temp            |      temp = 7          |      method returns 4   |
        //   |   call method(47)                         |      call Method(4)    |                         |
        //   |   x = 7                                   |      x = 4             |                         |
        //   |   x > temp                                |      temp > x          |                         |
        //   |   return 7                                |      return temp       |                         |


        public static int GetLargestDigitInAnInteger(int x)
        {
            if (x < 0)
            {
                x = x * x;
            }

            if (x < 10)
            {
                return x;
            }
            else
            {

                int temp = x % 10;
                x = GetLargestDigitInAnInteger(x / 10);

                if (temp > x)
                {
                    return temp;
                }
                else
                {
                    return x;
                }
            }

        }
        
    }
}
