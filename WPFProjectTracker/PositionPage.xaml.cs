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
using WPFProjectTracker.ViewModels;

namespace WPFProjectTracker
{
    /// <summary>
    /// Interaction logic for PositionPage.xaml
    /// </summary>
    public partial class PositionPage : Window
    {
        public PositionPage()
        {
            InitializeComponent();
        }
        PERSONALTRACKINGContext db = new PERSONALTRACKINGContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = db.Departments.ToList().OrderBy(x => x.DepartmentName);
            cmbDepartment.ItemsSource = list;
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;
            if(model!=null && model.Id != 0)
            {
                cmbDepartment.SelectedValue = model.departmentId;
                txtPositionName.Text = model.positionName;
            }
        }

        public PositionModel model;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDepartment.SelectedIndex == -1 || txtPositionName.Text.Trim() == "")
                MessageBox.Show("Enter all values");
            else
            {
                if(model!=null && model.Id != 0)
                {
                    Position pos = new Position();
                    pos.DepartmentId = (int)cmbDepartment.SelectedValue;
                    pos.Id = model.Id;
                    pos.PositionName = txtPositionName.Text;
                    db.Positions.Update(pos);
                    db.SaveChanges();
                    MessageBox.Show("Updated Sucessfully");
                }
                else
                {
                    Position position = new Position();
                    position.PositionName = txtPositionName.Text;
                    position.DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
                    db.Positions.Add(position);
                    db.SaveChanges();
                    cmbDepartment.SelectedIndex = -1;
                    txtPositionName.Clear();
                    MessageBox.Show("Postion added sucessfully");
                }
                
            }
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
