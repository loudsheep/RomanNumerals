using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RomanNumerals
{
    public partial class MainPage : ContentPage
    {
        static Dictionary<int, string> pairs = new Dictionary<int, string>()
        {
            {1000, "M" },
            {900, "CM" },
            {500, "D" },
            {400, "CD" },
            {100, "C" },
            {90, "XC" },
            {50, "L" },
            {40, "XL" },
            {10, "X" },
            {9, "IX" },
            {5, "V" },
            {4, "IV" },
            {1, "I" }
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private void UserInputEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = UserInputEntry.Text;
            if(text.Length == 0)
            {
                ResultLabel.Text = "";
                return;
            }

            Regex r = new Regex("[.,-]+");

            if (r.Matches(text).Count > 0)
            {
                ResultLabel.TextColor = new Color(1, 0, 0);
                ResultLabel.Text = "Incorrect number format";
                return;
            }

            if (!IsNumber(text))
            {
                ResultLabel.TextColor = new Color(1, 0, 0);
                ResultLabel.Text = "Incorrect number format";
                return;
            }

            int num = Int32.Parse(text);

            if(num <= 0 || num > 3999)
            {
                ResultLabel.TextColor = new Color(1, 0, 0);
                ResultLabel.Text = "Incorrect number format";
                return;
            }

            ResultLabel.TextColor = new Color(0,1,0);
            ResultLabel.Text = Convert(num);
        }

        private string Convert(int number)
        {
            string result = "";

            for(int i=0; i<pairs.Count; i++)
            {
                var pair = pairs.ElementAt(i);
                if(number >= pair.Key)
                {
                    number -= pair.Key;
                    result += pair.Value;
                    i--;
                }
            }

            return result;
        }

        private bool IsNumber(string text)
        {
            for(int i=0; i<text.Length; i++)
            {
                if (!(text[i] >= '0' && text[i] <= '9')) return false;
            }
            return true;
        }
    }
}
