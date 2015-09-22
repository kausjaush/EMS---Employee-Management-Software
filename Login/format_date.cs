using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    class format_date
    {
       public static string dateformat(string day,string month,string year)
        {
           
            try
            {
                if(Convert.ToInt32(day)<=9)
                {
                    day = "0" + day;
                }
                if(Convert.ToInt32(month)<=9)
                {
                    month = "0" + month;
                }
                return (year + "-" + month + "-" + day);
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
                return ("");
            }
        }
       public static string dateformat(string month, string year)
       {

           try
           {
               if (Convert.ToInt32(month) <= 9)
               {
                   month = "0" + month;
               }
               return (year + "-" + month);
           }
           catch (Exception e1)
           {
               MessageBox.Show(e1.ToString());
               return ("");
           }
       }

        public static string previous_day(DateTimePicker dt)
       {
           try
           {
               int days;
               int year = dt.Value.Year;
               int[] month = { 31, 0, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
               int month_number = dt.Value.Month-1;
               if(month_number<1)
               {   
                   month_number = 12;
                   year = year -1;
               }

               if ((month_number)== 2)
               {
                   if ((year % 4 == 0) && (year % 100 != 0) || year % 400 == 0)
                   {
                       days = 29;
                   }
                   else
                   {
                       days = 28;
                   }
               }
               else
               {
                   days = (month[month_number - 1]);                   
               }

               string date = dateformat(days.ToString(),month_number.ToString(),year.ToString());

               return (date);

           }
            catch(Exception e1)
           {
               MessageBox.Show(e1.ToString());
               return ("");
           }
       }
    }
}
