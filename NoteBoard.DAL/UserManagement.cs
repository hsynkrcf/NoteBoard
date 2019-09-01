using NoteBoard.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.DAL
{
    public class UserManagement
    {
        Helper h;
        public UserManagement()
        {
            h = new Helper();
        }

        public int Insert(User user)
        {
            string query = "INSERT INTO [User](UserID,RoleID,Firstname,Lastname,EMail,Password,IsActive) VALUES(@userID,@roleID,@name,@surname,@mail,@pass,@active)";
            List<SqlParameter> parameters = GetParameters(user, true);

            h.AddParametersToCommand(parameters);
            return h.MyExecuteQuery(query);
        }

        List<SqlParameter> GetParameters(User user, bool isInsert)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userID", user.UserID));
            if (isInsert)
            {
                parameters.Add(new SqlParameter("@roleID", user.RoleID));
            }
            parameters.Add(new SqlParameter("@name", user.Firstname));
            parameters.Add(new SqlParameter("@surname", user.Lastname));
            parameters.Add(new SqlParameter("@mail", user.EMail));
            parameters.Add(new SqlParameter("@pass", user.Password));
            parameters.Add(new SqlParameter("@active", user.IsActive));
            return parameters;
        }

        public int Update(User user)
        {
            string query = "UPDATE [User] SET Firstname = @name, Lastname = @surname, EMail = @mail, Password = @pass, IsActive = @active WHERE UserID = @userID";
            List<SqlParameter> parameters = GetParameters(user, false);

            h.AddParametersToCommand(parameters);
            return h.MyExecuteQuery(query);
        }

        public int Delete(Guid userID)
        {
            string query = "DELETE FROM [User] WHERE UserID = @userID";
            h.AddParametersToCommand(new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@userID",
                    Value = userID

                }
            });

            return h.MyExecuteQuery(query);
        }

        public User GetUserByID(Guid userID)
        {
            string query = "SELECT * FROM [User] WHERE UserID = @userID";
            h.AddParametersToCommand(new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@userID",
                    Value = userID

                }
            });

            User currentUser = new User();
            SqlDataReader reader = h.MyExecuteReader(query);
            reader.Read();
            currentUser.UserID = userID;
            currentUser.RoleID = (Guid)reader["RoleID"];
            currentUser.Firstname = reader["Firstname"].ToString();
            currentUser.Lastname = reader["Lastname"].ToString();
            currentUser.EMail = reader["Email"].ToString();
            currentUser.Password = reader["Password"].ToString();
            currentUser.IsActive = (bool)reader["IsActive"];
            currentUser.CreatedDate = (DateTime)reader["CreatedDate"];

            reader.Close();

            return currentUser;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM [User]";

            User currentUser;
            SqlDataReader reader = h.MyExecuteReader(query);
            while (reader.Read())
            {
                currentUser = new User();
                currentUser.UserID = (Guid)reader["UserID"];
                currentUser.RoleID = (Guid)reader["RoleID"];
                currentUser.Firstname = reader["Firstname"].ToString();
                currentUser.Lastname = reader["Lastname"].ToString();
                currentUser.EMail = reader["Email"].ToString();
                currentUser.Password = reader["Password"].ToString();
                currentUser.IsActive = (bool)reader["IsActive"];
                currentUser.CreatedDate = (DateTime)reader["CreatedDate"];
                users.Add(currentUser);
            }
            reader.Close();

            return users;
        }

        public UserRole GetUserRoleByID(Guid roleID)
        {
            string query = "SELECT * FROM UserRole WHERE RoleID = @roleID";
            h.AddParametersToCommand(new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@roleID",
                    Value = roleID
                }
            });

            SqlDataReader reader = h.MyExecuteReader(query);
            UserRole role = new UserRole();
            reader.Read();
            role.RoleID = roleID;
            role.Name = reader.GetString(1);
            reader.Close();

            return role;
        }

        public UserRole GetUserRoleByName(string roleName)
        {
            string query = "SELECT * FROM UserRole WHERE Name = @name";
            h.AddParametersToCommand(new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@name",
                    Value = roleName
                }
            });

            SqlDataReader reader = h.MyExecuteReader(query);
            UserRole role = new UserRole();
            reader.Read();
            role.RoleID = reader.GetGuid(0);
            role.Name = roleName;
            reader.Close();

            return role;
        }
    }
}
