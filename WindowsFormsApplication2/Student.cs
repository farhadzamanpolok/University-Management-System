using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Student : Form
    {
        private readonly string _sid;
        private string _sname;
        private string _dept;
        private string _presemister;
        
        public Student(string sid)
        {
            _sid = sid;
            InitializeComponent();
            SHomeP.BringToFront();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            SHomeP.BringToFront();
            sHomeSemistercomboBox.DataSource =
               DataAccess.GetDataTable("select semister from taken_course where sId='" + _sid + "' group by semister");

            sHomeSemistercomboBox.DisplayMember = "semister";
            sHomeSemisterDataGridView.DataSource = DataAccess.GetDataTable("select course_id as 'Course id',course_name as 'Course name',fname as 'Faculty',mid_grade as 'Mid',final_grade as 'Final' from taken_course where semister='" + sHomeSemistercomboBox.GetItemText(sHomeSemistercomboBox.SelectedItem) + "' and sId='" + _sid + "'");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SCourseP.BringToFront();
            SCourseSemistercomboBox.DataSource =
                DataAccess.GetDataTable("select semister from taken_course where sId='" + _sid + "' group by semister");

            SCourseSemistercomboBox.DisplayMember = "semister";

            SCourseSubjectcomboBox.DataSource =
                DataAccess.GetDataTable("select course_name from taken_course where semister='" +
                                        SCourseSemistercomboBox.GetItemText(SCourseSemistercomboBox.SelectedItem) + "' and sid='"+_sid+"' ");
            SCourseSubjectcomboBox.DisplayMember = "course_name";

        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            settingspanel.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            SPregP.BringToFront();
            DataTable dt1 =
                DataAccess.GetDataTable("select course_name from taken_course where sid='" + _sid + "' and semister='" +
                                        _presemister + "'");

            SOfferdCourses.DataSource = DataAccess.GetDataTable("select c.course_name,c.creadit_hour from taken_course e ," + _dept + "_course_list c where e.course_id=c.Prerequisite and e.sId='" + _sid + "' and mid_mark >=50");
            SOfferdCourses.DisplayMember = "course_name";                                         
                                                      
            //if (SOfferdCourses.Items.Count == 0)
            //{
            //    foreach (DataRow row in dt2.Rows)
            //    {
            //        SOfferdCourses.Items.Add(row[0]);

            //    }
            //}
            if (SCourseTaken.Items.Count == 0)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    SCourseTaken.Items.Add(row[0]);

                }
            }

        }

       

        private void Student_Load(object sender, EventArgs e)
        {
            DataTable dt1 = DataAccess.GetDataTable("select status,semister from sprereg");
            if (dt1.Rows[0]["status"].Equals(1))
            {
                SRegB.Enabled = true;
            }
            _presemister = dt1.Rows[0]["semister"].ToString();
            DataTable dt2 = DataAccess.GetDataTable("select student_name,dept from student where student_id='" + _sid + "'");
            _sname = dt2.Rows[0]["student_name"].ToString();
            _dept = dt2.Rows[0]["dept"].ToString();
            SNamelabel.Text = _sname;
            SIdlabel.Text = _sid;
            sHomeSemistercomboBox.DataSource =
                DataAccess.GetDataTable("select semister from taken_course where sId='" + _sid + "' group by semister");

            sHomeSemistercomboBox.DisplayMember = "semister";
           sHomeSemisterDataGridView.DataSource= DataAccess.GetDataTable("select course_id as 'Course id',course_name as 'Course name',fname as 'Faculty',mid_grade as 'Mid',final_grade as 'Final' from taken_course where semister='"+sHomeSemistercomboBox.GetItemText(sHomeSemistercomboBox.SelectedItem)+"' and sId='"+_sid+"'");
        
        }

        private void SAddCourse_Click(object sender, EventArgs e)
        {
            
            try
            {

            
            for (int k = 0; k < SOfferdCourses.Items.Count; k++)
            {
                if (SCourseTaken.Items.Contains(SOfferdCourses.SelectedItem) == false)
                {
                    SCourseTaken.Items.Add(SOfferdCourses.SelectedItem);
                    DataTable dt =
                        DataAccess.GetDataTable("select course_id,prerequisite,creadit_hour,course_name from " + _dept +
                                                "_course_list where course_name='" +
                                                SOfferdCourses.GetItemText(SOfferdCourses.SelectedItem) + "'");
                   
                    string prereq = dt.Rows[0]["prerequisite"].ToString();
                    string courseId = dt.Rows[0]["course_id"].ToString();
                    string creadit = dt.Rows[0]["creadit_hour"].ToString();
                    string courseName = dt.Rows[0]["course_name"].ToString();
                   
                    DataAccess.ExecuteSql(
                        "insert into taken_course(sId,sname,course_id,course_name,creadit,prereq,semister,Sdept) values('" +
                        _sid + "','" + _sname + "','" + courseId + "','" + courseName + "','" + creadit + "','" +
                        prereq + "','" + _presemister + "','" + _dept + "')");
                }
                
            }
            }
            catch (Exception)
            {
                MessageBox.Show(@"Please Select A Course", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SLogOutB_Click(object sender, EventArgs e)
        {
            new LoginWindow().Show();
            Dispose();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            DataAccess.Close();
            Application.Exit();
        }

        private void SRemoveCourse_Click(object sender, EventArgs e)
        {
            if (SCourseTaken.Items.Count!=0)
            {
                DataAccess.ExecuteSql("delete from taken_course where semister='" + _presemister + "' and sid='" + _sid + "' and course_name='" + SCourseTaken.GetItemText(SCourseTaken.SelectedItem) + "'");
                SCourseTaken.Items.Remove(SCourseTaken.SelectedItem);
            }
           
        }

        private void sHomeSemistercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sHomeSemisterDataGridView.DataSource = DataAccess.GetDataTable("select course_id as 'Course id',course_name as 'Course name',fname as 'Faculty',mid_grade as 'Mid',final_grade as 'Final' from taken_course where semister='" + sHomeSemistercomboBox.GetItemText(sHomeSemistercomboBox.SelectedItem) + "' and sId='" + _sid + "'");
        }

        private void SCourseSemistercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            SCourseSubjectcomboBox.DataSource =
               DataAccess.GetDataTable("select course_name from taken_course where semister='" +
                                       SCourseSemistercomboBox.GetItemText(SCourseSemistercomboBox.SelectedItem) + "' ");
             
            SCourseSubjectcomboBox.DisplayMember = "course_name";

           
        }

        private void SCourseSubjectcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            sMarkdataGridView.DataSource =
              DataAccess.GetDataTable(
                    "select mid_mark as 'MidTerm Mark',mid_grade as 'MidTerm Grade' ,final_mark as 'FinalTerm Mark',final_grade as 'FinalTerm Grade' from taken_course where course_name='" + SCourseSubjectcomboBox.GetItemText(SCourseSubjectcomboBox.SelectedItem) + "'and sid='" + _sid + "' and semister='" + SCourseSemistercomboBox.GetItemText(SCourseSemistercomboBox.SelectedItem) + "'");
            
            
        }

        private void sPSubbutton_Click(object sender, EventArgs e)
        {
            if (sPtextBox1.Text == sPtextBox2.Text)
            {
                DataAccess.ExecuteSql("update student set password='" + sPtextBox2.Text + "' where student_id='" + _sid + "'");
                MessageBox.Show(@"Password Is Chenged", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(@"Password Does Not Match", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
        }

        private void SAttendenceB_Click(object sender, EventArgs e)
        {
            AttendencePanel.BringToFront();
           
            SATSemistercomboBox.DataSource =
                DataAccess.GetDataTable("select semister from taken_course where sId='" + _sid + "' group by semister");

            SATSemistercomboBox.DisplayMember = "semister";

            sATCoursecomboBox.DataSource =
                DataAccess.GetDataTable("select course_name from taken_course where semister='" +
                                       SATSemistercomboBox.GetItemText(SATSemistercomboBox.SelectedItem) + "' and sid='" + _sid + "' ");
           sATCoursecomboBox.DisplayMember = "course_name";

        }

        private void SATScomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sATCoursecomboBox.DataSource =
               DataAccess.GetDataTable("select course_name from taken_course where semister='" +
                                        SATSemistercomboBox.GetItemText(SATSemistercomboBox.SelectedItem) + "' and sid='"+_sid+"' ");

            sATCoursecomboBox.DisplayMember = "course_name";

               }

        private void sATCNcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SATdataGridView.DataSource =
                DataAccess.GetDataTable(
                    "select day1,day2,day3,day4,day5 from taken_course where course_name='" + sATCoursecomboBox.GetItemText(sATCoursecomboBox.SelectedItem) + "'and sid='" + _sid + "' and semister='" + SATSemistercomboBox.GetItemText(SATSemistercomboBox.SelectedItem) + "'");
        }

   
    }
}
