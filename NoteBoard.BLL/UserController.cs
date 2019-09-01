using NoteBoard.CustomException;
using NoteBoard.DAL;
using NoteBoard.DTO;
using NoteBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.BLL
{
    public class UserController
    {
        UserManagement _userManagement;
        public UserController()
        {
            _userManagement = new UserManagement();
        }

        public bool Add(User user)
        {
            CheckFields(user);
            CheckMail(user.EMail);
            CheckPassword(user.Password);

            user.RoleID = _userManagement.GetUserRoleByName("Standart").RoleID;
            user.UserID = Guid.NewGuid();
            int result = _userManagement.Insert(user);
            return result > 0;
        }

        void CheckMail(string mail)
        {
            try
            {
                System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(mail);
            }
            catch
            {
                throw new MailFormatException();   
            }

            List<User> users = _userManagement.GetAllUsers();
            foreach (User item in users)
            {
                if (item.EMail == mail)
                {
                    throw new MailException();
                }
            }
        }

        void CheckFields(User user)
        {
            if (string.IsNullOrWhiteSpace(user.EMail))
            {
                throw new NotNullException("EMail");
            }
            else if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new NotNullException("Şifre");
            }
        }

        void CheckPassword(string password)
        {
            if (password.Length < 6)
            {
                throw new PasswordLengthException();
            }

            int letterCount = 0;
            foreach (char item in password)
            {
                if (char.IsLetter(item))
                {
                    letterCount++;
                }
            }

            if (letterCount < 3)
            {
                throw new ThreeLetterException();
            }
        }

        public bool Update(User user)
        {
            CheckPassword(user.Password);

            int result = _userManagement.Update(user);
            return result > 0;
        }

        public bool Delete(User user)
        {
            int result = _userManagement.Delete(user.UserID);
            return result > 0;
        }

        public User GetUser(Guid userID)
        {
            return _userManagement.GetUserByID(userID);
        }

        public List<User> GetUsers()
        {
            return _userManagement.GetAllUsers();
        }

        public string Login(LoginDTO login)
        {
            List<User> users = _userManagement.GetAllUsers();
            foreach (User item in users)
            {
                if (item.EMail == login.Mail)
                {
                    if (item.Password == login.Password)
                    {
                        return item.UserID.ToString();
                    }
                    else
                    {
                        return "Şifre yanlış";
                    }
                }
            }

            return "Mail yanlış";
        }
    }
}
