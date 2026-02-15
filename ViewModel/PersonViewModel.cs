using Lab_4_TokarevD.N._БПИ_23_01.Helper;
using Lab_4_TokarevD.N._БПИ_23_01.Model;
using Lab_4_TokarevD.N._БПИ_23_01.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Newtonsoft.Json;


namespace Lab_4_TokarevD.N._БПИ_23_01.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> ListPerson { get; set; }
        public ObservableCollection<PersonDpo> ListPersonDpo { get; set; }

        private PersonDpo selectedPersonDpo;
        public PersonDpo SelectedPersonDpo
        {
            get { return selectedPersonDpo; }
            set
            {
                selectedPersonDpo = value;
                OnPropertyChanged();
            }
        }

        readonly string path = @"C:\Users\dima0\Source\Repos\Lab_4_TokarevD.N._-_23_01\DataModels\PersonData.json";
        string _jsonPersons = String.Empty;
        public string Error { get; set; }

        public PersonViewModel()
        {
            try
            {
                
                ListPerson = LoadPerson();

                
                if (ListPerson == null)
                {
                    ListPerson = new ObservableCollection<Person>();
                }

                
                ListPersonDpo = GetListPersonDpo();
            }
            catch (Exception)
            {
                
                ListPerson = new ObservableCollection<Person>();
                ListPersonDpo = new ObservableCollection<PersonDpo>();
            }
        }


        


        public ObservableCollection<PersonDpo> GetListPersonDpo()
        {
            ListPersonDpo = new ObservableCollection<PersonDpo>();
            foreach (var p in ListPerson)
            {
                ListPersonDpo.Add(new PersonDpo().CopyFromPerson(p));
            }
            return ListPersonDpo;
        }

        public int MaxId()
        {
            if (ListPersonDpo.Count == 0)
                return 0;
            return ListPersonDpo.Max(x => x.Id);
        }

        
        public ObservableCollection<Person> LoadPerson()
        {
            _jsonPersons = File.ReadAllText(path);
            if (_jsonPersons != null)
            {
                ListPerson = JsonConvert.DeserializeObject<ObservableCollection<Person>>(_jsonPersons);
                return ListPerson;
            }
            else
            {
                return null;
            }
        }


      
        private void SaveChanges(ObservableCollection<Person> listPersons)
        {
            var jsonPerson = JsonConvert.SerializeObject(listPersons);
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(jsonPerson);
                }
            }
            catch (IOException e)
            {
                Error = "Ошибка записи json файла /n" + e.Message;
            }
        }

        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                if (addPerson == null)
                {
                    addPerson = new RelayCommand(obj =>
                    {
                        WindowNewEmployee wnPerson = new WindowNewEmployee
                        {
                            Title = "Добавление сотрудника"
                        };

                        PersonDpo per = new PersonDpo
                        {
                            Id = MaxId() + 1
                        };
                        wnPerson.DataContext = per;

                        if (wnPerson.ShowDialog() == true)
                        {
                            Role r = (Role)wnPerson.CbRole.SelectedItem;
                            per.RoleId = r.Id;
                            per.RoleName = r.NameRole;
                            ListPersonDpo.Add(per);

                            Person p = new Person();
                            p = p.CopyFromPersonDPO(per); 
                            ListPerson.Add(p);

                            try
                            {
                                SaveChanges(ListPerson);
                                MessageBox.Show("Сотрудник добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка добавления данных в json файл\n" + e.Message;
                                MessageBox.Show(Error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    },
                    obj => true);
                }
                return addPerson;
            }
        }

        private RelayCommand editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                if (editPerson == null)
                {
                    editPerson = new RelayCommand(obj =>
                    {
                        WindowNewEmployee wnPerson = new WindowNewEmployee
                        {
                            Title = "Редактирование сотрудника"
                        };

                        PersonDpo tempPerson = SelectedPersonDpo.ShallowCopy();
                        wnPerson.DataContext = tempPerson;

                        if (wnPerson.ShowDialog() == true)
                        {
                            Role r = (Role)wnPerson.CbRole.SelectedItem;
                            SelectedPersonDpo.RoleId = r.Id;
                            SelectedPersonDpo.RoleName = r.NameRole;
                            SelectedPersonDpo.FirstName = tempPerson.FirstName;
                            SelectedPersonDpo.LastName = tempPerson.LastName;
                            SelectedPersonDpo.Birthday = tempPerson.Birthday;

                            
                            Person p = ListPerson.FirstOrDefault(x => x.Id == SelectedPersonDpo.Id);
                            if (p != null)
                            {
                                
                                p.RoleId = SelectedPersonDpo.RoleId;
                                p.FirstName = SelectedPersonDpo.FirstName;
                                p.LastName = SelectedPersonDpo.LastName;
                                p.Birthday = SelectedPersonDpo.Birthday;
                            }

                            
                            try
                            {
                                SaveChanges(ListPerson);
                                MessageBox.Show("Изменения сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка редактирования данных в json файл\n" + e.Message;
                                MessageBox.Show(Error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    },
                    obj => SelectedPersonDpo != null && ListPersonDpo.Count > 0);
                }
                return editPerson;
            }
        }

        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                if (deletePerson == null)
                {
                    deletePerson = new RelayCommand(obj =>
                    {
                        PersonDpo person = SelectedPersonDpo;
                        if (person == null) return;

                        MessageBoxResult result = MessageBox.Show(
                            "Удалить данные по сотруднику:\n" + person.LastName + " " + person.FirstName,
                            "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.OK)
                        {
                            ListPersonDpo.Remove(person);

                            Person existing = ListPerson.FirstOrDefault(p => p.Id == person.Id);
                            if (existing != null)
                            {
                                ListPerson.Remove(existing);

                                
                                try
                                {
                                    SaveChanges(ListPerson);
                                }
                                catch (Exception e)
                                {
                                    Error = "Ошибка удаления данных\n" + e.Message;
                                    MessageBox.Show(Error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    },
                    obj => SelectedPersonDpo != null && ListPersonDpo.Count > 0);
                }
                return deletePerson;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

