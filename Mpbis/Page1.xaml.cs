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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            int number = 0;
            InitializeComponent();
            Loaded += delegate
            {
                int Cost = 0;
                using (var db = new Table1Entities())
                {

                    var q = from t in db.db01
                            where t.year == 2021 && t.month == 12
                            select t.name;
                    listBox.ItemsSource = q.ToList();

                    number = (from t1 in db.db01
                              from t2 in db.bussiness
                              where t1.name == t2.name && t2.kind == "a" && t1.year == 2021 && t1.month == 12
                              select t1.Id).FirstOrDefault();


                    for (int i = number; i < number + q.Count(); i++)
                    {
                        string q1 = (from t in db.db01
                                 where t.Id == i
                                 select t.cost).FirstOrDefault();
                        Cost += int.Parse(q1);
                    }
                    costLabel.Content = Cost;
                    for (int j = 2; j <= 12; j++)
                    {
                        var q1 = db.db01.FirstOrDefault((t) => t.month == j - 1);
                        var q2 = db.db01.FirstOrDefault((t) => t.month == j);
                        number = (from t1 in db.db01
                                  from t2 in db.bussiness
                                  where t1.name == t2.name && t2.kind == "a" && t1.year == 2021 && t1.month == j - 1
                                  select t1.Id).FirstOrDefault();

                        Cost = 0;
                        for (int i = number; i < number + q.Count(); i++)
                        {
                            var q3 = from t in db.db01
                                     where t.Id == i
                                     select t.cost;
                            Cost += int.Parse(q3.FirstOrDefault());
                        }
                        q2.balance = q1.balance - Cost;
                        db.SaveChanges();

                    }

                    var c = db.db01.FirstOrDefault((t) => t.month == 12);
                    balanceLabel.Content = c.balance;
                }


                listBox.SelectionChanged += (s, e) =>
                {
                    var idex = (s as ListBox).SelectedIndex;
                    using (var db = new Table1Entities())
                    {

                        string q = (from t in db.bussiness
                                    from t1 in db.db01
                                    where t1.Id == number + idex && t.name == t1.name
                                    select t.details).FirstOrDefault();

                        listBox.Visibility = Visibility.Collapsed;
                        label.Visibility = Visibility.Visible;
                        label.Content = q;

                    }
                };

            };
            btnClick();
        }
        private void btnClick()
        {
            btn2101.Click += (s, e) =>
            {
                show(1, 2021);
            };
            btn2102.Click += (s, e) =>
            {
                show(2, 2021);
            };
            btn2103.Click += (s, e) =>
            {
                show(3, 2021);
            };
            btn2104.Click += (s, e) =>
            {
                show(4, 2021);
            };
            btn2105.Click += (s, e) =>
            {
                show(5, 2021);
            };
            btn2106.Click += (s, e) =>
            {
                show(5, 2021);
            };
            btn2107.Click += (s, e) =>
            {
                show(7, 2021);
            };
            btn2108.Click += (s, e) =>
            {
                show(8, 2021);
            };
            btn2109.Click += (s, e) =>
            {
                show(9, 2021);
            };
            btn2110.Click += (s, e) =>
            {
                show(10, 2021);
            };
            btn2111.Click += (s, e) =>
            {
                show(11, 2021);
            };
            btn2112.Click += (s, e) =>
            {
                show(12, 2021);
            };

        }
        private void show(int month, int year)
        {
            using (var db = new Table1Entities())
            {
                int number = 0, Cost = 0;
                var q = from t in db.db01
                        where t.month == month && t.year == year
                        select t.name;
                listBox.ItemsSource = q.ToList();
                number = (from t1 in db.db01
                          from t2 in db.bussiness
                          where t1.name == t2.name && t2.kind == "a" && t1.year == year && t1.month == month
                          select t1.Id).FirstOrDefault();


                for (int i = number; i < number + q.Count(); i++)
                {
                    var q1 = from t in db.db01
                             where t.Id == i
                             select t.cost;
                    Cost += int.Parse(q1.FirstOrDefault());
                }
                costLabel.Content = Cost;
                var q2 = db.db01.FirstOrDefault((t) => t.month == month);
                balanceLabel.Content = q2.balance;
                listBox.Visibility = Visibility.Visible;
                label.Visibility = Visibility.Collapsed;
            }
        }
    }
}
