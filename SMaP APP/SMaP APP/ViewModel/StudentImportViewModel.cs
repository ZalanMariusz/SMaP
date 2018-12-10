using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMaP_APP.ViewModel
{
    class StudentImportViewModel : BaseViewModel<Student>
    {
        public DataTable Table { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public StudentImportViewModel(DataTable dt, StudentImportWindow source)
        {
            this.Table = dt;
            this.SourceWindow = source;
            this.SaveCommand = new RelayCommand(ImportStudents);
        }
        private void ImportStudents()
        {
            bool duplicateError = false;
            bool sessionGroupMissing = false;
            bool teamMissing = false;
            List<Users> userList = new List<Users>();
            foreach (DataRow item in Table.Rows)
            {
                userList.Add((Users)item["User"]);
            }
            var NEPTUNDuplicates = userList.GroupBy(x => x.NEPTUN).Select(c => new { Key = c.Key, total = c.Count() });
            var MailDuplicates = userList.GroupBy(x => x.Email).Select(c => new { Key = c.Key, total = c.Count() });

            foreach (var item in NEPTUNDuplicates)
            {
                if (item.total > 1)
                {
                    duplicateError = true;
                    break;
                }
            }
            foreach (var item in MailDuplicates)
            {
                if (item.total > 1)
                {
                    duplicateError = true;
                    break;
                }
            }

            UsersDAL ud = new UsersDAL();
            SessionGroupDAL sgd = new SessionGroupDAL();
            TeamDAL td = new TeamDAL();
            foreach (DataRow item in Table.Rows)
            {
                Users rowUser = ((Users)item["User"]);
                string sessiongroupName = (string)item["SessionGroup"];
                string teamName = (string)item["Team"];
                duplicateError = duplicateError || ud.FindAll().Exists(x => x.Email == rowUser.Email);
                duplicateError = duplicateError || ud.FindAll().Exists(x => x.Email == rowUser.NEPTUN);

                SessionGroup sg = sgd.FindAll(x=>x.Semester.IsActive && x.SessionGroupName== sessiongroupName).FirstOrDefault();
                sessionGroupMissing = sessionGroupMissing || (sg == null);
                teamMissing = teamMissing || sessionGroupMissing;
                if (!teamMissing)
                {
                    Team t = td.FindAll(x=>x.SessionGroupID==sg.ID && x.TeamName== teamName).FirstOrDefault();
                    teamMissing = teamMissing || (t == null);
                }
            }
            string errorMessage = "";
            if (duplicateError)
            {
                errorMessage += "\nAz állományban több azonos, vagy a rendszerben már szereplő E-mail cím vagy NEPTUN kód szerepel!";
            }
            if (sessionGroupMissing)
            {
                errorMessage += "\nAz állományban nem létező csoport szerepel!";
            }
            if (teamMissing)
            {
                errorMessage += "\nAz állományban nem létező csapat szerepel!";
            }
            if (!String.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Hiba az importált adatokban!"+errorMessage, "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                foreach (DataRow item in Table.Rows)
                {
                    string sessionGroupName = (string)item["SessionGroup"];
                    string teamName = (string)item["Team"];
                    SessionGroup sg = sgd.FindAll(x=>x.Semester.IsActive && x.SessionGroupName== sessionGroupName).FirstOrDefault();
                    Team t = td.FindAll(x => x.SessionGroupID==sg.ID && x.TeamName== teamName).FirstOrDefault();
                    Student st = new Student()
                    {
                        TeamID = t.ID
                    };
                    Users u = (Users)item["User"];
                    u.IsPasswordChangeRequired = true;
                    ud.Create(u);
                    st.UserID = u.ID;
                    u.Student.Add(st);
                    ud.Update(u);
                    MailSender.SendMail(u.Email);
                }
                MessageBox.Show("Sikeres importálás!", "", MessageBoxButton.OK);
                this.Close();
            }

        }
    }
}
