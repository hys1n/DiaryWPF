using System.Windows;

namespace DiaryWPF.Models
{
    public class UpdateViewData
    {
        // Method which updates data displaying if a user changes task or changes date in calendar
        public static void LoadData()
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            if (main != null)
            {
                main.LoadDays();
                main.FilterTasks();
            }
        }
    }
}
