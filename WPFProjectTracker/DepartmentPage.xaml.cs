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
using WPFProjectTracker.DB;

namespace WPFProjectTracker
{
    /// <summary>
    /// Interaction logic for DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Window
    {
        public DepartmentPage()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public Department department;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtDepartmentName.Text.Trim() == "")
                MessageBox.Show("Please Enter Department Name");
            else
            {
                using (PERSONALTRACKINGContext db = new PERSONALTRACKINGContext())
                {
                    if (department!=null && department.Id != 0)
                    {
                        Department update = new Department();
                        update.DepartmentName = txtDepartmentName.Text;
                        update.Id = department.Id;
                        db.Departments.Update(update);
                        db.SaveChanges();
                        txtDepartmentName.Clear();
                        MessageBox.Show("Updated Successfully");
                    }
                    else
                    {
                        Department department = new Department();
                        department.DepartmentName = txtDepartmentName.Text;
                        db.Departments.Add(department);
                        db.SaveChanges();
                        txtDepartmentName.Clear();
                        MessageBox.Show("Added Sucessfully");
                    }
                    
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (department != null && department.Id != 0)
            {
                txtDepartmentName.Text = department.DepartmentName;
            }
        }
    }
}
