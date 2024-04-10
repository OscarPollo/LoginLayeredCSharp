using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Cache;
using DataAccess;

namespace Domain
{
    public class UserModel
    {
        UserDao userDao = new UserDao();

        //Atributos
        private int idUser;
        private string loginName;
        private string password;
        private string firstName;
        private string lastName;
        private string position;
        private string email;

        public UserModel(int idUser, string loginName, string password, string firstName, string lastName, string position, string email)
        {
            this.idUser = idUser;
            this.loginName = loginName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = position;
            this.email = email;
        }
        public UserModel() { }

        public string nombreConexionn() 
        {
            return userDao.nombreServidor();
        }
        public string editUserProfile() 
        {
            try 
            {
                userDao.editProfile(idUser, loginName, password, firstName, lastName, email);
                LoginUser(loginName, password);
                return "Tu perfil se ha actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Nombre de usuario existente, usa otro";
            }
        }
        public string recoverPassword(string userRequesting)
        {
            return userDao.recoverPassword(userRequesting);
        }
        public bool LoginUser(string user, string pass)
        {
            return userDao.Login(user, pass);
        }

        public void AnyMethod()
        {
            //Seguridad y Permisos
            if (UserLoginCache.Position == Positions.Administrador)
            {
                //Codigo
            }
            if (UserLoginCache.Position == Positions.Ingeniero)
            {

            }
            if (UserLoginCache.Position == Positions.Interno)
            {

            }
        }
    }
}
