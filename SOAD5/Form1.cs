using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAD5
{
    public partial class Form : System.Windows.Forms.Form
    {
        private int n = 1000000;
        private long[] mas;
        private const int replay = 10000000;

        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            mas = new long[n + 1];
            int elem = rnd.Next(0, 5), step = 5;
            for (int i = 0; i < n; i++)
            {
                mas[i] = elem;
                elem = rnd.Next(elem + 1, elem + step);
            }
        }

        private void onCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void onSearchClick(object sender, EventArgs e)
        {
            int key = (int)keyInput.Value;
            nonoptSearch(key);
            optimalSearch(key);
            interSearch(key);
            sequentSearch(key);
            orderedSearch(key);
        }

        private void nonoptSearch(int key)
        {
            int index = 0;
            int startTime = Environment.TickCount;
            for (int i = 0; i < replay; i++)
            {
                // Алгоритм неоптимального бинарного поиска (алогритм А)
                int left = 0;
                int right = n - 1;
                index = (right + left) / 2;

                while (left <= right)
                {
                    if (mas[index] == key) break;
                    if (mas[index] > key) right = index - 1;
                    else left = index + 1;
                    index = (right + left) / 2;
                }
            }
            int resultTime = Environment.TickCount - startTime;
            if (mas[index] != key) nonoptIndexText.Text = "Не найден";
            else nonoptIndexText.Text = index.ToString();
            nonoptTimeText.Text = resultTime.ToString();
        }

        private void optimalSearch(int key)
        {
            int index = 0;
            int left = 0;
            int right = n - 1;
            int startTime = Environment.TickCount;
            for (int i = 0; i < replay; i++)
            {
                // Алгоритм оптимального бинарного поиска (алогритм B)
                index = (right + left) / 2;

                while (left < right)
                {
                    if (key <= mas[index]) right = index;
                    else left = index + 1;
                    index = (right + left) / 2;
                }
            }
            int resultTime = Environment.TickCount - startTime;
            if (mas[right] != key) optIndexText.Text = "Не найден";
            else optIndexText.Text = right.ToString();
            optTimeText.Text = resultTime.ToString();
        }

        private void interSearch(int key)
        {
            int index = 0;
            int left = 0;
            int right = n - 1;
            int startTime = Environment.TickCount;

            for (int k = 0; k < replay; k++)
            {
                while (left < right && key > mas[left] && key < mas[right])
                {   
                    index = (int)(left + (key - mas[left]) * (right - left) / (mas[right] - mas[left]));

                    if (mas[index] == key) break;
                    else if (key < mas[index]) right = index - 1;
                    else left = index + 1;
                }

                if (mas[left] == key) index = left;
                else if (mas[right] == key) index = right;
            }

            int resultTime = Environment.TickCount - startTime;
            if (mas[index] != key) interIndexText.Text = "Не найден";
            else interIndexText.Text = index.ToString();
            interTimeText.Text = resultTime.ToString();
        }

        private void sequentSearch(int key)
        {
            int index = 0;
            int startTime = Environment.TickCount;

            for (int k = 0; k < replay; k++)
            {
                int P = 0;
                int B = n / 2;

                while (B > 0)
                {
                    while (P + B < n && mas[P + B] <= key)
                    {
                        P = P + B;
                    }

                    B = B / 2;
                }

                if (mas[P] == key) index = P;
            }

            int resultTime = Environment.TickCount - startTime;
            if (mas[index] == key) sequentIndexText.Text = index.ToString();
            else sequentIndexText.Text = "Не найден";
            sequentTimeText.Text = resultTime.ToString();
        }

        private void orderedSearch(int key) 
        {
            mas[n] = key + 1;
            int index = 0;

            int startTime = Environment.TickCount;
            for (int i = 0; i < 1000; i++)
            {
                int j = 0;
                while (mas[j] < key) 
                {
                    j++;
                }
                index = j;
            }
            int resultTime = (Environment.TickCount - startTime) * 10000;
            if (mas[index] != key) orderedIndexText.Text = "Не найден";
            else orderedIndexText.Text = index.ToString();
            orderedTimeText.Text = resultTime.ToString();
        }
    }
}
