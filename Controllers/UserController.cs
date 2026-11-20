using System;
using System.Data.SqlClient;
using GoTogether.Models;

namespace GoTogether.Controllers
{
    public class UserController
    {
        public bool RegisterUser(User user, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(user.FullName) ||
                string.IsNullOrWhiteSpace(user.EmployeeCode) ||
                string.IsNullOrWhiteSpace(user.Phone) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                message = "Please fill in all registration fields.";
                return false;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string checkSql = "SELECT COUNT(*) FROM Users WHERE EmployeeCode = @EmployeeCode";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@EmployeeCode", user.EmployeeCode.Trim());
                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            message = "An account with this Employee Code already exists.";
                            return false;
                        }
                    }

                    string insertSql = @"INSERT INTO Users (FullName, EmployeeCode, Phone, Password)
                                       VALUES (@FullName, @EmployeeCode, @Phone, @Password)";
                    using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", user.FullName.Trim());
                        cmd.Parameters.AddWithValue("@EmployeeCode", user.EmployeeCode.Trim());
                        cmd.Parameters.AddWithValue("@Phone", user.Phone.Trim());
                        cmd.Parameters.AddWithValue("@Password", user.Password);

                        cmd.ExecuteNonQuery();
                    }
                }

                message = "Registration successful! You can now log in.";
                return true;
            }
            catch (Exception ex)
            {
                message = "Database error during registration: " + ex.Message;
                return false;
            }
        }

        public User Login(string employeeCode, string password, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(employeeCode) || string.IsNullOrWhiteSpace(password))
            {
                message = "Please enter both Employee Code and Password.";
                return null;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT UserID, FullName, EmployeeCode, Phone, Password
                                 FROM Users
                                 WHERE EmployeeCode = @EmployeeCode AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode.Trim());
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    FullName = reader["FullName"].ToString(),
                                    EmployeeCode = reader["EmployeeCode"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Password = reader["Password"].ToString()
                                };
                            }
                        }
                    }
                }

                message = "Invalid Employee Code or Password.";
                return null;
            }
            catch (Exception ex)
            {
                message = "Database error during login: " + ex.Message;
                return null;
            }
        }
    }
}