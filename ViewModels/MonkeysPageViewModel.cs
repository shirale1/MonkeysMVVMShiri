using MonkeysMVVM.Models;
using MonkeysMVVM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonkeysMVVM.ViewModels
{
    public class MonkeysPageViewModel : ViewModel
    {
        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
        public Monkey SelectedMonkey {  get; set; } 
        public ObservableCollection<Monkey> Monkeys { get; set; }
        public Monkey Monkey { get; set; }
        public ICommand LoadMonkeysCommand { get; private set; }
        public ICommand NavigateToShowMonkey {  get; private set; }

        public MonkeysPageViewModel()
        {
            Monkeys= new ObservableCollection<Monkey>();

            LoadMonkeysCommand = new Command(async () => await LoadMonkeys());
            NavigateToShowMonkey = new Command(async () => await GoToMonkeyPage());
        }

        private async Task GoToMonkeyPage()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("monkey", SelectedMonkey);
            await AppShell.Current.GoToAsync("ShowMonkeyView",data);
           
        }

        private async Task LoadMonkeys()
        {
            IsRefreshing = true;
            MonkeysService service= new MonkeysService();
            var List= service.GetMonkeys();
            for(int i = 0; i < List.Count; i++)
            {
                Monkeys.Add(List[i]);
            }
            IsRefreshing= false;
        }

    }
}
