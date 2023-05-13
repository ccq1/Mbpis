using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mpbis
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string loadname="张三";
       
        public MainWindow()
        {
            
            InitializeComponent();
            Main();
            
        }
        public void Main()
        {
          
            okbtn.Click += (s, e) =>
            {
                using(var db=new Table1Entities())
                {
                    
                    string type = (from t in db.user
                                  where t.name == textbox.Text
                                  select t.password).FirstOrDefault();
                    if (comboBox.SelectedItem==user)
                    {
                        string pwd = (from t in db.user
                                      where t.name == textbox.Text && t.type=="user"
                                      select t.password).FirstOrDefault();
                        if (pwd == passWord.Password)
                        {

                            loadname = textbox.Text;
                            UserWindow window1 = new UserWindow();
                            window1.getName(loadname);
                            window1.Show();

                            
                            


                            this.Close();
                            
                        }
                        else
                        {
                            MessageBox.Show("密码或账号错误！");
                        }
                    }
                    else
                    {
                        string pwd = (from t in db.user
                                      where t.name == textbox.Text && t.type == "owner"
                                      select t.password).FirstOrDefault();
                        if (pwd == passWord.Password)
                        {
                            new ownerWindow().Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("密码或账号错误！");
                        }

                    }
                }
            };
            cancelbtn.Click += (s, e) =>
            {
                Application.Current.Shutdown();
            };




        }
       
    }
}
