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

            label_a11r.Text = "a11";
            label_a12r.Text = "a12";
            label_a13r.Text = "a13";
            label_a21r.Text = "a21";
            label_a22r.Text = "a22";
            label_a23r.Text = "a23";

            label_a11rt.Text = "a11";
            label_a12rt.Text = "a12";
            label_a21rt.Text = "a21";
            label_a22rt.Text = "a22";
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

            double[] vector = new double[2];
            double[] vector_x = new double[2];
            double[,] matrix_der = new double[2, 2];
            double[,] matrix = new double[2, 3];
            double[] vector_c = new double[2];
            double[] vector_der = new double[2];
            double[] vector_xs = new double[2];
            double n = 0;
            vector[0] = 0.5;
            vector[1] = 2;

            matrix[0, 0] = Math.Pow(vector[1], 1/3);
            matrix[0, 1] = vector[0];
            matrix[0, 2] = -4;
            matrix[1, 0] = 2 / vector[0];
            matrix[1, 1] = -vector[1];
            matrix[1, 2] = 1;

            matrix_der[0, 0] = Math.Pow(vector[1], 3);
            matrix_der[0, 1] = 1;
            matrix_der[1, 0] = -2 / Math.Pow(vector[0], 3);
            matrix_der[1, 1] = -1;

            for (int i = 0; i < 2; i++)
            {
                vector_x[i] = vector[i];
            }
            for (int j = 0; j < 5; j++)
            {
                n = j;
                for (int i = 0; i < 2; i++)
                {
                    vector_xs[i] = vector_x[i];
                }
                matrix[0, 0] = Math.Pow(vector_x[1], 1 / 3);
                matrix[0, 1] = vector_x[0];
                matrix[0, 2] = -4;
                matrix[1, 0] = 2 / vector_x[0];
                matrix[1, 1] = -vector_x[1];
                matrix[1, 2] = 1;

                matrix_der[0, 0] = Math.Pow(vector_x[1], 3);
                matrix_der[0, 1] = 1;
                matrix_der[1, 0] = -2 / Math.Pow(vector_x[0], 3);
                matrix_der[1, 1] = -1;

                for (int i = 0; i < 2; i++)
                {
                    vector_der[i] = matrix[i, 0] + matrix[i, 1] + matrix[i, 2];
                }

                for (int i = 0; i < 2; i++)
                {
                    vector_c[i] = matrix_der[i, 0] * vector_der[0] + matrix_der[i, 1] * vector_der[1];
                }
                for (int i = 0; i < 2; i++)
                {
                    vector_x[i] = vector_x[i] - vector_c[i];
                }
                if (vector_x[0] == (vector_xs[0]+error) && vector_x[0] == (vector_xs[0] - error) && vector_x[1] == (vector_xs[1] + error) && vector_x[1] == (vector_xs[1] - error))
                {
                    break;
                }
            }

            label_a11r.Text = Convert.ToString(matrix[0, 0]);
            label_a12r.Text = Convert.ToString(matrix[0, 1]);
            label_a13r.Text = Convert.ToString(matrix[0, 2]);
            label_a21r.Text = Convert.ToString(matrix[1, 0]);
            label_a22r.Text = Convert.ToString(matrix[1, 1]);
            label_a23r.Text = Convert.ToString(matrix[1, 2]);

            label_a11rt.Text = Convert.ToString(matrix_der[0, 0]);
            label_a12rt.Text = Convert.ToString(matrix_der[0, 1]);
            label_a21rt.Text = Convert.ToString(matrix_der[1, 0]);
            label_a22rt.Text = Convert.ToString(matrix_der[1, 1]);

            label_x1r.Text = Convert.ToString(vector_x[0]);
            label_x2r.Text = Convert.ToString(vector_x[1]);
            label_n.Text = Convert.ToString(n);
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
