using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Administrator : Form
    {

       
        private readonly string _adminId ;
        public Administrator(string id)
        {
            InitializeComponent();
            massagepanel.BringToFront();
            _adminId = id;
        }


        private void homeButton_Click(object sender, EventArgs e)
        {
           massagepanel.BringToFront();
           
        }

        private void studentbutton_Click(object sender, EventArgs e)
        {
            studentPanel.BringToFront();
            DataTable dt = DataAccess.GetDataTable("select status from sprereg");
            if (dt.Rows[0]["status"].Equals(1))
            {
                addStudentButton.Enabled = true;
            }
        }

        private void addStudentButton_Click(object sender, EventArgs e)
        {
            addstudentpanel.BringToFront();
            DataTable dt = DataAccess.GetDataTable("select semister from sprereg");
            addStudentSemisterDurationTextBox.Text = dt.Rows[0][0].ToString();
        }

        private void removeStudentButton_Click(object sender, EventArgs e)
        {
            SRemovePanel.BringToFront();
            SearchSTextBox.DataSource = DataAccess.GetDataTable("select student_id from student");
            SearchSTextBox.DisplayMember = "student_id";
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            modifySpanel.BringToFront();
            mSTIDtextBox.DataSource = DataAccess.GetDataTable("select student_id from student");
            mSTIDtextBox.DisplayMember = "student_id";
        }

        private void facultybutton_Click(object sender, EventArgs e)
        {
            facultyPanel.BringToFront();
            DataTable dt = DataAccess.GetDataTable("select status from fprereg");
            if (dt.Rows[0]["status"].Equals(1))
            {
                addteacherButton.Enabled = true;

            }
        }

        private void coursebutton_Click(object sender, EventArgs e)
        {
            coursePanel.BringToFront();
        }

        private void courseHome_Click(object sender, EventArgs e)
        {
            massagepanel.BringToFront();
        }

        private void teacherHomeButton_Click_1(object sender, EventArgs e)
        {
            massagepanel.BringToFront();
        }

        

        

        private void addStudentTabpageNextButton_Click(object sender, EventArgs e)
        {
            addStudentPanelTab.SelectTab(addStudentPaneltabPage2);
        }

       

        private void addteacherButton_Click(object sender, EventArgs e)
        {
            addFacultyPanel.BringToFront();
        }

        private void addFacultyIDTextBox_TextChanged(object sender, EventArgs e)
        {
            toolTip.Show("000-000-00", addFacultyIDTextBox);
        }


        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            toolTip.Show("yyyy-yyyy",addStudentSemisterDurationTextBox);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            toolTip.Show("00-00000-0",addStudentIdTextBox);
        }

        

        private void sudentCourseListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolTip.Show("Added Courses", studentCourseListBox1);
        }

        private void addFacultypanelSaveButton_Click(object sender, EventArgs e)
        {
            if (addFacultyIDTextBox.Text.Equals(""))
            {
                addFacultyIDTextBox.BackColor = Color.LightCoral;
                toolTip.Show(@"Username cannot Be Blank", addFacultyIDTextBox);

            }
            else if (addFacultyPasswordTextBox.Text.Equals(""))
            {
                addFacultyPasswordTextBox.BackColor = Color.LightCoral;
                toolTip.Show(@"Password Cannot Be Blank", addFacultyPasswordTextBox);
            }
            else if (addFacultyDepartmentcomboBox.Text.Equals(""))
            {
                MessageBox.Show(@"Select A Department ", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string dept;
                if (addFacultyDepartmentcomboBox.Text.Equals("C.S.E"))
                {
                    dept = "CSE";
                }
                else if (addFacultyDepartmentcomboBox.Text.Equals("E.E.E"))
                {
                    dept = "EEE";
                }
                else
                {
                    dept = "BBA";  
                }
                
               
                try
                {
                    
                    DataAccess.ExecuteSql(
              "insert into faculty values ('" + addFacultyIDTextBox.Text + "','" + addFacultyPasswordTextBox.Text + "','" + addFacultyNameTextBox.Text + "','" + addFacultyGenderComboBox.Text + "','" + dept + "','" + addFacultybirth_dateDateTimePicker.Text + "','" + addFacultyphoneTextBox.Text + "','" + addFacultyaddressTextBox.Text + "','" + addFacultycountryTextBox.Text + "','" + addFacultycityTextBox.Text + "','" + addFacultynationalityTextBox.Text + "','" + addFacultyemailTextBox.Text + "')");
                    MessageBox.Show(@" Added".ToUpper());
                }
                catch (Exception)
                {

                    MessageBox.Show(@"This Faculty Id Is Alrady Added");
                    return;
                }

                foreach (Control c in addFacultyPanel.Controls)
                {
                    if (c is TextBox || c is RichTextBox || c is ComboBox)
                    {
                        c.Text = string.Empty;
                    }
                }
            
            }
          
        }


        private void addAdmin_Click_1(object sender, EventArgs e)
        {
            addAdminpanel.BringToFront();
            DataTable dt = DataAccess.GetDataTable("select status,semister from sprereg");
            DataTable dt2 = DataAccess.GetDataTable("select status from fprereg");
            if (dt.Rows[0][0].Equals(1))
            {
                sRegONbutton.BackColor = Color.GreenYellow;
                sRegOFFbutton.BackColor = Color.Red;
            }
            else
            {
                sRegONbutton.BackColor = Color.Red;
                sRegOFFbutton.BackColor = Color.GreenYellow;
            }
            if (dt2.Rows[0][0].Equals(1))
            {
                fRegONbutton.BackColor = Color.GreenYellow;
                fRegOFFbutton.BackColor = Color.Red;
            }
            else
            {
                fRegONbutton.BackColor = Color.Red;
                fRegOFFbutton.BackColor = Color.GreenYellow;
            }
            prevSemistertextBox.Text = dt.Rows[0]["semister"].ToString();
        }

        private void newAdminAddbutton_Click(object sender, EventArgs e)
        {
            try
            {

            
            if (newAdminUserNametextBox.Text.Equals("") == false && newAdminPasswordtextBox.Text.Equals("") == false)
            {
                string substring = newAdminUserNametextBox.Text.Substring(0);
                if (substring.Length == 10 && substring[1] == '-' && substring[6] == '-')
                {
                    DataAccess.ExecuteSql("insert into admin values ('" + newAdminUserNametextBox.Text + "','" +
                                          newAdminPasswordtextBox.Text + "','" + addAdminNametextBox.Text + "')");
                    MessageBox.Show(@"New Administrator Added", @"INFO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(@"ERROR!
 Administrator ID Formet ERROR .
Please Try Again", @"ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(@"please fill both fild".ToUpper(), @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
            catch (SqlException)
            {

                MessageBox.Show(@"User Is Alrady present", @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show(@"ERROR!
 A Problem Created While Saving Data.
Please Try Again", @"ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }

        private void logOut_Click(object sender, EventArgs e)
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

        private void massagepanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void addFacultyPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            addFacultyPasswordTextBox.BackColor = Color.White;
        }

        private void addFacultyIDTextBox_TextChanged_1(object sender, EventArgs e)
        {
            addFacultyIDTextBox.BackColor = Color.White;
        }

        private void addStudentdeptComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (addStudentdeptComboBox.Text.Equals("C.S.E"))
            {
                studentCourselistBox2.DataSource = DataAccess.GetDataTable("select course_name from CSE_course_list where prerequisite='-'");
                studentCourselistBox2.DisplayMember = "course_name";
            }
            else if (addStudentdeptComboBox.Text.Equals("E.E.E"))
            {
                studentCourselistBox2.DataSource = DataAccess.GetDataTable("select course_name  from eee_course_list where prerequisite='-'");
                studentCourselistBox2.DisplayMember = "course_name";
            }
            else if (addStudentdeptComboBox.Text.Equals("B.B.A"))
            {
                studentCourselistBox2.DataSource = DataAccess.GetDataTable("select course_name  from BBA_course_list where prerequisite='-'");
                studentCourselistBox2.DisplayMember = "course_name";
            }
           
        }

        private void sudentAddCourseButton_Click(object sender, EventArgs e)
        {
            string prereq=null, courseId=null, creadit=null;
            string dept=null;
            try
            {
            for (int k = 0; k < studentCourselistBox2.Items.Count; k++)
            {
                if (studentCourseListBox1.Items.Contains(studentCourselistBox2.SelectedItem) == false)
                {
                    studentCourseListBox1.Items.Add(studentCourselistBox2.SelectedItem);
                    studentCourseListBox1.DisplayMember = "course_name";
                     string courseName=studentCourselistBox2.GetItemText(studentCourselistBox2.SelectedItem);
                    if (addStudentdeptComboBox.Text.Equals("C.S.E"))
                     {
                         DataTable dt = DataAccess.GetDataTable("select course_id,prerequisite,creadit_hour from CSE_course_list where course_name='" +
                          studentCourselistBox2.GetItemText(studentCourselistBox2.SelectedItem)+"'");
                         prereq = dt.Rows[0]["prerequisite"].ToString();
                          courseId = dt.Rows[0]["course_id"].ToString();
                         creadit = dt.Rows[0]["creadit_hour"].ToString();
                         dept = "CSE";
                     }
                    else if (addStudentdeptComboBox.Text.Equals("E.E.E"))
                    {
                        DataTable dt = DataAccess.GetDataTable("select course_id,prerequisite,creadit_hour from EEE_course_list where course_name='" +
                         studentCourselistBox2.GetItemText(studentCourselistBox2.SelectedItem) + "'");
                        prereq = dt.Rows[0]["prerequisite"].ToString();
                        courseId = dt.Rows[0]["course_id"].ToString();
                        creadit = dt.Rows[0]["creadit_hour"].ToString();
                        dept = "EEE";
                    }
                    else if (addStudentdeptComboBox.Text.Equals("B.B.A"))
                    {
                        DataTable dt = DataAccess.GetDataTable("select course_id,prerequisite,creadit_hour from BBA_course_list where course_name='" +
                        studentCourselistBox2.GetItemText(studentCourselistBox2.SelectedItem) + "'");
                        prereq = dt.Rows[0]["prerequisite"].ToString();
                        courseId = dt.Rows[0]["course_id"].ToString();
                        creadit = dt.Rows[0]["creadit_hour"].ToString();
                        dept = "BBA";
                    }
                    string sId = addStudentIdTextBox.Text;
                    string semister = addStudentSemisterDurationTextBox.Text;
                    string sname = addStudent_nameTextBox.Text;

                    DataAccess.ExecuteSql("insert into taken_course(sId,sname,course_id,course_name,creadit,prereq,semister,Sdept) values('" + sId + "','" + sname + "','" + courseId + "','" + courseName + "','" + creadit + "','" + prereq + "','" + semister + "','"+dept+"')");

                }

            }
            }
            catch (Exception)
            {

                MessageBox.Show(@"Please Select A Course", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void studentSaveButton_Click(object sender, EventArgs e)
        {
            if (addStudentIdTextBox.Text.Equals(""))
            {
                addStudentIdTextBox.BackColor = Color.LightCoral;
                toolTip.Show(@"Username cannot Be Blank", addStudentIdTextBox);

            }
            else if (addStudentPasswordTextBox.Text.Equals(""))
            {
                addStudentPasswordTextBox.BackColor = Color.LightCoral;
                toolTip.Show(@"Password Cannot Be Blank", addStudentPasswordTextBox);
            }
            else if (addStudentdeptComboBox.Text.Equals(""))
            {
                MessageBox.Show(@"PLEASE SELECT A DEPARTMENT", @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string dept;
                if (addStudentdeptComboBox.Text.Equals("C.S.E"))
                {
                    dept = "CSE";
                }
                else if (addStudentdeptComboBox.Text.Equals("E.E.E"))
                {
                    dept = "EEE";
                }
                else 
                {
                    dept = "BBA";
                }
                string address = addStudentaddressTextBox.Text;
                string sid = addStudentIdTextBox.Text;
                string ssemister = addStudentSemisterDurationTextBox.Text;
                string phoneno = addStudentphonenoTextBox.Text;
                string password = addStudentPasswordTextBox.Text;
                string nationality = addStudentNationalityTextBox.Text;
                string gender = addStudentgenderComboBox.Text;
                string fatherName = addStudentfather_nameTextBox.Text;
                string dateOfBirth = addStudentdate_of_birthDateTimePicker.MaxDate.ToShortDateString();
                string country = addStudentcountryTextBox.Text;
                string city = addStudentcityTextBox.Text;
                string email = addStudentEmailTextBox.Text;
                string studentName = addStudent_nameTextBox.Text;
                try
                {
                    DataAccess.ExecuteSql("insert into student values ('"+sid+"','"+password+"','"+studentName+"','"+fatherName+"','"+gender+"','"+dateOfBirth+"','"+phoneno+"','"+address+"','"+country+"','"+city+"','"+nationality+"','"+email+"','"+dept+"','"+ssemister+"')");
                    MessageBox.Show(@"This Student Is Added");
                    studentCourselistBox2.Enabled = true;
                    sudentAddCourseButton.Enabled = true;
                    submitButton.Enabled = true;
                }
                catch (Exception)
                {

                    MessageBox.Show(@"This Student ID Is Alrady Added");

                }

            }

        }

        private void submitButton_Click(object sender, EventArgs e)
        {
           
            foreach (TabPage t in addStudentPanelTab.TabPages)
            {
                foreach (Control c in t.Controls)
                {
                    if (c is TextBox || c is RichTextBox || c is ComboBox)
                    {
                        c.Text = string.Empty;

                    }
                    
                }
            }

            MessageBox.Show(@"All Data Submited");
            
            studentCourseListBox1.Items.Clear();
            studentCourselistBox2.DataSource = null;
            studentCourselistBox2.Items.Clear();
            studentCourselistBox2.Enabled = false;
            sudentAddCourseButton.Enabled = false;
            submitButton.Enabled = false;

            addStudentPanelTab.SelectTab(addStudentPaneltabPage1);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string substring = SearchSTextBox.Text.Substring(0);
            if (substring.Length == 10 && substring[2] == '-' && substring[8] == '-')
            {
                DataTable dt =
                    DataAccess.GetDataTable("select * from student where student_id='" + SearchSTextBox.Text + "'");
                searchSRemoveDataGridView.DataSource = dt;
            }
            else
            {
                
                MessageBox.Show(@"Invalid ID", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void searchSRemoveButton_Click(object sender, EventArgs e)
        {
            if (searchSRemoveDataGridView.Rows.Count.Equals(0))
            {
                MessageBox.Show(@"Nothing Selected", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (SearchSTextBox.Text.Equals("")==false)
            {
                DataAccess.ExecuteSql("delete from student where student_id='" + SearchSTextBox.Text + "'");
                searchSRemoveDataGridView.Rows.RemoveAt(0);
                MessageBox.Show(@"Student Removed", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(@"Nothing Selected", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void removeTeacherButton_Click(object sender, EventArgs e)
        {
          FRemovePanel.BringToFront();
          sftextBox.DataSource = DataAccess.GetDataTable("select faculty_id from faculty");
            sftextBox.DisplayMember = "faculty_id";
        }

        private void sfsearchButton_Click(object sender, EventArgs e)
        {
            string substring = sftextBox.Text.Substring(0);
            if (substring.Length == 10 && substring[3] == '-' && substring[7] == '-')
            {
                DataTable dt =
                     DataAccess.GetDataTable("select * from faculty where faculty_id='" + sftextBox.Text + "'");
                sfdataGridView.DataSource = dt;
            }
            else
            {
                MessageBox.Show(@"Invalid ID", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sfRemoveButton_Click(object sender, EventArgs e)
        {
            if (sfdataGridView.Rows.Count.Equals(0))
            {
                MessageBox.Show(@"Nothing Selected", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sftextBox.Text.Equals("")==false)
            {
                DataAccess.ExecuteSql("delete from faculty where faculty_id='" + sftextBox.Text + "'");
                sfdataGridView.Rows.RemoveAt(0);
                MessageBox.Show(@"Faculty Removed", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(@"Nothing Selected", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            
        }

        private void addCourseADDbutton_Click(object sender, EventArgs e)
        {
            string dept = addCourseDEPTcomboBox.Text;
            string creadit = addCourseCeaditTextBox.Text;
            string cname = addCourseNametextBox.Text;
            string cid = addCourseIDtextBox.Text;
            string prerq = addCoursePREREQUISITEtextBox.Text;
            if (cname.Equals(""))
            {
                addCourseNametextBox.BackColor = Color.LightCoral;
                return;
            }
            if (cid.Equals(""))
            {
                addCourseIDtextBox.BackColor = Color.LightCoral;
                return;
            }
            if (creadit.Equals(""))
            {
                addCourseCeaditTextBox.BackColor = Color.LightCoral;
                return;
            }
            try
            {

            
            if (dept.Equals("C.S.E"))
            {
                DataAccess.ExecuteSql("insert into CSE_course_list values('" + cid + "','" + cname + "','" + creadit +
                                      "','" + prerq + "')");
                DataAccess.ExecuteSql("insert into CSE_faculty_course values('" + cid + "','" + cname + "','" + creadit +
                                      "')");
            }
            else if (dept.Equals("E.E.E"))
            {
                DataAccess.ExecuteSql("insert into eee_course_list values('" + cid + "','" + cname + "','" + creadit +
                                     "','" + prerq + "')");
                DataAccess.ExecuteSql("insert into eee_faculty_course values('" + cid + "','" + cname + "','" + creadit +
                                    "')");
            }
            else if (dept.Equals("B.B.A"))
            {
                DataAccess.ExecuteSql("insert into bba_course_list values('" + cid + "','" + cname + "','" + creadit +
                                     "','" + prerq + "')");
                DataAccess.ExecuteSql("insert into bba_faculty_course values('" + cid + "','" + cname + "','" + creadit +
                                     "')");
            }
            else if (dept.Equals(""))
            {
                MessageBox.Show(@"SELECT A DEPARTMENT", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show(@"Course Is Added");
            }
            catch (Exception)
            {

                MessageBox.Show(@"Course IS Present In The List",@"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void addCourseCLEARbutton_Click(object sender, EventArgs e)
        {
           
                foreach (Control c in addCoursePanel.Controls)
                {
                    if (c is TextBox || c is RichTextBox || c is ComboBox)
                    {
                        c.Text = string.Empty;
                        c.BackColor = Color.White;
                    }

                }
            addCoursePREREQUISITEtextBox.Text = @"-";
        }

        private void addCourseButton_Click(object sender, EventArgs e)
        {
            addCoursePanel.BringToFront();
        }

        private void removeCourseButton_Click(object sender, EventArgs e)
        {
            removeCpanel.BringToFront();
        }

        private void addCoursePREREQUISITEtextBox_TextChanged(object sender, EventArgs e)
        {
            addCoursePREREQUISITEtextBox.SelectAll();
        }

        private void removeCsearchbutton_Click(object sender, EventArgs e)
        {
            string dept = removeCdeptcomboBox.Text;
           
            string cid = removeCcidtextBox.Text;

            if (dept.Equals("C.S.E"))
            {
                DataTable dt =
                     DataAccess.GetDataTable("select * from CSE_course_list where course_id='" + cid + "'");
                removeCdataGridView.DataSource = dt;
            }
            else if (dept.Equals("E.E.E"))
            {
                DataTable dt =
                     DataAccess.GetDataTable("select * from eee_course_list where course_id='" + cid + "'");
                removeCdataGridView.DataSource = dt;
            }
            else if (dept.Equals("B.B.A"))
            {
                DataTable dt =
                     DataAccess.GetDataTable("select * from bba_course_list where course_id='" + cid + "'");
                removeCdataGridView.DataSource = dt;
            }
            else if (dept.Equals(""))
            {
                MessageBox.Show(@"SELECT A DEPARTMENT", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
             if (removeCdataGridView.Rows.Count.Equals(0))
            {
                MessageBox.Show(@"INVALID COURSE ID", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeCremovebutton_Click(object sender, EventArgs e)
        {
            string dept = removeCdeptcomboBox.Text;
             
            string cid = removeCcidtextBox.Text;
            if (removeCdataGridView.Rows.Count.Equals(0))
            {
                MessageBox.Show(@"NO COURSE TO REMOVE", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cid.Equals("") == false)
            {

                if (dept.Equals("C.S.E"))
                {
                    DataAccess.ExecuteSql("delete from CSE_course_list where course_id='" + cid + "'");
                    DataAccess.ExecuteSql("delete from CSE_faculty_course where course_id='" + cid + "'");
                    removeCdataGridView.Rows.RemoveAt(0);

                    MessageBox.Show(@"COURSE REMOVED", @"iNFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dept.Equals("E.E.E"))
                {
                    DataAccess.ExecuteSql("delete from EEE_course_list where course_id='" + cid + "'");
                    DataAccess.ExecuteSql("delete from EEE_faculty_course where course_id='" + cid + "'");
                    removeCdataGridView.Rows.RemoveAt(0);
                    MessageBox.Show(@"COURSE REMOVED", @"iNFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dept.Equals("B.B.A"))
                {
                    DataAccess.ExecuteSql("delete from BBA_course_list where course_id='" + cid + "'");
                    DataAccess.ExecuteSql("delete from BBA_faculty_course where course_id='" + cid + "'");
                    removeCdataGridView.Rows.RemoveAt(0);
                    MessageBox.Show(@"COURSE REMOVED", @"iNFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(@"NO COURSE TO REMOVE", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
           
        }

        private void addCourseNametextBox_TextChanged(object sender, EventArgs e)
        {
            addCourseNametextBox.BackColor = Color.White;
        }

        private void addCourseIDtextBox_TextChanged(object sender, EventArgs e)
        {
            addCourseIDtextBox.BackColor = Color.White;
        }

        private void addCourseCeaditTextBox_TextChanged(object sender, EventArgs e)
        {
            addCourseCeaditTextBox.BackColor = Color.White;
        }

        private void mSTSearchButton_Click(object sender, EventArgs e)
        {
            if (mSTIDtextBox.Text.Equals(""))
            {
                MessageBox.Show(@"ID IS EMPTY", @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                mSTdataGridView.DataSource = DataAccess.GetDataSet("select * from student where student_id='" + mSTIDtextBox.Text + "'").Tables[0];
            }
        }

        private void mSTSubmitButton_Click(object sender, EventArgs e)
        {
            if (mSTdataGridView.Rows.Count.Equals(0))
            {
                MessageBox.Show(@"NOTHING TO SUBMIT", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                

            }
            else
            {
                DataAccess.Submitdataset();
                MessageBox.Show(@"SUBMITED", @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void mFSbutton_Click(object sender, EventArgs e)
        {
            if (mFIDtextBox.Text.Equals(""))
            {
                MessageBox.Show(@"ID IS EMPTY", @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
               mFdataGridView.DataSource = DataAccess.GetDataSet("select * from faculty where faculty_id='" + mFIDtextBox.Text + "'").Tables[0];
            }
        }

        private void mFSubmitbutton1_Click(object sender, EventArgs e)
        {
            if (mFdataGridView.Rows.Count.Equals(0))
            {
                MessageBox.Show(@"NOTHING TO SUBMIT", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
            {
                DataAccess.Submitdataset();
                MessageBox.Show(@"SUBMITED", @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void modifyTeacherButton_Click(object sender, EventArgs e)
        {
            mFpanel.BringToFront();
            mFIDtextBox.DataSource = DataAccess.GetDataTable("select faculty_id from faculty");
            mFIDtextBox.DisplayMember = "faculty_id";
        }

        private void addStudentIdTextBox_TextChanged(object sender, EventArgs e)
        {
            addStudentIdTextBox.BackColor = Color.White;
        }

        private void addStudentPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            addStudentPasswordTextBox.BackColor = Color.White;
        }

        private void sRegONbutton_Click(object sender, EventArgs e)
        {
            if (preRegScomboBox.Text.Equals("") == false && preRegDtextBox.Text.Equals("") == false)
            {
                DataAccess.ExecuteSql("update sprereg set status=1 , semister='" + preRegScomboBox.Text +" "+
                                      preRegDtextBox.Text + "'");
                sRegONbutton.BackColor = Color.GreenYellow;
                sRegOFFbutton.BackColor = Color.Red;
            }
            else
            {
                MessageBox.Show(@"please fill both fild".ToUpper(), @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sRegOFFbutton_Click(object sender, EventArgs e)
        {
            DataAccess.ExecuteSql("update sprereg set status=0 ");
            sRegONbutton.BackColor = Color.Red;
            sRegOFFbutton.BackColor = Color.GreenYellow;
        }

        private void fRegONbutton_Click(object sender, EventArgs e)
        {
            if (preRegScomboBox.Text.Equals("") == false && preRegDtextBox.Text.Equals("") == false)
            {
                DataAccess.ExecuteSql("update fprereg set status=1 , semister='" + preRegScomboBox.Text +" "+
                                      preRegDtextBox.Text + "'");
                fRegONbutton.BackColor = Color.GreenYellow;
                fRegOFFbutton.BackColor = Color.Red;
            }
            else
            {
                MessageBox.Show(@"please fill both fild".ToUpper(), @"INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void fRegOFFbutton_Click(object sender, EventArgs e)
        {
            DataAccess.ExecuteSql("update fprereg set status=0 ");
            fRegONbutton.BackColor = Color.Red;
            fRegOFFbutton.BackColor = Color.GreenYellow;
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            DataTable dt = DataAccess.GetDataTable("select name from admin where admin_id='" + _adminId + "'");
            label72.Text = dt.Rows[0]["name"].ToString();
        }

        private void preRegDtextBox_TextChanged(object sender, EventArgs e)
        {
            toolTip.Show("yyyy-yyyy", preRegDtextBox);
        }

      
    }
}
