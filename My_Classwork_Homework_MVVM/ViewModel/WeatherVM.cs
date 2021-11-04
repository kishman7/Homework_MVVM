using My_Classwork_Homework_MVVM.Models;
using My_Classwork_Homework_MVVM.ViewModel.Command;
using My_Classwork_Homework_MVVM.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace My_Classwork_Homework_MVVM.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged  //зв’язка ViewModel
    {
        public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>(); // колекція, яка вже відображає зміни в списку

        public City selectedCity; //властивість, яка буде відображати обране місто

        public CurrentConditions currentConditions; //властивість, яка буде відображати дані для міста

        public SearchCommand SearchCommand { get; set; }

        private string query; // запит того, що ми будемо вводити з клавіатури
        public WeatherVM()
        {
            query = "";
            SearchCommand = new SearchCommand(this);
        }
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnNotify();
            }
        }

        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                SelectedCity = value;
                GetConditionsAsync();
                OnNotify();
            }
        }

        public CurrentConditions CurrentConditions
        {
            get => currentConditions;
            set
            {
                currentConditions = value;
                OnNotify();
            }
        }

        private async void GetConditionsAsync()
        {
            if (SelectedCity != null)
            {
                CurrentConditions = await WheatherHelper.GetCurrentConditionsAsync(SelectedCity.Key);
            }
            else
            {
                CurrentConditions = new CurrentConditions();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public async Task MakeRequestCitiesAsync() // мнтод, який буде заповнювати колекцію ObservableCollection<City> Cities
        {
            var cities = await WheatherHelper.GetCitiesAsync(Query);
            Cities.Clear();

            foreach (var item in cities)
            {
                Cities.Add(item);
            }
        }
    }
}
