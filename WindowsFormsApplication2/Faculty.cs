using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Faculty : Form
    {
        private readonly string _fid;
        private string _fdept,_fname,_pregsemister;

        public Faculty(string id)
        {
            _fid = id;
            InitializeComponent();
            FHomeP.BringToFront();

        }

        private void FHomeP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FHomeB_Click(object sender, EventArgs e)
        {
            FHomeP.BringToFront();
            FHomeSemistercomboBox.DataSource =
              DataAccess.GetDataTable("select semister from taken_course where fId='" + _fid + "' group by semister");

            FHomeSemistercomboBox.DisplayMember = "semister";
            FHomeSemisterDataGridView.DataSource = DataAccess.GetDataTable("select course_id as 'Course id',course_name as 'Course name',creadit as 'Creadit'  from taken_course where semister='" + FHomeSemistercomboBox.GetItemText(FHomeSemistercomboBox.SelectedItem) + "' and fId='" + _fid + "'");
        }

        private void FCourse_Click(object sender, EventArgs e)
        {
            FCoursesP.BringToFront();
            
            
        }

        private void FAttB_Click(object sender, EventArgs e)
        {
            FAttP.BringToFront();
            FATTENSemistercomboBox.DataSource =
               DataAccess.GetDataTable("select semister from taken_course where fId='" + _fid + "' group by semister");

            FATTENSemistercomboBox.DisplayMember = "semister";

            FATTENCoursecomboBox.DataSource =
                DataAccess.GetDataTable("select course_name from taken_course where semister='" +
                                       FATTENSemistercomboBox.GetItemText(FATTENSemistercomboBox.SelectedItem) + "' and fid='" + _fid + "' ");
            FATTENCoursecomboBox.DisplayMember = "course_name";
        }

        private void FGradeB_Click(object sender, EventArgs e)
        {
            FGradeP.BringToFront();
            FATSemistercomboBox.DataSource =
                DataAccess.GetDataTable("select semister from taken_course where fId='" + _fid + "' group by semister");

            FATSemistercomboBox.DisplayMember = "semister";

            FATCoursecomboBox.DataSource =
                DataAccess.GetDataTable("select course_name from taken_course where semister='" +
                                       FATSemistercomboBox.GetItemText(FATSemistercomboBox.SelectedItem) + "' and fid='" + _fid + "' ");
            FATCoursecomboBox.DisplayMember = "course_name";
        }

        private void FRegB_Click(object sender, EventArgs e)
        {
            FRegP.BringToFront();
            FOfferedCourses.DataSource = DataAccess.GetDataTable("select course_name from " + _fdept + "_faculty_course ");
            FOfferedCourses.DisplayMember = "course_name";
        }

        private void Faculty_Load(object sender, EventArgs e)
        {
            DataTable dt = DataAccess.GetDataTable("select name,dept from faculty");
           
            _fname = dt.Rows[0]["name"].ToString();
            _fdept = dt.Rows[0]["dept"].ToString();
            FNlabel.Text = _fname;
            FIdlebel.Text = _fid;

            DataTable dt1 = DataAccess.GetDataTable("select status,semister from fprereg");

            if (dt1.Rows[0]["status"].Equals(1))
            {
                FRegB.Enabled = true;
            }
            _pregsemister = dt1.Rows[0]["semister"].ToString();
            FHomeSemistercomboBox.DataSource =
               DataAccess.GetDataTable("select semister from taken_course where fId='" + _fid + "' group by semister");

            FHomeSemistercomboBox.DisplayMember = "semister";
            FHomeSemisterDataGridView.DataSource = DataAccess.GetDataTable("select course_id as 'Course id',course_name as 'Course name',creadit as 'Creadit'  from taken_course where semister='" + FHomeSemistercomboBox.GetItemText(FHomeSemistercomboBox.SelectedItem) + "' and fId='" + _fid + "'");
        }

        private void SAddCourse_Click(object sender, EventArgs e)
        {
            DataAccess.ExecuteSql("update taken_course set fname='" + _fname + "' ,fid='" + _fid + "' where course_name='" + FOfferedCourses.GetItemText( FOfferedCourses.SelectedItem)+ "' and semister='"+_pregsemister+"'");
            FCourseTaken.DataSource = DataAccess.GetDataTable("select course_name from taken_course where fid='"+_fid+"' and semister='"+_pregsemister+"'");
            FCourseTaken.DisplayMember = "course_name";
        }

        private void FRemoveCourse_Click(object sender, EventArgs e)
        {
            DataAccess.ExecuteSql("update taken_course set fname='" + null + "' ,fid='" + null + "' where course_name='" + FCourseTaken.GetItemText(FCourseTaken.SelectedItem) + "' and semister='" + _pregsemister + "'");
            FCourseTaken.DataSource = DataAccess.GetDataTable("select course_name from taken_course where fid='" + _fid + "' and semister='" + _pregsemister + "'");
            FCourseTaken.DisplayMember = "course_name";
        }

        private void FATSemistercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FATCoursecomboBox.DataSource =
               DataAccess.GetDataTable("select course_name from taken_course where semister='" +
                                        FATSemistercomboBox.GetItemText(FATSemistercomboBox.SelectedItem) + "' and fid='"+_fid+"' ");

            FATCoursecomboBox.DisplayMember = "course_name";

            
        }

        private void FATCoursecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            FATdataGridView.DataSource =
                 DataAccess.GetDataSet(
                     "select serial, sid as 'student id',sname as 'student name',mid_mark as 'Midterm Mark',mid_grade as 'Midterm Grade',final_mark as 'Finalterm Mark',final_grade as 'Finalterm Grade' from taken_course where course_name='" + FATCoursecomboBox.GetItemText(FATCoursecomboBox.SelectedItem) + "'and Fid='" + _fid + "' and semister='" + FATSemistercomboBox.GetItemText(FATSemistercomboBox.SelectedItem) + "'").Tables[0];
            FATdataGridView.Columns[0].Visible = false;
            FATdataGridView.Columns[0].Frozen = true;
            FATdataGridView.Columns[1].ReadOnly = true;
            FATdataGridView.Columns[1].Frozen = true;
            FATdataGridView.Columns[2].ReadOnly = true;
            FATdataGridView.Columns[2].Frozen = true;
        }

        private void FGSubmitbutton_Click(object sender, EventArgs e)
        {
            
            DataAccess.Submitdataset();
            
            MessageBox.Show(@"Data Uploaded", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FATTENSemistercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FATTENCoursecomboBox.DataSource =
               DataAccess.GetDataTable("select course_name from taken_course where semister='" +
                                        FATTENSemistercomboBox.GetItemText(FATTENSemistercomboBox.SelectedItem) + "' and fid='" + _fid + "' ");

            FATTENCoursecomboBox.DisplayMember = "course_name";

            
        }

        private void FATTENCoursecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            FATTdataGridView.DataSource =
                DataAccess.GetDataSet(
                    "select serial,sid as 'Student id',sname as'Student Name', day1,day2,day3,day4,day5 from taken_course where course_name='" + FATTENCoursecomboBox.GetItemText(FATTENCoursecomboBox.SelectedItem) + "'and fid='" + _fid + "' and semister='" + FATTENSemistercomboBox.GetItemText(FATTENSemistercomboBox.SelectedItem) + "'").Tables[0];
            
            FATTdataGridView.Columns[0].Visible = false;
            FATTdataGridView.Columns[0].Frozen = true;
            FATTdataGridView.Columns[1].Frozen = true;
            FATTdataGridView.Columns[1].ReadOnly = true;
            FATTdataGridView.Columns[2].Frozen = true;
            FATTdataGridView.Columns[2].ReadOnly = true;
            
        }

        private void FATTSubmitButton_Click(object sender, EventArgs e)
        {
            DataAccess.Submitdataset();

            MessageBox.Show(@"Data Uploaded", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fPSubbutton_Click(object sender, EventArgs e)
        {
            if (fPtextBox1.Text == fPtextBox2.Text)
            {
                DataAccess.ExecuteSql("update faculty set password='" + fPtextBox2.Text + "' where faculty_id='" + _fid + "'");
                MessageBox.Show(@"Password Is Chenged", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(@"Password Does Not Match", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void FLogOutB_Click(object sender, EventArgs e)
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

        private void FHomeSemistercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FHomeSemisterDataGridView.DataSource = DataAccess.GetDataTable("select course_id as 'Course id',course_name as 'Course name',creadit as 'Creadit'  from taken_course where semister='" + FHomeSemistercomboBox.GetItemText(FHomeSemistercomboBox.SelectedItem) + "' and fId='" + _fid + "'");
        }
        
    }
}
