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
            label_halfi.Text = "N= ";
            label_tangenti.Text = "N= ";
            label_chordi.Text = "N= ";
            label_combi.Text = "N= ";
            label_half.Text = "X= ";
            label_tangent.Text = "X= ";
            label_chord.Text = "X= ";
            label_comb.Text = "X= ";
            textBox_error.Clear();
            textBox_a.Clear();
            textBox_b.Clear();


        }
        private void button_calculate_Click(object sender, EventArgs e)
        {
            double a, b, error;

            double[] x = new double[4];

            error = Convert.ToDouble(textBox_error.Text);
            a = Convert.ToDouble(textBox_a.Text);
            b = Convert.ToDouble(textBox_b.Text);

            x[0] = half_division_method(a, b, error);
            label_half.Text = "X= " +  Convert.ToString(x[0]);

            x[1] = tangent_method(a, b, error);
            label_tangent.Text = "X= " + Convert.ToString(x[1]);

            x[2] = chord_method(a, b, error);
            label_chord.Text = "X= " + Convert.ToString(x[2]);

            x[3] = combined_method(a, b, error);
            label_comb.Text = "X= " + Convert.ToString(x[3]);


        }

        public double chord_method_func(double a_h, double b_h)
        {
            double x;
            x = a_h - ((2 * Math.Pow(a_h, 2) * b_h - 2 * Math.Pow(a_h, 3) - 4 * a_h * b_h + 4 * Math.Pow(a_h, 2) + 2 * b_h - 2 * a_h - 3 * b_h * Math.Exp(a_h) + 3 * a_h * Math.Exp(a_h)) /
                            (2 * Math.Pow(b_h, 2) - 4 * b_h - 3 * Math.Exp(b_h) - 2 * Math.Pow(a_h, 2) + 4 * a_h + 3 * Math.Exp(a_h)));
            return x;
        }

        public double first_function(double a_h)
        {
            double x;

            x = Math.Pow(a_h - 1, 2) - (1.5 * Math.Exp(a_h));

            return x;
        }

        public double second_derivative(double a_h)
        {
            double x;

            x = 2 - (3 * Math.Exp(a_h) / 2);

            return x;
        }

        public double tangent_method_func(double b_h)
        {
            double x;
            x = (2 * Math.Pow(b_h, 2) - 3 * b_h * Math.Exp(b_h) - 2 + 3 * Math.Exp(b_h)) / (4 * b_h - 4 - 3 * Math.Exp(b_h));
            return x;
        }

        public double half_division_method(double a_h, double b_h, double error)
        {
            double x = 0, x_s, a_s;

            for (int i = 0; i < 1000; i++)
            {
                x = ((a_h + b_h) / 2);

                a_s = first_function(a_h);

                x_s = first_function(x);

                if (Math.Sign(x_s) == Math.Sign(a_s))
                {
                    a_h = x;
                }
                if (Math.Sign(x_s) != Math.Sign(a_s))
                {
                    b_h = x;
                }
                if (Math.Abs((b_h - a_h) / Math.Pow(2, i + 1)) <= error)
                {
                    label_halfi.Text = "N= " + Convert.ToString(i);
                    return x;
                }
            }

            return x;
        }

        public double tangent_method(double a_h, double b_h, double error)
        {
            double x = 0, x_s, a_s, b_s, b_ss, a_ss;

            for (int i = 0; i < 1000; i++)
            {
                x_s = x;
                x = tangent_method_func(x);

                a_s = first_function(a_h);
                b_s = first_function(b_h);

                a_ss = second_derivative(a_h);
                b_ss = second_derivative(b_h);

                if (Math.Abs(x - x_s) <= error)
                {
                    label_tangenti.Text = "N= " + Convert.ToString(i);
                    return x;
                }
                if (a_s * a_ss > 0)
                {
                    a_h = x;
                }
                if (b_s * b_ss > 0)
                {
                    b_h = x;
                }
            }
            return x;
        }

        public double chord_method(double a_h, double b_h, double error)
        {
            double x = 0, x_s, a_s, b_s, b_ss, a_ss, c;
            c = a_h;

            for (int i = 0; i < 1000; i++)
            {
                x_s = x;

                a_s = first_function(a_h);
                b_s = first_function(b_h);

                a_ss = second_derivative(a_h);
                b_ss = second_derivative(b_h);

                x = chord_method_func(x, c);


                if (a_s * a_ss > 0)
                {
                    b_h = x;
                    a_h = c;
                }

                if (b_s * b_ss > 0)
                {
                    a_h = x;
                    b_h = c;
                }

                if (Math.Abs(x - x_s) <= error)
                {
                    label_chordi.Text = "N=" + Convert.ToString(i);
                    return x;
                }
            }
            return x;
        }

        public double combined_method(double a_h, double b_h, double error)
        {
            double x = 0;

            for (int i = 0; i < 1000; i++)
            {

                b_h = tangent_method_func(b_h);

                a_h = chord_method_func(a_h, b_h);


                if (Math.Abs(b_h - a_h) <= error)
                {
                    label_combi.Text = "N= " + Convert.ToString(i);
                    return x;
                }

                x = a_h + b_h / 2;
            }
            return x;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
