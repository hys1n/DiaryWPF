using Diary.Models;
using DiaryWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.ViewModels
{
    public class InboxViewModel
    {
        private readonly ObservableCollection<DiaryTask> userTasks = UserManager.CurrentUser.Tasks;

        public ObservableCollection<DiaryTask> FilteredTasks { get { return DiaryTask.FilterTasks(userTasks); } }
    }
}
