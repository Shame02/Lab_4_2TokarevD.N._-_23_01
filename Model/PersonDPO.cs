using Lab_4_TokarevD.N._БПИ_23_01.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Lab_4_TokarevD.N._БПИ_23_01.Model
{


    public class PersonDpo : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int roleId;
        public int RoleId
        {
            get => roleId;
            set { roleId = value; OnPropertyChanged(); }
        }

        private string roleName;
        public string RoleName
        {
            get => roleName;
            set { roleName = value; OnPropertyChanged(); }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set { firstName = value; OnPropertyChanged(); }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set { lastName = value; OnPropertyChanged(); }
        }

        // сделал string
        private string birthday;
        public string Birthday
        {
            get => birthday;
            set { birthday = value; OnPropertyChanged(); }
        }

        public PersonDpo() { }

        // сделал string для днюхи
        public PersonDpo(int id, int roleId, string roleName, string firstName, string lastName, string birthday)
        {
            Id = id;
            RoleId = roleId;
            RoleName = roleName;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        public PersonDpo ShallowCopy() => (PersonDpo)this.MemberwiseClone();

        public PersonDpo CopyFromPerson(Person person)
        {
            var dpo = new PersonDpo
            {
                Id = person.Id,
                RoleId = person.RoleId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birthday = person.Birthday 
            };

            var roleVm = new RoleViewModel();
            var role = roleVm.ListRole.FirstOrDefault(r => r.Id == person.RoleId);
            dpo.RoleName = role != null ? role.NameRole : "";

            return dpo;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
