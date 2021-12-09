using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyWindowsFormsApp
{
    public partial class StudentsForm : Form
    {
        List<Student> studentsList = new List<Student>();
        List<string> lastNameList = new List<string>();

        public StudentsForm()
        {
            InitializeComponent();
            //this.Load += delegate { RefreshGridView(); };
        }
        private void StudentsForm_Load(object sender, EventArgs e)
        {
            studentsList.Add(new Student() { LastName = "Doe", FirstName = "Jhon", Age = 25 });
            studentsList.Add(new Student() { LastName = "Davis", FirstName = "Jane", Age = 27 });
            studentsList.Add(new Student() { LastName = "Parker", FirstName = "Sara", Age = 21 });
            studentsList.Add(new Student() { LastName = "Simpson", FirstName = "Jessica", Age = 21 });
            studentsList.Add(new Student() { LastName = "Washington", FirstName = "Andre", Age = 21 });
            studentsList.Add(new Student() { LastName = "Wilson", FirstName = "Garry", Age = 21 });
            studentsList.Add(new Student() { LastName = "Wiliams", FirstName = "Amanda", Age = 21 });
            studentsList.Add(new Student() { LastName = "Wanderbuilt", FirstName = "Karen", Age = 21 });
            studentsList.Add(new Student() { LastName = "Walker", FirstName = "Monika", Age = 21 });
            studentsList.Add(new Student() { LastName = "Wilmington", FirstName = "Andre", Age = 21 });
            grdStudent.AutoGenerateColumns = false;
            grdStudent.DataSource = studentsList;
            ReadSelectedRow();

            foreach (var item in studentsList)
            {
                lastNameList.Add(item.LastName);
            }
            {
                cmbLastName.ValueMember = "Id";
                cmbLastName.DisplayMember = "LastName";
                cmbLastName.DataSource = lastNameList;
                cmbLastName.SelectedIndex = -1;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.LastName = txtLastName.Text;
            student.FirstName = txtFirstName.Text;
            student.Age = Convert.ToInt32(txtAge.Text);
            studentsList.Add(student);
            RefreshGridView();
            ReadSelectedRow();
        }
        private void btnRemove_Clik(object sender, EventArgs e)
        {
            Guid id = Guid.Parse(txtId.Text);
            Student student = studentsList.Find(x => x.Id == id);
            studentsList.Remove(student);
            RefreshGridView();
            ReadSelectedRow();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Guid id = Guid.Parse(txtId.Text);
            Student student = studentsList.Find(x => x.Id == id);
            int age = Convert.ToInt32(txtAge.Text);
            student.Age = age;
            student.LastName = txtLastName.Text;
            student.FirstName = txtFirstName.Text;
            RefreshGridView();
            ReadSelectedRow();
        }
        private void grdStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbLastName.SelectedItem = txtLastName.Text.ToString();
            ReadSelectedRow();
        }
        private void RefreshGridView()
        {
            grdStudent.DataSource = null;
            grdStudent.DataSource = studentsList;
        }
        private void ReadSelectedRow()
        {
            if (grdStudent.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grdStudent.SelectedRows[0];
                txtId.Text = row.Cells["id"].Value.ToString();
                txtLastName.Text = row.Cells["stLastName"].Value.ToString();
                txtFirstName.Text = row.Cells["stFirstName"].Value.ToString();
                txtAge.Text = row.Cells["stAge"].Value.ToString();
            }
        }

        private void grdStudent_SelectionChanged(object sender, EventArgs e)
        {
            if (grdStudent.SelectedRows.Count > 0)
            {
                cmbLastName.SelectedItem = txtLastName.Text.ToString();
                ReadSelectedRow();
            }
        }

        private void cmbLastName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbLastName.SelectedIndex != -1)
            {
                string lastName = cmbLastName.SelectedValue.ToString();
                Student student = studentsList.FirstOrDefault(x => x.LastName == lastName);
                txtId.Text = student.Id.ToString();
                txtLastName.Text = student.LastName.ToString();
                txtFirstName.Text = student.FirstName.ToString();
                txtAge.Text = student.Age.ToString();
                foreach (DataGridViewRow row in grdStudent.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(lastName))
                    {
                        row.Selected = true;
                    }
                }
            }
        }
    }

    public class Student
    {
        public Student()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
