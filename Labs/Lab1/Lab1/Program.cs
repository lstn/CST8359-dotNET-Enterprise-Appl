using Dynamitey;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        private static readonly dynamic MenuBuilder = Builder.New<ExpandoObject>();
        private static readonly ExpandoObject MENU_OPTS = MenuBuilder.Expando
        (
            IMPORT_WORDS: MenuBuilder.New<ExpandoObject>('1', "Import words from files"),
            BUBBLE_SORT: MenuBuilder.New<ExpandoObject>('2', "Bubble sort words"),
            LINQ_LAMBDA_SORT: MenuBuilder.New<ExpandoObject>('3', "LINQ.Lambda sort"),
            CNT_DISTINCT: MenuBuilder.New<ExpandoObject>('4', "Count the distinct words"),
            FIRST_TEN: MenuBuilder.New<ExpandoObject>('5', "Take the first 10 words"),
            CNT_J_WORDS: MenuBuilder.New<ExpandoObject>('6', "Get the number of words that start with 'j' and display the count"),
            DISP_CNT_D_WORDS: MenuBuilder.New<ExpandoObject>('7', "Get and display of words that end with 'd' and display the count"), 
            DISP_CNT_GT4_WORDS: MenuBuilder.New<ExpandoObject>('8', "Get and display of words that are greater than 4 characters long, and display the count"),
            DISP_CNT_LT3_A_WORDS: MenuBuilder.New<ExpandoObject>('9', "Get and display of words that are less than 3 characters long and start with the letter 'a', and display the count"),
            EXIT: MenuBuilder.New<ExpandoObject>('x', "Exist")
        );

        private static readonly int NUM_MENU_OPTS = ((IDictionary<string, object>)MENU_OPTS).Count;
        static void Main(string[] args)
        {
            Console.WriteLine(GetMenuString());
            Console.Write("Make a selection: ");
            string input = Console.In.ReadLine();
        }

        private static string GetMenuString()
        {
            return String.Join(
                Environment.NewLine,
                "Hello World! Lucas Estienne's First C# App",
                "Options",
                "---------------------",
                GetMenuOptions()
                );
        }

        private static string GetMenuOptions()
        {
            string[] optionsArr = new string[NUM_MENU_OPTS];
            int i = 0;
            
            foreach (var it in MENU_OPTS)
            {
                var dictRepr = ((IDictionary<char, string>)it.Value).First();
                var realValue = MenuBuilder.Expando(
                    k: dictRepr.Key,
                    v: dictRepr.Value
                    );
                optionsArr[i] = realValue.k + " - " + realValue.v;
                ++i;
            }
            return String.Join(
                Environment.NewLine,
                String.Join(
                    Environment.NewLine,
                    optionsArr
                    )
                );
        }
    }
}
