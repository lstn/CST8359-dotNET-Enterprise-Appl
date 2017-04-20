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
            IMPORT_WORDS: new { id_ = '1', desc = "Import words from files" },
            BUBBLE_SORT: new { id_ = '2', desc = "Bubble sort words" },
            LINQ_LAMBDA_SORT: new { id_ = '3', desc = "LINQ.Lambda sort"},
            CNT_DISTINCT: new { id_ = '4', desc = "Count the distinct words"},
            FIRST_TEN: new { id_ = '5', desc = "Take the first 10 words"},
            CNT_J_WORDS: new { id_ = '6', desc = "Get the number of words that start with 'j' and display the count"},
            DISP_CNT_D_WORDS: new { id_ = '7', desc = "Get and display of words that end with 'd' and display the count"}, 
            DISP_CNT_GT4_WORDS: new { id_ = '8', desc = "Get and display of words that are greater than 4 characters long, and display the count"},
            DISP_CNT_LT3_A_WORDS: new { id_ = '9', desc = "Get and display of words that are less than 3 characters long and start with the letter 'a', and display the count"},
            EXIT: new { id_ = 'x', desc = "Exit"}
        );

        private static readonly int NUM_MENU_OPTS = MENU_OPTS.Count();//((IDictionary<string, object>)MENU_OPTS).Count;
        static void Main(string[] args)
        {
            bool complete = false;
            while (!complete)
            {
                Console.WriteLine(MENU_OPTS);
                Console.WriteLine(GetMenuString());
                Console.Write("Make a selection: ");
                string input = Console.In.ReadLine();
                switch (input[0])
                {
                    case '2':
                        break;
                    default:
                        break;
                }
            }
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
                dynamic menuObj = it.Value;
                optionsArr[i++] = menuObj.id_ + " - " + menuObj.desc;
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
