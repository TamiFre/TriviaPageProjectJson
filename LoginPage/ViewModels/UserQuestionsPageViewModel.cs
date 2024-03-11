using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LoginPage.Models;
using LoginPage.Service;

namespace LoginPage.ViewModels
{
    [QueryProperty(nameof(Player), "Player")]
    public class UserQuestionsPageViewModel : ViewModel
    {

        public ObservableCollection<Q> Questions { get; set; }
        public Player Player { get; set; }
        QService qService;
       

        public ICommand FilterCommand { get; set; }
        public ICommand ClearFilterCommand { get; set; }
        public ICommand ShowAllQuestions { get; set; }

        private List<Q> fullList;
        private List<Q> filteredList;
        private StatusQService statusQService;
        private int selectedIndex;
        private StatusQ selectedStatus;
        public int SelectedIndex{ get { return selectedIndex; } set { if (selectedIndex != value) { selectedIndex = value;  OnPropertyChanged(); } } }
        public ObservableCollection<StatusQ> Status { get; set; } 
        public StatusQ SelectedStatus { get=>selectedStatus; set { if (selectedStatus != value) { selectedStatus = value; OnPropertyChanged();  ShowFilteredQuestions();}  } }

        private Q selectedQuestion;
        public Q SelectedQuestion { get { return selectedQuestion; } set { selectedQuestion = value; OnPropertyChanged(); } }
       

        public UserQuestionsPageViewModel(QService qService, StatusQService statusService)
        {
            fullList = new List<Q>();   
            this.qService = qService;//הסרבי
            this.statusQService = statusService;
            Status = new ObservableCollection<StatusQ>();

            foreach(StatusQ s in statusQService.StatusQs)
            {
                Status.Add(s);
            }

            Questions = new ObservableCollection<Q>();
            ShowAllQuestions = new Command(async () => await ShowQuestions());
            FilterCommand = new Command(ShowFilteredQuestions);
            selectedIndex = -1;
          
        }

        public async Task ShowQuestions()
        {
           
            fullList = qService.GetUserQuestion(Player);
            Questions.Clear();
          
            for (int i = 0; i < fullList.Count; i++)
            {
                Questions.Add(fullList[i]);
            }
            SelectedIndex = -1;
        }

        public void ShowFilteredQuestions()
        {
            if(SelectedIndex!=-1)
            filteredList = fullList.Where(x=> x.StatusId==SelectedStatus.StatusId).ToList();
            else
                filteredList = fullList;
            Questions.Clear();
            foreach (Q question in filteredList)
            {
                Questions.Add(question);
            }
          
        }
        

    }
}
