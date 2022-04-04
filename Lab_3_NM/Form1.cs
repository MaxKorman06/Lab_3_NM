using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3_NM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_exit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {

        }
        private void button_calculate_Click(object sender, EventArgs e)
        {
            double x_s, a_s, a, a_h, b, b_h, b_s, a_ss, b_ss, error;

            double[] x = new double[4];

            error = Convert.ToDouble(textBox_error.Text);
            a = Convert.ToDouble(textBox_a.Text);
            b = Convert.ToDouble(textBox_b.Text);

            a_h = a;
            b_h = b;
            
            for (int i = 0; i < 1000; i++)
            {
                x[0] = ((a_h + b_h) / 2);

                a_s = Math.Pow(a_h - 1, 2) - (1.5 * Math.Exp(a_h));

                x_s = Math.Pow(x[0] - 1, 2) - (1.5 * Math.Exp(x[0]));

                if (Math.Sign(x_s) == Math.Sign(a_s))
                {
                    a_h = x[0];
                }
                if (Math.Sign(x_s) != Math.Sign(a_s))
                {
                    b_h = x[0];
                }
                if (Math.Abs((b_h - a_h) / Math.Pow(2, i + 1)) <= error)
                {
                    break;
                }
            }
            label1.Text = Convert.ToString(x[0]);

            a_h = a;
            b_h = b;

            for (int i = 0; i < 1000; i++)
            {
                x_s = x[1];
                x[1] = (2 * Math.Pow(x[1], 2) - 3 * x[1] * Math.Exp(x[1]) - 2 + 3 * Math.Exp(x[1])) / (4 * x[1] - 4 - 3 * Math.Exp(x[1]));
                a_s = Math.Pow(a_h - 1, 2) - (1.5 * Math.Exp(a_h));
                b_s = Math.Pow(b_h - 1, 2) - (1.5 * Math.Exp(b_h));

                a_ss = 2 - (3 * Math.Exp(a_h) / 2);
                b_ss = 2 - (3 * Math.Exp(b_h) / 2);
                
                if (Math.Abs(x[1] - x_s) <= error)
                {
                    break;
                }

                if (a_s * a_ss > 0)
                {
                    a_h = x[1];
                }

                if (b_s * b_ss > 0)
                {
                    b_h = x[1];
                }
                
            }

            label6.Text = Convert.ToString(x[1]);

            a_h = a;
            b_h = b;

            for (int i = 0; i < 1000; i++)
            {
              
                b_h = (2 * Math.Pow(b_h, 2) - 3 * b_h * Math.Exp(b_h) - 2 + 3 * Math.Exp(b_h)) / (4 * b_h - 4 - 3 * Math.Exp(b_h));

                a_h = (2 * a_h * b_h - 2 + 3 * Math.Exp(x[2])) / (2 * b_h - 4 + 2 * a_h);

                

                if (Math.Abs(b_h - a_h) <= error)
                {
                    break;
                }  

                x[2] = a_h + b_h / 2;
            }

            label7.Text = Convert.ToString(x[2]);

        }

        public double half_dec()
        {
            
            return 0;
        }
    }
}
