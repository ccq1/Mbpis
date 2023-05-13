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
    /// bhWindow.xaml 的交互逻辑
    /// </summary>
    public partial class bhWindow : Window
    {
        string kind;
        string loadname;
        string exist;
        int idex;
        public bhWindow()
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
                 idex = (s as ListBox).SelectedIndex;
                using (var db = new Table1Entities())
                {

                    string q = (from t in db.bussiness

                                where t.Id == 1 + idex
                                select t.details).FirstOrDefault();
                    kind = (from t in db.bussiness

                                where t.Id == 1 + idex
                                select t.kind).FirstOrDefault();
                    

                    listBox.Visibility = Visibility.Collapsed;
                    dockPanel.Visibility = Visibility.Visible;
                    backbtn1.Visibility = Visibility.Collapsed;
                    label.Content = q;

                }
               


            };
            btn1.Click += (t, f) =>
            {
                var result = MessageBox.Show("是否确定购买？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new Table1Entities())
                    {


                        DateTime now = DateTime.Now;
                        int year1 = now.Year;
                        int month1 = now.Month;
                        int day1 = now.Day;

                        int count = db.db01.Count() + 1;
                        exist = (from t1 in db.bussiness
                                 from t2 in db.db01
                                 where t1.Id == 1 + idex && t2.name == t1.name && t2.year == year1 && t2.month == month1
                                 select t1.name).FirstOrDefault();
                        string q1 = (from t1 in db.bussiness

                                     where t1.Id == 1 + idex
                                     select t1.name).FirstOrDefault();
                        string cost1 = (from t2 in db.bussiness

                                        where t2.Id == 1 + idex
                                        select t2.cost
                                       ).FirstOrDefault();
                        if (kind.Contains("a"))
                        {
                            if (exist != q1)
                            {
                                var q3 = db.db01.FirstOrDefault((t1) => t1.month == month1 && t1.year == year1);
                                q3.name = q1;
                                q3.Odate = year1 + "-" + month1 + "-" + day1;
                                q3.cost = cost1;
                                db.SaveChanges();
                                MessageBox.Show("购买成功(a)！");
                            }
                            else
                            {
                                MessageBox.Show("当前使用的正是该套餐！");
                            }
                        }

                        else if (kind.Contains("b"))
                        {
                            if (exist != q1)
                            {
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
                                MessageBox.Show("购买成功(b)！");
                            }
                            else
                            {
                                MessageBox.Show("该业务当前已存在，无法再次办理！");
                            }
                        }
                        else
                        {
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
                            MessageBox.Show("购买成功(c)！");
                        }






                    }


                }
            };
            backbtn.Click += (t, f) =>
            {
                listBox.Visibility = Visibility.Visible;
                dockPanel.Visibility = Visibility.Collapsed;
                backbtn1.Visibility = Visibility.Visible;
            };

            backbtn1.Click += (t, f) =>
            {
               UserWindow user1= new UserWindow();
                user1.getName(loadname);
                user1.Show();
                this.Close();


            };
        }
        public void getName(string name)
        {
            loadname = name;
        }
    }
}
