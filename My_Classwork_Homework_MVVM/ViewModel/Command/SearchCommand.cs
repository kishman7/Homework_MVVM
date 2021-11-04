using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace My_Classwork_Homework_MVVM.ViewModel.Command
{
    public class SearchCommand : ICommand
    {
        public event EventHandler CanExecuteChanged // чи може команда виконуватись чи не може
        {
            add //привязка до події
            {
                CommandManager.RequerySuggested += value;
            }
            remove //відвязуємося від події
            {
                CommandManager.RequerySuggested -= value;
            }
        } 
        WeatherVM VM { get; set; }
        public SearchCommand(WeatherVM vm)

        {
            VM = vm;
        }
        public bool CanExecute(object parameter) // bool значення, чи дозволяє чи не дозволяє виконувати метод Execute
        {
            return !string.IsNullOrWhiteSpace(parameter as string);
        }

        public async void Execute(object parameter) // реалізовує те, що робить сама команда при натисненні на кнопку
        {
            await VM.MakeRequestCitiesAsync(); 
        }
    }
}
