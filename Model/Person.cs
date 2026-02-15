using Lab_4_TokarevD.N._БПИ_23_01.Model;
using System.Linq;
public class Person
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Birthday { get; set; } 

    public Person() { }

    public Person(int id, int roleId, string firstName, string lastName, string birthday) // Изменен тип параметра
    {
        Id = id;
        RoleId = roleId;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
    }

    public Person CopyFromPersonDPO(PersonDpo dpo)
    {
        return new Person
        {
            Id = dpo.Id,
            RoleId = dpo.RoleId,
            FirstName = dpo.FirstName,
            LastName = dpo.LastName,
            Birthday = dpo.Birthday 
        };
    }
}