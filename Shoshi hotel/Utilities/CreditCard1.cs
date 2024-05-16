using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoshi_hotel.Bll;
using Shoshi_hotel.BLL;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Gui;
using Shoshi_hotel.Properties;


namespace Shoshi_hotel.Bll
{
    class CreditCard1
    {
        

        // Return true if the card number is valid
      //  מחזיר נכון אם מספר הכרטיס תקף
        public static bool isValid(long number)
        {
            return (getSize(number) >= 13 &&
                    getSize(number) <= 16) &&
                    (prefixMatched(number, 4) ||
                    prefixMatched(number, 5) ||
                    prefixMatched(number, 37) ||
                    prefixMatched(number, 6)) &&
                    ((sumOfDoubleEvenPlace(number) +
                    sumOfOddPlace(number)) % 10 == 0);
        }

        // Get the result from Step 2
      //  קבל את התוצאה משלב 2
        public static int sumOfDoubleEvenPlace(long number)
        {
            int sum = 0;
            String num = number + "";
            for (int i = getSize(number) - 2; i >= 0; i -= 2)
                sum += getDigit(int.Parse(num[i] + "") * 2);

            return sum;
        }

        // Return this number if it is a
        // single digit, otherwise, return
        // the sum of the two digits
        // החזר את המספר הזה אם הוא א
        // ספרה אחת, אחרת, החזר
        // סכום שתי הספרות
        public static int getDigit(int number)
        {
            if (number < 9)
                return number;
            return number / 10 + number % 10;
        }

        // Return sum of odd-place digits in number
        // החזר סכום של ספרות במקומות אי זוגיים במספר 
        public static int sumOfOddPlace(long number)
        {
            int sum = 0;
            String num = number + "";
            for (int i = getSize(number) - 1; i >= 0; i -= 2)
                sum += int.Parse(num[i] + "");
            return sum;
        }

        // Return true if the digit d
        // is a prefix for number
        // החזר אמת אם הספרה d
        // היא קידומת למספר
        public static bool prefixMatched(long number, int d)
        {
            return getPrefix(number, getSize(d)) == d;
        }

        // Return the number of digits in d
        // החזר את מספר הספרות ב-d
        public static int getSize(long d)
        {
            String num = d + "";
            return num.Length;
        }

        // Return the first k number of digits from
        // number. If the number of digits in number
        // is less than k, return number.
        // החזר את המספר k הראשון של הספרות מ
        // מספר. אם מספר הספרות במספר
        // קטן מ-k, מספר החזר.
        public static long getPrefix(long number, int k)
        {
            if (getSize(number) > k)
            {
                String num = number + "";
                return long.Parse(num.Substring(0, k));
            }
            return number;
        }
    }

    

}

