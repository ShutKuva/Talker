namespace Core.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public User()
        {

        }

        public User(User original)
        {
            Name = original.Name;
            Surname = original.Surname;
            Age = original.Age;
            Password = original.Password;
            Username = original.Username;
        }
    }
}