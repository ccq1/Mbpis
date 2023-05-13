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
using System.Windows.Shapes;

namespace Mpbis
{
    /// <summary>
    /// UserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Window
    {
        string loadname;
        public UserWindow()
        {
            InitializeComponent();
            Click();
        }
        private void Click()
        {
            btn1.Click += (s, e) =>
            {

                mpbisWindow window1= new mpbisWindow();
                window1.getName(loadname);
                window1.Show();
                this.Close();



            };
            btn2.Click += (s, e) =>
            {
                
                bhWindow bh= new bhWindow();
                bh.getName(loadname);
                bh.Show();
                this.Close();
            };
            exitbtn.Click += (s, e) =>
            {
                Application.Current.Shutdown();
            };
            again.Click += (s, e) =>
            {
                new MainWindow().Show();
                this.Close();
            };
        }
        public void getName(string name)
        {
            loadname = name;
        }
    }
}
