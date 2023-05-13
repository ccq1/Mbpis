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
    /// ownerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ownerWindow : Window
    {
        int index;
        public ownerWindow()
        {
            InitializeComponent();
            ShowResult(data);
            Init();
        }
        private void ShowResult(DataGrid data)
        {
            using (var a = new Table1Entities())
            {
                var q = from t in a.user
                        
                        select t;
                data.ItemsSource = q.ToList();
            }
        }
        private void Init()
        {
            data.SelectionChanged += (s, e) =>
            {
                LabelTip.Visibility = Visibility.Collapsed;
                StackPanelDetail.Visibility = Visibility.Visible;
                
                user table = (user)data.SelectedItem;
                if (table != null)
                {
                    idbox.Text = "" + table.Id;
                    namebox.Text = table.name;
                    typebox.Text = table.type;
                    numberbox.Text = table.number;
                    passwordbox.Text = table.password;
                    index = table.Id;
                }

               
                
              
            };
            btnAdd.Click += (f, h) =>
            {
                using (var db = new Table1Entities())
                {
                    idbox.Text = "" + (db.user.Count() + 1);
                    namebox.Text = "";
                    typebox.Text = "";
                    numberbox.Text = "";
                    passwordbox.Text = "";
                    wrap.Visibility = Visibility.Visible;
                }

                okbtn.Click += (t, c) =>
                {
                    using (var db = new Table1Entities())
                    {
                        user k = new user
                        {
                            Id = db.user.Count() + 1,
                            name = namebox.Text,
                            type = typebox.Text,
                            number = numberbox.Text,
                            password = passwordbox.Text
                        };
                        db.user.Add(k);
                        db.SaveChanges();
                        ShowResult(data);
                        wrap.Visibility = Visibility.Collapsed;
                        LabelTip.Visibility = Visibility.Visible;
                        StackPanelDetail.Visibility = Visibility.Collapsed;
                    }


                };
                cancelbtn.Click += (t, c) =>
                {

                    wrap.Visibility = Visibility.Collapsed;
                };

            };
            btnModify.Click += (f, h) =>
            {
                wrap1.Visibility = Visibility.Visible;
                okbtn1.Click += (t, c) =>
                {
                    using (var db = new Table1Entities())
                    {
                        int id1 = int.Parse(idbox.Text);
                        var q1 = db.user.FirstOrDefault((y) => y.Id == id1);
                        q1.name = namebox.Text;
                        q1.type = typebox.Text;
                        q1.number = numberbox.Text;
                        q1.password = passwordbox.Text;

                        db.SaveChanges();
                        ShowResult(data);
                        wrap1.Visibility = Visibility.Collapsed;
                        LabelTip.Visibility = Visibility.Visible;
                        StackPanelDetail.Visibility = Visibility.Collapsed;

                    }
                };
                cancelbtn1.Click += (t, c) =>
                {

                    wrap1.Visibility = Visibility.Collapsed;
                };
            };
            btnDelete.Click += (f, h) =>
            {
                wrap2.Visibility = Visibility.Visible;
                okbtn2.Click += (t, c) =>
                {
                    using (var db = new Table1Entities())
                    {
                        
                        var q2 = db.user.FirstOrDefault((y) => y.Id == index);
                        db.user.Remove(q2);
                        db.SaveChanges();

                        ShowResult(data);
                        wrap2.Visibility = Visibility.Collapsed;
                        LabelTip.Visibility = Visibility.Visible;
                        StackPanelDetail.Visibility = Visibility.Collapsed;

                    }
                };
                cancelbtn2.Click += (t, c) =>
                {

                    wrap2.Visibility = Visibility.Collapsed;
                };
            };
        }
        
    }
}
