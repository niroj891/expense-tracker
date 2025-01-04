using System.Text.Json;

using DotnetCoursework.Model;

namespace ExpenseTrackerApp.Services
{
    public class UserService
    {
        private List<User> _users;

        public UserService()
        {
            LoadUsersFromJson();
        }

        private void LoadUsersFromJson()
        {
            string jsonFilePath = Path.Combine(FileSystem.AppDataDirectory, "users.json"); // Changed to users.json
            
            if (File.Exists(jsonFilePath))
            {
                var jsonData = File.ReadAllText(jsonFilePath);
                _users = JsonSerializer.Deserialize<List<User>>(jsonData) ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
            }
        }

        public bool Login(string username, string password)
        {
            return _users.Any(user => user.Username == username && user.Password == password);
        }

        public List<Expense> GetUserExpenses(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            return user?.Expenses ?? new List<Expense>();
        }

        public List<Expense> GetExpensesByTag(string username, string tag)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            return user?.Expenses.Where(e => e.ExpenseTag == tag).ToList() ?? new List<Expense>();
        }

        // Assuming you have a UserData class if you want to store Users in JSON.
        // You can remove this if unnecessary
        private class UserData
        {
            public List<User> Users { get; set; }
        }
    }
}