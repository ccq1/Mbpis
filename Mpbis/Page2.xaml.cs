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
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            Test();
        }
        private void Test()
        {
            using (var db = new Table1Entities())
            {
                var q = from t in db.bussiness
                        select t.name;
                listBox.ItemsSource = q.ToList();
            }
            listBox.SelectionChanged += (s, e) =>
            {
                var idex = (s as ListBox).SelectedIndex;
                using (var db = new Table1Entities())
                {

                    string q = (from t in db.bussiness

                                where t.Id == 1 + idex
                                select t.details).FirstOrDefault();


                    listBox.Visibility = Visibility.Collapsed;
                    dockPanel.Visibility = Visibility.Visible;
                    backbtn1.Visibility = Visibility.Collapsed;
                    label.Content = q;

                }
                    btn.Click += (t, f) =>
                    {
                        using (var db = new Table1Entities())
                        {
                            DateTime now = DateTime.Now;
                            int year1 = now.Year;
                            int month1 = now.Month;
                            int day1 = now.Day;

                            int count = db.db01.Count()+1;

                            string q1 = (from t1 in db.bussiness

                                         where t1.Id == 1 + idex
                                         select t1.name).FirstOrDefault();
                            string cost1 = (from t2 in db.bussiness

                                            where t2.Id == 1 + idex
                                            select t2.cost).FirstOrDefault();
                            db01 mytable = new db01
                            {
                                year = year1,
                                Id = count,
                                month = month1,
                                name = q1,
                                cost = cost1,
                                Odate = year1 + "-" + month1 + "-" + day1




                            };
                            db.db01.Add(mytable);
                            db.SaveChanges();
                            
                        }
                    };
                    backbtn.Click += (t, f) =>
                    {
                        listBox.Visibility = Visibility.Visible;
                        dockPanel.Visibility = Visibility.Collapsed;
                        backbtn1.Visibility = Visibility.Visible;
                    };
               

                
            };
            backbtn1.Click += (t, f) =>
            {
               
                 

            };
        }
    }
}