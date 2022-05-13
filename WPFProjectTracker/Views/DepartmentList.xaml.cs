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
using WPFProjectTracker.DB;

namespace WPFProjectTracker.Views
{
    /// <summary>
    /// Interaction logic for DepartmentList.xaml
    /// </summary>
    public partial class DepartmentList : UserControl
    {
        public DepartmentList()
        {
            InitializeComponent();
            using (PERSONALTRACKINGContext db = new PERSONALTRACKINGContext())
            {
                List<Department> list = db.Departments.ToList();
                gridDepartment.ItemsSource = list;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DepartmentPage page = new DepartmentPage();
            page.ShowDialog();
            using (PERSONALTRACKINGContext db = new PERSONALTRACKINGContext())
            {
                List<Department> list = db.Departments.ToList();
                gridDepartment.ItemsSource = list;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Department dpt = (Department)gridDepartment.SelectedItem;
            DepartmentPage page = new DepartmentPage();
            page.department = dpt;
            page.ShowDialog();
            using (PERSONALTRACKINGContext db = new PERSONALTRACKINGContext())
            {
                gridDepartment.ItemsSource = db.Departments.OrderBy(x => x.DepartmentName).ToList();
            }
        }
    }
}
