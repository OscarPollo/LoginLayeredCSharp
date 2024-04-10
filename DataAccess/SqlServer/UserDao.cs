using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common.Cache;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace DataAccess
{
    public class UserDao:ConnectionToSql
    {
        public void editProfile(int id,string userName,string password,string name,string lastName,string mail) 
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using (var commnand = new SqlCommand())
                {
                    commnand.Connection = connection;
                    commnand.CommandText = "update Users set" +
                        "loginName=@userName,Password=@pass,FirstName=@name,LastName=@lastName,Email=@mail where UserId=@id";
                    commnand.Parameters.AddWithValue("@userName", userName);
                    commnand.Parameters.AddWithValue("@pass", password);
                    commnand.Parameters.AddWithValue("@name", name);
                    commnand.Parameters.AddWithValue("@lastName", lastName);
                    commnand.Parameters.AddWithValue("@mail", mail);
                    commnand.Parameters.AddWithValue("@id", id);
                    commnand.CommandType = CommandType.Text;
                    commnand.ExecuteNonQuery();

                }

            }
                
        }
        public string nombreServidor() 
        {
            var connection = GetConnection();
            return connection.ToString();

        }
        public bool Login(string user, string pass)
        { 
            using(var connection = GetConnection())
            {
                connection.Open();
                using(var command=new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select *from Users where LoginName=@user and Password=@pass";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass",pass);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader= command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserLoginCache.IdUser = reader.GetInt32(0);
                            UserLoginCache.FirstName = reader.GetString(3);
                            UserLoginCache.LastName = reader.GetString(4);
                            UserLoginCache.Position = reader.GetString(5);
                            UserLoginCache.Email = reader.GetString(6);
                        }
                        return true;
                    }
                    else
                        return false;
                }
            }
            
        }
        //
        public string recoverPassword(string userRequesting)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select *from Users where LoginName=@user or Email=@mail";
                    command.Parameters.AddWithValue("@user", userRequesting);
                    command.Parameters.AddWithValue("@mail", userRequesting);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read() == true)
                    {
                        string userName = reader.GetString(3) + ", " + reader.GetString(4);
                        string userMail = reader.GetString(6);
                        string accountPassword = reader.GetString(2);

                        //PARA ENVIAR A VARIOS DESTINATARIOS

                        //string accountPassword = reader.GetString(2);
                        //List<string> listMail = new List<string>();
                        //foreach
                        //listMail.Add(userMail);

                        var mailService = new MailServices.SystemSupportMail();
                        mailService.sendMail(
                            subject: "Solicitud de recuperacion de contraseña",
                            body: "Hola " + userName + "\nHas solicitado la recuperacion de tu contraseña. \n" +
                            "tu contraseña actual es: " + accountPassword +
                            "\nDe cualquier manera, te recomendados que solicites tu cambio lo mas pronto posible",
                            recipientMail: new List<string> { userMail }

                            );
                        return "Hola " + userName + "\nHas solicitado la recuperacion de tu contraseña. \n" +
                            "Corrobora tu bandeja de entrada del correo " + userMail +
                            "\nDe cualquier manera, te recomendados que solicites tu cambio lo mas pronto posible";
                    }
                    else
                        return "Lo sientimos, no hay ninguna cuenta con este usuario o correo electronico";
                }
            }
        }
        public void AnyMethod()
        {
            if (UserLoginCache.Position==Positions.Administrador)
            {
                //Codigo
            }
            if(UserLoginCache.Position==Positions.Ingeniero)
            {

            }
            if (UserLoginCache.Position == Positions.Interno)
            {

            }
        }
    }
}
