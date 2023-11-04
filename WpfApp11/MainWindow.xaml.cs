using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp11
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private string email;
        private int age;
        private string gender;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(AllInformation));
                }
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged(nameof(Surname));
                    OnPropertyChanged(nameof(AllInformation));
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                    OnPropertyChanged(nameof(AllInformation));
                }
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(AllInformation));
                }
            }
        }

        private string otherGender;
        private bool isOtherGender;

        public string Gender
        {
            get => gender;
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged(nameof(Gender));
                    OnPropertyChanged(nameof(AllInformation));
                    IsOtherGender = gender == "Other";
                }
            }
        }

        public string OtherGender
        {
            get => otherGender;
            set
            {
                if (otherGender != value)
                {
                    otherGender = value;
                    OnPropertyChanged(nameof(OtherGender));
                    OnPropertyChanged(nameof(AllInformation));
                }
            }
        }

        public bool IsOtherGender
        {
            get => isOtherGender;
            set
            {
                if (isOtherGender != value)
                {
                    isOtherGender = value;
                    OnPropertyChanged(nameof(IsOtherGender));
                    if (!isOtherGender)
                    {
                        OtherGender = string.Empty;
                    }
                }
            }
        }

        public string AllInformation
        {
            get
            {
                return $"Name: {Name}\nSurname: {Surname}\nEmail: {Email}\nAge: {Age}\nGender: {(IsOtherGender ? OtherGender : Gender)}";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString() == "PreferNotToSay" || parameter.ToString() == "Other")
            {
                return value?.ToString() == parameter.ToString();
            }
            return value?.ToString() == (string)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value && (parameter.ToString() == "PreferNotToSay" || parameter.ToString() == "Other"))
            {
                return parameter.ToString();
            }
            return (bool)value ? parameter.ToString() : Binding.DoNothing;
        }
    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ProfileViewModel();
        }
    }
}
