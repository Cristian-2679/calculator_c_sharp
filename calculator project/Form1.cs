using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            makebuttons();
            label2.Text = "";
        }

        public struct btnStruct
        {
            public string content;
            public bool font;
            public btnStruct(string c, bool f)
            {
                this.content = c;
                this.font = f;
            }
        }

        private btnStruct[,] buttons =
        {
            { new btnStruct("%", false),new btnStruct("CE", true),new btnStruct("C", true),new btnStruct("<X|", true)},
            { new btnStruct("1/x", true),new btnStruct("x²", true),new btnStruct("²√x", true),new btnStruct("/", false)},
            { new btnStruct("7", false),new btnStruct("8", false),new btnStruct("9", false),new btnStruct("*", false)},
            { new btnStruct("4", false),new btnStruct("5", false),new btnStruct("6", false),new btnStruct("-", false)},
            { new btnStruct("1", false),new btnStruct("2", false),new btnStruct("3", false),new btnStruct("+", false)},
            { new btnStruct("+/-", false),new btnStruct("0", false),new btnStruct(",", false),new btnStruct("=", false)}
        };

        private void makebuttons()
        {
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Button mybutton= new Button();
                    mybutton.Text= buttons[i, j].content;
                    mybutton.Top = 162+ 62*i;
                    mybutton.Left = 105*j;
                    mybutton.Width = 105;
                    mybutton.Height = 62;
                    mybutton.BackColor = Color.SandyBrown;
                    mybutton.ForeColor = Color.Wheat;
                    if (buttons[i, j].font)
                    {
                        mybutton.Font = new Font("Unown", 34);
                    }
                    else
                    {
                        mybutton.Font = new Font("Jokerman", 21);
                    }
                    if( j==3 || i == 1 || i == 0)
                    {
                        mybutton.BackColor = Color.BurlyWood;
                        mybutton.ForeColor = Color.Coral;
                    }
                    if (i + j == 8)
                    {
                        mybutton.BackColor = Color.Coral;
                        mybutton.ForeColor = Color.BurlyWood;
                    }
                    mybutton.Click += Mybutton_Click;
                    this.Controls.Add(mybutton);
                }
            }
        }
        static bool fase= false;
        private void Mybutton_Click(object sender, EventArgs e)
        {
            Button h = (Button)sender;
            string j = h.Text;
            //System.Windows.Forms.Label lb = label1;
            switch (j)
            {
                case "1":
                    label1.Text += "1";
                    break;
                case "2":
                    label1.Text += "2";
                    break;
                case "3":
                    label1.Text += "3";
                    break;
                case "4":
                    label1.Text += "4";
                    break;
                case "5":
                    label1.Text += "5";
                    break;
                case "6":
                    label1.Text += "6";
                    break;
                case "7":
                    label1.Text += "7";
                    break;
                case "8":
                    label1.Text += "8";
                    break;
                case "9":
                    label1.Text += "9";
                    break;
                case "0":
                    label1.Text += "0";
                    break;
                case "CE":
                    label1.Text = "0";
                    break;
                case "C":
                    label1.Text = "";
                    label2.Text = "";
                    fase = false;
                    break;
                case "%":
                    label1.Text += "%";
                    break;
                case "<X|":
                    if(label1.Text.Length > 0) 
                    { 
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1);
                    }
                    break;
                case "x²":
                    label1.Text += "²";
                    fase = false;
                    end(1);
                    break;
                case "²√x":
                    label1.Text = "√" + label1.Text;
                    fase = false;
                    end(2);
                    break;
                case "1/x":
                    label1.Text = "1/" + label1.Text;
                    fase = false;
                    end(3);
                    break;
                case "/":
                    if (fase == false)
                    {
                        label1.Text += "/";
                        Next();
                    }
                    break;
                case "*":
                    if (fase == false)
                    {
                        label1.Text += "*";
                        Next();
                    }
                    break;
                case "+":
                    if (fase == false)
                    {
                        label1.Text += "+";
                        Next();
                    }
                    break;
                case "-":
                    if (fase == false)
                    {
                        label1.Text += "-";
                        Next();
                    }
                    break;
                case "+/-":
                    if (label1.Text[0] == '+')
                    {
                        label1.Text = "-" + label1.Text.Substring(1);
                    }
                    else
                    {
                        if (label1.Text[0] == '-')
                        {
                            label1.Text = "+" + label1.Text.Substring(1);
                        }
                        else
                        {
                            label1.Text= "-"+label1.Text;
                        }

                    }
                    break;
                case ",":
                    if(!(label1.Text.Contains(",") || label1.Text.Length==0))
                    {
                        label1.Text += ",";
                    }
                    break;
                case "=":
                    end(0);
                    break;
            }
            if (label1.Text.Length > 1 && label1.Text[0] == '0' && label1.Text[1] != ',' && label1.Text[1] != '%' && label1.Text[1] != '²')
            {
                label1.Text = label1.Text.Substring(1);
            }
            string strout;
            /*if (label1.Text.Length > 0)
            {
                double num = double.Parse(label1.Text);
                strout=num.ToString("#,##0.################");
                label1.Text=strout;
            }*/
            if(label1.Text.Length > 8 && ((label1.Text.Length - 8) * (21/8)) >= 40)
            {
                float delta = (label1.Text.Length - 8);
                float v1 = 48;
                float v2 = 21/8;
                float val = v1 - (delta * v2);
                if (val > 0)
                {
                    label1.Font = new Font("Jokerman", val , FontStyle.Regular);
                }
                else
                {
                    label1.Font = new Font("Jokerman", 21, FontStyle.Regular);
                }
            }
            else
            {
                if(label1.Text.Length < 8)
                {
                    label1.Font = new Font("Jokerman", 48, FontStyle.Regular);
                }
                else
                {
                    label1.Font = new Font("Jokerman", 21, FontStyle.Regular);
                }
            }
        }

        private void end(int operation)
        {
            if (fase)
            {
                double n1 = Convert.ToDouble(label2.Text.Remove(label2.Text.Length - 1));
                double n2 = Convert.ToDouble(label1.Text);
                char op = label2.Text[label2.Text.Length - 1];
                //MessageBox.Show(n1 + "()" + n2 + "[]" + op);
                switch (op)
                {
                    case '-':
                        label1.Text = (n1 - n2).ToString();
                        break;
                    case '+':
                        label1.Text = (n1 + n2).ToString();
                        break;
                    case '*':
                        label1.Text = (n1 * n2).ToString();
                        break;
                    case '/':
                        label1.Text = (n1 / n2).ToString();
                        break;
                }
                label2.Text += n2;
            }
            else
            {
                switch (operation)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
            }
            fase = !fase;
        }

        private void Next()
        {
            label2.Text = label1.Text;
            label1.Text = "";
            fase = true;
        }
    }
}
