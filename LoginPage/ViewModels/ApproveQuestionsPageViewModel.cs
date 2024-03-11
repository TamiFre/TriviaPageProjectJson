using LoginPage.Models;
using LoginPage.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoginPage.ViewModels
{
    public class ApproveQuestionsPageViewModel : ViewModel
    {
        QService service;
        SubjectQService subjectService;
        private Q selectedQuestion;
        public Q SelectedQuestion {  get { return selectedQuestion; } set { selectedQuestion = value; OnPropertyChanged(); } }
        private List<Q> fullList;
        private List<SubjectQ> subjects;
        public List<SubjectQ> Subjects { get => subjects; set {  subjects = value; OnPropertyChanged(); } }
        private SubjectQ selectedSubject;
        public SubjectQ SelectedSubject { get => selectedSubject; set { selectedSubject = value; OnPropertyChanged(); } }
        public ICommand FilterCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }
        private List<Q> Questions { get; set; }
        public ObservableCollection<Q> PenQuestions { get; set; }
        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
        public ICommand LoadQuestionsCommand { get; private set; }
        public ICommand ApproveCommand { get; private set; }
        public ICommand DeclineCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }


        public ApproveQuestionsPageViewModel(QService qService, SubjectQService sService)
        {
            fullList = new List<Q>();
            Subjects = new();
            Questions = new List<Q>();
            FilterCommand = new Command(async () => await Filter());
            ClearFilterCommand = new Command(async () => {; await LoadQuestions(); });
            IsRefreshing = false;
            this.service = qService;
            this.subjectService = sService;
            RefreshCommand = new Command(async () => await LoadQuestions());
            var list=service.GetQsWherePending();
            PenQuestions = new();
            for (int i = 0;i<list.Count; i++)
            {
                PenQuestions.Add(list[i]);
            }
            ApproveCommand = new Command<Q>((q) => ChangeStatusToApprove(q));
            DeclineCommand = new Command<Q>((q) => ChangeStatusToDecline(q));
            foreach (SubjectQ s in subjectService.SubjectQs)
            {
                Subjects.Add(s);
            }
            LoadQuestions();
        }

        private async Task LoadQuestions()
        {
            SelectedSubject = null;
            fullList = service.Qs;
            Questions.Clear();
            foreach (var quest in fullList)
                Questions.Add(quest);
            IsRefreshing = true;
            PenQuestions.Clear();
            foreach (Q question in service.Qs.Where(x => x.StatusId == 3))
            {
                PenQuestions.Add(question);
            }
            IsRefreshing = false;
        }
        public async Task Filter()
        {
            PenQuestions.Clear();
            foreach (Q q in service.Qs.Where(x => x.StatusId == 3 && x.SubjectId == SelectedSubject.SubjectId))
            {
                PenQuestions.Add(q);
            }
        }


        public void ChangeStatusToApprove(Q q)
        {
           q.StatusId = 1;
           
           PenQuestions.Remove(q);
           
        }
        public void ChangeStatusToDecline(Q q)
        {
            q.StatusId = 2;
            
            PenQuestions.Remove(q);
           
        }

    }

  }
