using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using parserDecimal.Parser;
using System.Threading;

namespace Even_Search
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        decimal H, tol, y0, x1, x0, y1, xMe, abs;
        int k, k_max, check, Cond;
        string f, out1, out2, out3, out4, out5, msg, title, elapsedTime;
        Stopwatch sw = new Stopwatch();
        TimeSpan ts;
        DialogResult response;
        double time, eltime;
        //single eltime1;
        private decimal parserFx(decimal a)
        {
            Computer computer = new Computer();
            decimal d = computer.Compute(f, a);
            return d;
        }
        private decimal proizvod(string b,decimal a)
        {
            Computer computer = new Computer();
            return computer.Compute(b, a);
        }
        public void noll()
        {
            check = 0;
            progressBar1.Visible = false;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            title = "Even Search";
            xMe = 0;
            eltime = 0;
            time = 0;
            //eltime1=0;
            y1 = 0;
            y0 = 0;
            x1 = 0;
            x0 = 0;
            tol = 0;
            k_max = 0;
            Cond = 0;
            k = 0;
            abs = 0;
            f = "";
            out1 = "";
            out2 = "";
            out3 = "";
            out4 = "";
            out5 = "";
        }
            

        public void test()
        {
            //1 
            try
            {
                x0 = decimal.Parse(textBox1.Text);
                check = check + 1;
            }
            catch (Exception) { MessageBox.Show("x0 is incorrect!"); };
            //2
            try
            {
                f = comboBox1.Text;
                f = f.ToLower();//Uppercase transformed to lower;
                decimal d;
                d = parserFx(x0);
                check = check + 1;
            }
            catch (Exception e)
            {
                string s = "Reference on object doesn't point on example of object.";
                //if (s != e.Message)
                //{
                //    MessageBox.Show("May be 'x' argument of the 'log(x)' logarithmic function is x<=0. Try to revise initial point x0");
                //}


                MessageBox.Show("f(x) is not correct!");

            }
            //3
            try
            {
                tol = decimal.Parse(textBox2.Text, System.Globalization.NumberStyles.Float);//Specifies styles for String representations of numeric values.
                check = check + 1;
            }
            catch (Exception) { MessageBox.Show("Tolerance doesn't correct"); };
            //4
            try
            {
                k_max = int.Parse(textBox3.Text);
                if (k_max <= 0) { MessageBox.Show("k_max is not correct!"); }
                else
                {
                    check = check + 1;
                }
            }
            catch (Exception) { MessageBox.Show("k_max is not correct!"); };
            //5
            try
            {
                time = double.Parse(textBox4.Text);
                if (time <= 0) { MessageBox.Show("Time is not correct!"); }
                else
                {
                    check = check + 1;
                }
            }
            catch (Exception) { MessageBox.Show("Time is not correct"); };
        }
        //function to find derivative
        private string diff(string f)
        {
            Derivative derivative = new Derivative();
            string k = derivative.ReturnDerivative(f);
            return k;
        }
        public void clear_output()
        {
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            label6.Visible = false;
            progressBar1.Visible = false;
            progressBar1.Value = 0;
        }


        
        public void output()
        {
            decimal pro,pro1,pro2;
            string ww; 
            ww = comboBox1.Text;
            //decimal dec ;
            //dec= Decimal.Parse(diff(ww));
            //pr1 = parserFx(dec);
            textBox5.Text = Convert.ToString(x1);
            textBox6.Text = y1.ToString("G28", System.Globalization.CultureInfo.InvariantCulture);
            textBox7.Text = abs.ToString("G28", System.Globalization.CultureInfo.InvariantCulture);
            textBox8.Text = Convert.ToString(k);
            textBox10.Text = diff(ww);
            pro=proizvod(diff(ww), x1);
            textBox11.Text = Convert.ToString(pro);
            pro1 = proizvod(diff(ww), x1 - tol);
            textBox12.Text = Convert.ToString(pro1);
            pro2 = proizvod(diff(ww), x1 + tol);
            textBox13.Text = Convert.ToString(pro2);


            //textBox9.Text = Convert.ToString(ts.Milliseconds);
        }


        private void algo_max()//ALGORITGM TO FIND MAX
        {
            try
            {
                TimeSpan ts = new TimeSpan();
                sw.Reset();
                sw.Start();
                progressBar1.Maximum = k_max;
                H = tol;
                y0 = parserFx(x0);
                x1 = Decimal.Add(x0, H);//x0+H 
                y1 = parserFx(x1);
                int compare0, compare1, compare2;
                compare0 = 0;
                compare1 = 0;
                compare2 = 0;
                k = 0;
                ts = sw.Elapsed;

                while (k < k_max || time > ts.TotalMilliseconds)
                {
                    ts = sw.Elapsed;
                    progressBar1.Visible = true;
                    progressBar1.Increment(1);
                    xMe = x1;
                    k = k + 1;
                    compare0 = Decimal.Compare(y1, y0);
                    if (compare0 <= 0)//y1<=y0
                    {
                        x1 = x0;
                        y1 = y0;
                    }
                    else
                    {
                        x0 = x1;
                        y0 = y1;
                        x1 = Decimal.Add(x0, H);
                        y1 = parserFx(x1);
                    }
                    compare1 = Decimal.Compare(xMe, x1);
                    if (compare1 == 0)//*(xMe==x1)
                    {
                        compare2 = Decimal.Compare(decimal.Parse(textBox1.Text), x1);
                        if (compare2 >= 0)
                        {
                            sw.Stop();
                            MessageBox.Show("ESM can't find an extrum or initial point x0= " + textBox1.Text + "may be a solution or may be the initial point is placed in the right-of-solution x*");
                            label6.Text = "The right-of-side range of solution x* is forbidden area.\nTry to revise initial point to check validity of the value x0";
                            label6.Visible = true;
                        }
                        else
                        {
                            sw.Stop();
                            label6.Text = "    The solution was found with desired tolerance. If f'(x)> 0 where x is less\nthan optimal x* and f'(x)<0 where x is greater than optimal x*,then f(x*) is local maximum. \nSo by applying this theorem above i can say that i found the local maximum point";
                            label6.Visible = true;
                        } break;
                    }
                    if (k > k_max)
                    {
                        sw.Stop();
                        msg = "not enough iteration for solution, add iteration?";
                        response = MessageBox.Show(msg, title, MessageBoxButtons.YesNo);
                        if (response == DialogResult.Yes)
                        {
                            k_max = k_max + k_max;
                            sw.Start();
                        }
                        else
                        {
                            label6.Text = "     The maximum number of iterations is exceeded!";
                            label6.Visible = true;
                            break;
                        }
                    }
                    ts = sw.Elapsed;
                    if (time <= ts.TotalSeconds)
                    {
                        sw.Stop();
                        msg = "  not enough time for  solution, add time?";
                        response = MessageBox.Show(msg, title, MessageBoxButtons.YesNo);
                        if (response == DialogResult.Yes)
                        {
                            time = time + time;
                            sw.Start();
                        }
                        else
                        {
                            label6.Text = " The maximum time was exceeded!";
                            label6.Visible = true;
                            break;
                        }
                    }
                    abs = Math.Abs(decimal.Subtract(x1, x0));
                    //eltime =sw.ElapsedMilliseconds
                    ts = sw.Elapsed;
                }//end while
                sw.Stop();
                ts = sw.Elapsed;
                eltime = ts.TotalSeconds;
                eltime = Math.Round(eltime, 1);//округление времени
                if (time < eltime)
                {
                    textBox9.Text = "" + time;
                }
                else
                {
                    textBox9.Text = ts.TotalSeconds.ToString("0.0");
                }

                if (k < k_max)
                {
                    if (k_max > 10000)
                    {
                        progressBar1.Maximum = 1000;
                        for (int i = 0; i < 1000; i++)
                        {
                            progressBar1.Increment(1);
                        }
                    }
                    else
                    {
                        for (int i = k; i < k_max; i++)
                        {
                            progressBar1.Increment(1);
                        }
                    }
                }
                output();
            }//END TRY
            catch (Exception e)
            {
                clear_output();
                MessageBox.Show("ESM can not find extrum or initial point x0=" + textBox1.Text + " may be a solution or may be the initial point is placed in the right-of-solution x* ");
                label6.Visible = true;
                label6.Text = " Try to revise initial point to check the validity of the value x0.";
            }
            noll();
        }

        private void algo_min()//algorithm of esm to find MIN
        {
            try
            {
                TimeSpan ts = new TimeSpan();
                sw.Reset();
                sw.Start();
                progressBar1.Maximum = k_max;
                H = tol;
                y0 = parserFx(x0);
                x1 = Decimal.Add(x0, H);//x0+H 
                y1 = parserFx(x1);
                int compare0, compare1, compare2;
                compare0 = 0;
                compare1 = 0;
                compare2 = 0;
                k = 0;
                ts = sw.Elapsed;

                while (k < k_max || time < ts.TotalMilliseconds)
                {
                    ts = sw.Elapsed;
                    progressBar1.Visible = true;
                    progressBar1.Increment(1);
                    xMe = x1;
                    k = k + 1;
                    compare0 = Decimal.Compare(y1, y0);
                    if (compare0 >= 0)//y1>=y0
                    {
                        x1 = x0;
                        y1 = y0;
                    }
                    else
                    {
                        x0 = x1;
                        y0 = y1;
                        x1 = Decimal.Add(x0, H);
                        y1 = parserFx(x1);
                    }
                    compare1 = Decimal.Compare(xMe, x1);
                    if (compare1 == 0)//*(xMe==x1)
                    {
                        compare2 = Decimal.Compare(decimal.Parse(textBox1.Text), x1);
                        if (compare2 >= 0)
                        {
                            sw.Stop();
                            MessageBox.Show("ESM can't find an extrum or initial point x0= " + textBox1.Text + "may be a solution or may be the initial point is placed in the right-of-solution x*");
                            label6.Text = "The right-of-side range of solution x* is forbidden area.\nTry to revise initial point to check validity of the value x0";
                            label6.Visible = true;
                        }
                        else
                        {
                            sw.Stop();
                            label6.Text = "The solution was found with desired tolerance.If f'(x)<0 where x is less\nthan optimal x* and f'(x)>0 where x is greater than optimal x*,then f(x*) is local minimum. \nSo by applying this theorem above i can say that i found the local minimum point";
                            label6.Visible = true;
                        } break;
                    }
                    if (k >= k_max)
                    {
                        sw.Stop();
                        msg = "not enough iteration for solution, add iteration?";
                        response = MessageBox.Show(msg, title, MessageBoxButtons.YesNo);
                        if (response == DialogResult.Yes)
                        {
                            k_max = k_max + k_max;
                            sw.Start();
                        }
                        else
                        {
                            label6.Text = "     The maximum number of iterations is exceeded!";
                            label6.Visible = true;
                            break;
                        }
                    }
                    ts = sw.Elapsed;
                    if (time <= ts.TotalSeconds)
                    {
                        sw.Stop();
                        msg = "  not enough time for  solution, add time?";
                        response = MessageBox.Show(msg, title, MessageBoxButtons.YesNo);
                        if (response == DialogResult.Yes)
                        {
                            time = time + time;
                            sw.Start();
                        }
                        else
                        {
                            label6.Text = " The maximum time was exceeded!";
                            label6.Visible = true;
                            break;
                        }
                    }
                    abs = Math.Abs(decimal.Subtract(x1, x0));
                    //eltime =sw.ElapsedMilliseconds
                    ts = sw.Elapsed;
                }//end while
                sw.Stop();
                ts = sw.Elapsed;
                eltime = ts.TotalSeconds;
                eltime = Math.Round(eltime, 1);//округление времени
                if (time < eltime)
                {
                    textBox9.Text = "" + time;
                }
                else
                {
                    textBox9.Text = ts.TotalSeconds.ToString("0.0");
                }

                if (k < k_max)
                {
                    if (k_max > 10000)
                    {
                        progressBar1.Maximum = 1000;
                        for (int i = 0; i < 1000; i++)
                        {
                            progressBar1.Increment(1);
                        }
                    }
                    else
                    {
                        for (int i = k; i < k_max; i++)
                        {
                            progressBar1.Increment(1);
                        }
                    }
                }
                output();
            }//END TRY
            catch (Exception e)
            {
                clear_output();
                MessageBox.Show("ESM can not find extrum or initial point x0=" + textBox1.Text + " may be a solution or may be the initial point is placed in the right-of-solution x* ");
                label6.Visible = true;
                label6.Text = " Try to revise initial point to check the validity of the value x0.";
            }
            noll();
        }

    
        


     

        private void button2_Click_1(object sender, EventArgs e)
        {
            clear_output();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label6.Visible = false;
            test();
            if (check == 5)
            {
                if (radioButton1.Checked) { algo_min(); }
                else { algo_max(); }
            }
            else
            {
                noll();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     


            

    }
}
