﻿using MonkeysMVVM.Models;
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
        MonkeysService service;
        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
        public Monkey SelectedMonkey {  get; set; } 
        public ObservableCollection<Monkey> Monkeys { get; set; }
        public Monkey Monkey { get; set; }
        public ICommand LoadMonkeysCommand { get; private set; }
        public ICommand NavigateToShowMonkey {  get; private set; }

        public MonkeysPageViewModel(MonkeysService s)
        {
            Monkeys = new ObservableCollection<Monkey>();

            LoadMonkeysCommand = new Command(async () => await LoadMonkeys());
            NavigateToShowMonkey = new Command(async () => await GoToMonkeyPage());
            this.service = s;
        }

        private async Task GoToMonkeyPage()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("monkey", SelectedMonkey);
            await AppShell.Current.GoToAsync($"ShowMonkey?title=פרטי הקוף {SelectedMonkey.Name}",data);
            SelectedMonkey= null; 
           
        }

        private async Task LoadMonkeys()
        {
            IsRefreshing = true;
            
            var List= service.GetMonkeys();
            for(int i = 0; i < List.Count; i++)
            {
                Monkeys.Add(List[i]);
            }
            IsRefreshing= false;
        }

    }
}
