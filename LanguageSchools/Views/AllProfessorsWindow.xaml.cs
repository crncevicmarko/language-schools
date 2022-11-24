using LanguageSchools.Models;
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

namespace LanguageSchools.Views
{
    /// <summary>
    /// Interaction logic for AllProfessorsPage.xaml
    /// </summary>
    public partial class AllProfessorsWindow : Window
    {
        public AllProfessorsWindow()
        {
            InitializeComponent();
            List<User> users = Data.Instance.ProfessorService.GetAll().Select(p => p.User).ToList();
            dgProfessors.ItemsSource = users;
            
        }

        private void miAddProfessor_Click(object sender, RoutedEventArgs e)
        {
            var professorWindow = new AddProfessorWindow();
            var successful = professorWindow.ShowDialog();

            if ((bool)successful)
            {
                dgProfessors.ItemsSource = Data.Instance.ProfessorService.GetAll().Select(p => p.User).ToList();
            }
        }

        private void miDeleteProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selctedItem = ((User)dgProfessors.SelectedItem).Email;
            if(selctedItem != null)
            {
                MessageBoxResult ms = MessageBox.Show("Da li ste sigurni da zelite daobrisete profeesor", "", MessageBoxButton.YesNo);
                {
                    if(ms == MessageBoxResult.Yes)
                    {
                        Data.Instance.ProfessorService.Delete(selctedItem);
                        List<User> users = Data.Instance.ProfessorService.GetAll().Select(p => p.User).ToList();
                        dgProfessors.ItemsSource = users;
                    }
                }
            }
            
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dgProfessors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = txtSearch.Text;
            Data.Instance.ProfessorService.Search(value);
        }
    }
}
