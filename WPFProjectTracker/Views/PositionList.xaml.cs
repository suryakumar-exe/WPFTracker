using Microsoft.EntityFrameworkCore;
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
using WPFProjectTracker.ViewModels;

namespace WPFProjectTracker.Views
{
    /// <summary>
    /// Interaction logic for PositionList.xaml
    /// </summary>
    public partial class PositionList : UserControl
    {
        public PositionList()
        {
            InitializeComponent();
        }
        PERSONALTRACKINGContext db = new PERSONALTRACKINGContext();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefereshGrid();
        }
        void RefereshGrid()
        {
            var list = db.Positions.Include(x => x.Department).Select(a => new
            {
                positionId = a.Id,
                positionName = a.PositionName,
                departmentId = a.DepartmentId,
                departmentName = a.Department.DepartmentName
            }).OrderBy(x => x.positionName).ToList();
            List<PositionModel> modellist = new List<PositionModel>();
            foreach (var item in list)
            {
                PositionModel model = new PositionModel();
                model.Id = item.positionId;
                model.positionName = item.positionName;
                model.departmentName = item.departmentName;
                model.departmentId = item.departmentId;
                modellist.Add(model);
            }
            gridDepartment.ItemsSource = modellist;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PositionPage page = new PositionPage();
            page.ShowDialog();
            RefereshGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            PositionModel model = (PositionModel)gridDepartment.SelectedItem;
            if(model!=null && model.Id != 0)
            {
                PositionPage page = new PositionPage();
                page.model = model;
                page.ShowDialog();
                RefereshGrid();
            }

        }
    }
}
