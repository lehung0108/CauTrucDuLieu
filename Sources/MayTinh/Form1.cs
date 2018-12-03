using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MayTinh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool NhapLai;
        void ThemKiTu(string c)
        {
            if (NhapLai)
            {
                manhinh.Text = "";
                Invalidate();
            }
            manhinh.Text += c;
            NhapLai = false;
        }

        void XoaKiTu()
        {
            if (manhinh.Text.Length != 0)
                manhinh.Text = manhinh.Text.Remove(manhinh.Text.Length - 1);
            else
                MessageBox.Show("Khong co gi de xoa");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            ThemKiTu("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemKiTu("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThemKiTu("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThemKiTu("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ThemKiTu("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ThemKiTu("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ThemKiTu("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ThemKiTu("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ThemKiTu("9");
        }

        private void buttonkq_Click_1(object sender, EventArgs e)
        {
            ThucHienPhepTinh();
            NhapLai = true;
        }


        private void buttonXoa_Click(object sender, EventArgs e)
        {
            XoaKiTu();
        }

     
        private void button0_Click(object sender, EventArgs e)
        {
            ThemKiTu("0");
        }

        private void buttonCong_Click(object sender, EventArgs e)
        {
            manhinh.Text += '+';
            NhapLai = false;
        }

        private void buttonTru_Click(object sender, EventArgs e)
        {
            manhinh.Text += '-';
            NhapLai = false;
        }

        private void buttonNhan_Click(object sender, EventArgs e)
        {
            manhinh.Text += '*';
            NhapLai = false;
        }

        private void buttonChia_Click(object sender, EventArgs e)
        {
            manhinh.Text+='/';
            NhapLai = false;
        }

        public static int GetPriority(string op)
        {
            if (op == "sin" || op == "cos" || op == "tan" || op == "cotg" ||op == "sqrt")
                return 3;
            if (op == "*" || op == "/" || op == "%")
                return 2;
            if (op == "+" || op == "-")
                return 1;
            return 0;
        }
        // code thuat toan

        void ThucHienPhepTinh()
        {
            manhinh.Text += ' ';
            Stack sh = new Stack();
            Stack st = new Stack();
            int i = 0, j = 0;
            double ketQua = 0, a, b;
            string str1;

            while (i < manhinh.Text.Length-1)
            {
                
                //nạp sin,cos...vào toán tử st
                if (manhinh.Text[i] >= 'a' && manhinh.Text[i] <= 'z')
                {
                    j = i;
                    while (manhinh.Text[i] >= 'a' && manhinh.Text[i] <= 'z')
                        i++;
                    string str = manhinh.Text.Substring(j, i - j);
                    st.Push((str.ToString()));
                }

                //nạp toán hạng vào sh
                if (manhinh.Text[i] >= '0' && manhinh.Text[i] <= '9')
                {
                    j = i;
                    while (manhinh.Text[i+1] >= '0' && manhinh.Text[i+1] <= '9' ||manhinh.Text[i+1] =='.' && manhinh.Text[i + 1] != ' ' && i+1 < manhinh.Text.Length)
                        i++;
                    string str = manhinh.Text.Substring(j, i + 1-j);   
                    sh.Push((str.ToString()));
                }

                //nạp dấu ( vào st
                if (manhinh.Text[i] == '(')
                {
                    st.Push(manhinh.Text[i]);
                    if (manhinh.Text[i + 1] == '-' || manhinh.Text[i + 1] == '+')
                        sh.Push(0);
                }

                //nạp toán tử vào st
                if (manhinh.Text[i] == '+' || manhinh.Text[i] == '-' || manhinh.Text[i] == '*' || manhinh.Text[i] == '/' || manhinh.Text[i] == '%'  || manhinh.Text[i] == '^')
                {   
                    while (st.Count > 0 && GetPriority(manhinh.Text[i].ToString()) <= GetPriority(st.Peek().ToString()))
                    {
                        str1 = st.Pop().ToString();

                        //nếu là các phép tính
                        if (str1 == "+" || str1 == "-" || str1 == "*" || str1 == "/" || str1 == "%" || str1 == "^")
                        {
                            a = double.Parse((sh.Pop().ToString()));
                            b = double.Parse((sh.Pop().ToString()));
                            switch (str1)
                            {
                                case "+":
                                    ketQua = (a + b);
                                    break;
                                case "-":
                                    ketQua = (b - a);
                                    break;
                                case "*":
                                    ketQua = (a * b);
                                    break;
                                case "/":
                                    if (a == 0)
                                    {
                                        MessageBox.Show("Không thể chia cho 0", "Thông báo");
                                    }
                                    ketQua = (b / a);
                                    break;
                                case "%":
                                    if (a == 0)
                                    {
                                        MessageBox.Show("Không thể chia cho 0", "Thông báo");
                                    }
                                    ketQua = (b % a);
                                    break;
                                case "^":
                                    ketQua = Math.Pow(b, a);
                                    break;
                            }
                            sh.Push(ketQua);
                        }

                        //nếu là sin, cos, tan, cotag
                        else
                            if (str1 == "sin" || str1 == "cos" || str1 == "tan" || str1 == "cotg" || str1 == "sqrt")
                            {
                                
                                switch (str1)
                                {
                                    case "sin":
                                     
                                        sh.Push(Math.Sin(double.Parse(sh.Pop().ToString())));
                                        break;
                                    case "cos":
                                        
                                        sh.Push(Math.Cos(double.Parse(sh.Pop().ToString())));
                                        break;
                                    case "tan":
                                        
                                        sh.Push(Math.Tan(double.Parse(sh.Pop().ToString())));
                                        break;
                                    case "cotg":
                                       
                                        sh.Push(1 / Math.Tan(double.Parse(sh.Pop().ToString())));
                                        break;                                  
                                    case "sqrt":
                                        
                                        sh.Push(Math.Sqrt(double.Parse(sh.Pop().ToString())));
                                        break;
                                }
                            }
                    }

                    //xử lí nhiều dấu + hoặc - hoặc + - hoặc - + liên tiếp(số âm)
                    
                    if (manhinh.Text[i] == '-' && manhinh.Text[i + 1] == '-' || manhinh.Text[i] == '-' && manhinh.Text[i + 1] == '+' || manhinh.Text[i] == '+' && manhinh.Text[i + 1] == '+'|| manhinh.Text[i] == '+' && manhinh.Text[i + 1] == '-')
                    {
                        if (manhinh.Text[i] == '-')
                            j = 1;
                        while (manhinh.Text[i + 1] == '-' || manhinh.Text[i + 1] == '+')
                        {
                            if (manhinh.Text[i + 1] == '-')
                                j++;
                            i++;
                        }
                        if (( j) % 2 == 0)
                            str1 = "+";
                        else
                            str1 = "-";
                        st.Push((str1.ToString()));
                    }
                    else
                        st.Push(manhinh.Text[i]);
                }

                

                //xử lí các phép toán trong dấu ngoặc
                if (manhinh.Text[i] == ')')
                {
                    str1 = "+"; // khai báo str1 ảo(tránh stack rỗng)
                    while (str1 != "(")
                    {
                        
                        a = double.Parse((sh.Pop().ToString()));
                        str1 = st.Pop().ToString();
                        if (str1 == "(")
                        {
                            sh.Push(a);
                            break;
                        }
                        if (sh.Count == 0 && (str1 == "+" || str1 == "-")) b = 0;
                        else
                            if (sh.Count == 0 && (str1 == "*" || str1 == "/"))
                            {
                                MessageBox.Show("Loi!");
                                break;
                            }
                            else
                                b = double.Parse((sh.Pop().ToString()));
                        switch (str1)
                        {
                            case "+":
                                ketQua = (a + b);
                                break;
                            case "-":
                                ketQua = (b - a);
                                break;
                            case "*":
                                ketQua = (a * b);
                                break;
                            case "/":
                                if (a == 0)
                                {
                                    MessageBox.Show("Không thể chia cho 0", "Thông báo");                                
                                }
                                ketQua = (b / a);
                                break;
                            case "%":
                                if (a == 0)
                                {
                                    MessageBox.Show("Không thể chia cho 0", "Thông báo");
                                }
                                ketQua = (b % a);
                                break;
                        }
                        sh.Push(ketQua);
                        if (str1 != "(") 
                            str1 = st.Pop().ToString();
                    }
                    if (manhinh.Text[i] == ')' && (manhinh.Text[i] >= '0' && manhinh.Text[i] <= '9'))
                        st.Push("*");
                    
                }
                
                i++;
            }

            //xử lí các phép toán còn lại
            while (st.Count > 0)
            {
                a = double.Parse(sh.Pop().ToString()); ;
                str1 = st.Pop().ToString();
                if (sh.Count == 0 && (str1 == "+" || str1 == "-")) b = 0;
                else
                    if (sh.Count == 0 && (str1 == "*" || str1 == "/"))
                    {
                        MessageBox.Show("Loi!");
                        manhinh.Text = "0";
                        return;
                    }
                
               if (str1 == "+" || str1 == "-" || str1 == "*" || str1 == "/" || str1 == "%")
                {
                    b = double.Parse((sh.Pop().ToString()));
                    switch (str1)
                    {
                        case "+":
                            ketQua = (a + b);
                            break;
                        case "-":
                            ketQua = (b - a);
                            break;
                        case "*":
                            ketQua = (a * b);
                            break;
                        case "/":
                            if (a == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0", "Thông báo");
                            }
                            ketQua = (b / a);
                            break;
                        case "%":
                            if (a == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0", "Thông báo");
                            }
                            ketQua = (b % a);
                            break;
                        case "^":
                            ketQua = Math.Pow(b, a);
                            break;
                    }
                    sh.Push(ketQua);
                }
                else
                    if (str1 == "sin" || str1 == "cos" || str1 == "tan" || str1 == "cotg" || str1 =="sqrt")
                    {

                        switch (str1)
                        {
                            case "sin":
                                sh.Push(Math.Sin(a));
                                break;
                            case "cos":
                                sh.Push(Math.Cos(a));
                                break;
                            case "tan":
                                sh.Push(Math.Tan(a));
                                break;
                            case "cotg":
                                sh.Push(1.00/Math.Tan(a));
                                break;
                            case "sqrt":
                                sh.Push(Math.Sqrt(a));
                                break;
                        }
                    }
            }
            manhinh.Text = sh.Pop().ToString();
        }

        private void buttonMoNg_Click(object sender, EventArgs e)
        {
            ThemKiTu("(");
        }

        private void buttonDongNg_Click(object sender, EventArgs e)
        {
            ThemKiTu(")");
        }

        private void buttonChialaydu_Click(object sender, EventArgs e)
        {
            manhinh.Text += '%';
            NhapLai = false;
        }

        private void buttonSin_Click(object sender, EventArgs e)
        {
            ThemKiTu("sin");
        }

        private void buttonAC_Click(object sender, EventArgs e)
        {
            manhinh.Text = "0";
            NhapLai = true;
        }

        private void buttonCos_Click(object sender, EventArgs e)
        {
            ThemKiTu("cos");
        }

        private void buttonTan_Click(object sender, EventArgs e)
        {
            ThemKiTu("tan");
        }

        private void buttonCotg_Click(object sender, EventArgs e)
        {
            ThemKiTu("cotg");
        }

        private void buttonSqrt_Click(object sender, EventArgs e)
        {
            ThemKiTu("sqrt");
        }

        private void buttonCham_Click(object sender, EventArgs e)
        {
            ThemKiTu(".");
        }
    }
}
