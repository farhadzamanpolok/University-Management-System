using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class LoginWindow : Form
    {
        

        public LoginWindow()
        {
            InitializeComponent();
            
        }

        private void Usernamebox_TextChanged(object sender, EventArgs e)
        {
            Usernamebox.BackColor = Color.White;
            label1.Text = "";
            Usernamebox.SelectAll();
        }

        private void passwordbox_TextChanged(object sender, EventArgs e)
        {
            passwordbox.BackColor = Color.White;
            label1.Text = "";
            passwordbox.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string username = Usernamebox.Text;
            string password = passwordbox.Text;
            string substring = username.Substring(0);
            if (Usernamebox.Text.Equals(""))
            {
                Usernamebox.BackColor = Color.LightCoral;
                label1.Text = @"Username cannot Be Blank";

            }
            else if (passwordbox.Text.Equals(""))
            {
                passwordbox.BackColor = Color.LightCoral;
                label1.Text = @"Password Cannot Be Blank";
            }
            else if (substring.Length == 10 && substring[2] == '-' && substring[8] == '-')
            {
                if (DataAccess.Find_user(username, password, 1) == 1)
                {
                    
                    new Student(username).Show();
                    this.Hide();
                }
                else
                {
                    label1.Text = @"Invalid Username Or Password";
                }
            }
            else if (substring.Length == 10 && substring[3] == '-' && substring[7] == '-')
            {
                if (DataAccess.Find_user(username, password, 2) == 2)
                {
                    Hide();
                    new Faculty(username).Show();
                }
                else
                {
                    label1.Text = @"Invalid Username Or Password";
                }
            }
            else if (substring.Length == 10 && substring[1] == '-' && substring[6] == '-')
            {
                if (DataAccess.Find_user(username, password, 3) == 3)
                {
                    Hide();
                    new Administrator(username).Show();

                }
                else
                {
                    label1.Text = @"Invalid Username Or Password";
                }
            }
            else
            {
                label1.Text = @"Invalid Username Or Password";
            }

        }
    

    private void LoginWindow_Load(object sender, EventArgs e)
        {
           DataAccess.Open();

        }

   
   

    
        
    }
}
