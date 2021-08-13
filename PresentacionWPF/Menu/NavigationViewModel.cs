using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Vista
{
     class NavigationViewModel: INotifyPropertyChanged
    {
        public int cont1 = 0;

        public int cont2 = 0;

        public ICommand BusinessInteligenceCommand1 { get; set; }

        public ICommand BusinessInteligenceCommand2 { get; set; }


        private object selectedViewModel1;

        private object selectedViewModel2;

        public object SelectedViewModel1
        {
            get { return selectedViewModel1; }
            set { selectedViewModel1 = value; OnPropertyChanged("SelectedViewModel1"); }
        }

        public object SelectedViewModel2
        {
            get { return selectedViewModel2; }
            set { selectedViewModel2 = value; OnPropertyChanged("SelectedViewModel2"); }
        }
        private object selectedTitleBusinessInteligence1;

        private object selectedTitleBusinessInteligence2;

        public object SelectedTitleBusinessInteligence1
        {
            get { return selectedTitleBusinessInteligence1; }
            set { selectedTitleBusinessInteligence1 = value; OnPropertyChanged("SelectedTitleBusinessInteligence1"); }
        }

        public object SelectedTitleBusinessInteligence2
        {
            get { return selectedTitleBusinessInteligence2; }
            set { selectedTitleBusinessInteligence2 = value; OnPropertyChanged("SelectedTitleBusinessInteligence2"); }
        }


        public NavigationViewModel()
        {
            BusinessInteligenceCommand1 = new BaseCommand(OpenEmp);

            SelectedViewModel1 = new PieChartExample();

            SelectedTitleBusinessInteligence1 = "PieChartExample";

            BusinessInteligenceCommand2 = new BaseCommand(OpenEmp1);

            SelectedViewModel2 = new SolidColorExample();

            SelectedTitleBusinessInteligence2 = "SolidColorExample";

        }

        private void OpenEmp1(object obj)
        {
            if (cont2 == 0)
            {
                SelectedViewModel2 = new PointShapeLineExample();

                SelectedTitleBusinessInteligence2 = "PointShapeLineExample";


            }
            else if (cont2 == 1)
            {
                SelectedViewModel2 = new SolidColorExample();

                SelectedTitleBusinessInteligence2 = "SolidColorExample";

            }

            if (cont2 == 1)
            {
                cont2 = 0;
            }
            else
            {
                cont2 += 1;
            }


        }
        private void OpenEmp(object obj)
        {
            if (cont1 == 0)
            {
                SelectedViewModel1 = new GeoMapExample();

                SelectedTitleBusinessInteligence1 = "GeoMapExample";


            }
            else if (cont1 == 1)
            {
                SelectedViewModel1 = new PieChartExample();

                SelectedTitleBusinessInteligence1 = "PieChartExample";

            }             

            if (cont1 == 1)
            {
                cont1 = 0;
            }
            else
            {
                cont1 += 1;
            }

           
        }
        private void OpenDept(object obj)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }

    public class BaseCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _method;
        public event EventHandler CanExecuteChanged;

        public BaseCommand(Action<object> method)
            : this(method, null)
        {
        }

        public BaseCommand(Action<object> method, Predicate<object> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _method.Invoke(parameter);
        }
    }
}

