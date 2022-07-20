using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;


namespace Scanan_go.Classes
{
    public class SQLControl
    {
        Classes.connection obj = new Classes.connection();

        #region Check if Exist in the Registered 
        // check if exist and add data
        public string CheckIfExist(string fullsname, string email, string phoneNo, string address)
        {
            SqlCommand cmdcheck = new SqlCommand("SELECT * FROM tblRegistered WHERE fullname = '" + (fullsname) + "' or email = '" + (email) + "'", obj.Activecon());
            SqlDataAdapter sdacheck = new SqlDataAdapter(cmdcheck);
            DataTable dtcheck = new DataTable();
            sdacheck.Fill(dtcheck);
            SqlCommand cmd = new SqlCommand("INSERT INTO tblRegistered(fullname,email, phoneNo, address) VALUES ('" + (fullsname) + "','" + (email) + "','" + (phoneNo) + "','" + (address) + "' ) ", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            NewMethodcheck(dtcheck, cmd);
            return fullsname;
        }
        // checking if the data exist
        private static void NewMethodcheck(DataTable dtcheck, SqlCommand cmd)
        {
            
            if (dtcheck.Rows.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("ACCOUNT EXIST");
                //Form2 frm2 = new Form2();
                // frm2.Show();      
            }
            else
            {
                if (cmd.ExecuteNonQuery().Equals(1))
                {
                    //System.Windows.Forms.MessageBox.Show("REGISTERED SUCCESFUL");
                }
                else
                {
                   // System.Windows.Forms.MessageBox.Show("REGISTERED SUCCESFUL");
                }
            }
        }
        #endregion
        // database login
        #region Login Query
        public string Login(string username, string password)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblAdmin Where username = '" + (username) + "' and password = '" + (password) + "'", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            NewMethod(dt);      
            return username;
        }
        private static void NewMethod(DataTable dt)
        {
           
            if (dt.Rows.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("WELCOME");
                AdMain_Form adminform = new AdMain_Form();
                adminform.ShowDialog();
                Form1 frm1 = new Form1();
                frm1.Close();
                frm1.Dispose();
            }
            else
            {                          
                System.Windows.Forms.MessageBox.Show("wrong username or password");
                

            }
        }
        #endregion
        #region Admin Main Form
        public string MonitoringDatabase(string fullname, string email, string phoneNo, string address, string date, string time)
        {
            SqlCommand cmd = new SqlCommand("if exists(select * from tblRegistered d where d.FullName = '" + (fullname) + "' or d.Email = '" + (email) + "') INSERT INTO tblMonitoring(FullName, Email, PhoneNum, Address, date, time) VALUES('" + (fullname) + "', '" + (email) + "', '" + (phoneNo) + "', '" + (address) + "', '" + (date) + "', '" + (time) + "') ", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM tblRegistered Where fullname = '" + (fullname ) + "' and email = '" + (email ) + "'", obj.Activecon());
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            NewMethod1(dt);
            cmd1.ExecuteNonQuery();
            cmd.ExecuteNonQuery();        
            return (fullname);        
        }
        private static void NewMethod1(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                frmWelcome frmwelcome = new frmWelcome();
                frmwelcome.ShowDialog(); 
            }
            else
            {
                frmUnregistered unregistered = new frmUnregistered();
                unregistered.ShowDialog();
             
            }
            return;
        }
        public string fetchRegistered(string sql, DataGridView grid)
        {
            SqlCommand cmd = new SqlCommand(" SELECT fullname, email, phoneNo, address FROM tblRegistered", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grid.DataSource = dt;
            return sql;
        }
        public string findingRegistered(string search, DataGridView grid)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblRegistered WHERE(CONCAT(fullname,email,phoneNo,address )) LIKE '%" + search + "%'  ", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grid.DataSource = dt;
            return search;
        }
        public string fetchMonitoring(string sql, DataGridView grid)
        {
            SqlCommand cmd = new SqlCommand(" SELECT FullName, Email, PhoneNum, Address, date, time FROM tblMonitoring", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grid.DataSource = dt;
            return sql;
        }
        public string findingMonitoring(string search, DataGridView grid)
        {
            SqlCommand cmd = new SqlCommand("SELECT FullName, Email, PhoneNum, Address, date, time FROM tblMonitoring WHERE(CONCAT(FullName, Email, PhoneNum, Address, date, time )) LIKE '%" + search + "%'  ", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grid.DataSource = dt;
            return search;
        }
        public string fetchingDatetoDate(string  dateFrom, string  dateTo, DataGridView grid)
        {
            SqlCommand cmd = new SqlCommand("SELECT FullName, Email, PhoneNum, Address, date, time FROM tblMonitoring WHERE date BETWEEN '" + (dateFrom) + "' and '" + (dateTo) + "' ", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grid.DataSource = dt;
            return dateFrom;
        }
        public string fetchAdmin(string sql, DataGridView grid)
        {
            SqlCommand cmd = new SqlCommand(" SELECT * FROM tblAdmin", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grid.DataSource = dt;

            return sql;
        }
        public string InsertAdmin(string usertype, string username, string password)
        {
            SqlCommand cmdcheck = new SqlCommand("SELECT * FROM tblAdmin WHERE username = '" + (username ) + "' or password = '" + (password) + "'", obj.Activecon());
            SqlDataAdapter sdacheck = new SqlDataAdapter(cmdcheck);
            DataTable dtcheck = new DataTable();
            sdacheck.Fill(dtcheck);
            SqlCommand cmd = new SqlCommand("INSERT INTO tblAdmin(usertype, username, password) VALUES ('" + (usertype) + "','" + (username) + "','" + (password) + "' ) ", obj.Activecon());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            NewMethodcheck2(dtcheck, cmd);
            return usertype ;
        }
        // checking if the data exist
        private static void NewMethodcheck2(DataTable dtcheck, SqlCommand cmd)
        {
            if (dtcheck.Rows.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("ACCOUNT EXIST");
                //Form2 frm2 = new Form2();
                // frm2.Show();      
            }
            else
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
                
            }
        }
        public string DeleteAdmin(string id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM tblAdmin WHERE id = '" + (id) + "'", obj.Activecon());
            System.Windows.Forms.DialogResult dialogResult = MessageBox.Show($"you want to delete ?", "CONFIRMATION", MessageBoxButtons.OKCancel);
            if (dialogResult  == DialogResult.OK)
            {
                cmd.ExecuteNonQuery();
            }
            else
            {
                return id;
            }
                
            //MessageBox.Show("done");
            return id;
        }
        public string UpdateAdmin(string usertype,string username, string password,string id)
        {
            SqlCommand cmd = new SqlCommand("UPDATE tblAdmin SET usertype = '"+(usertype )+"',username = '"+(username)+"' , password = '"+(password)+"' WHERE id = '" + (id) + "'", obj.Activecon());
            System.Windows.Forms.DialogResult dialogResult = MessageBox.Show($"you want to update ?", "CONFIRMATION", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                cmd.ExecuteNonQuery();
            }
            else
            {
                return id;
            }

            //MessageBox.Show("done");
            return id;
        }
        #endregion
    }
}
