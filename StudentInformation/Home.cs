using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentInformation
{
    public partial class Home : Form
    {
        List<string> ids = new List<string> { };
        List<string> names = new List<string> { };
        List<string> mobiles = new List<string> { };
        List<int> ages = new List<int> { };
        List<string> addresses = new List<string> { };
        List<double> gpaPoints = new List<double> { };

        public Home()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(idTextBox.Text) && !ids.Contains(idTextBox.Text) && idTextBox.TextLength == 4 && !String.IsNullOrEmpty(nameTextBox.Text) && nameTextBox.TextLength<31 && !String.IsNullOrEmpty(mobileTextBox.Text) && !mobiles.Contains(mobileTextBox.Text) && mobileTextBox.TextLength == 11 && !String.IsNullOrEmpty(ageTextBox.Text) && !String.IsNullOrEmpty(gpaPointTextBox.Text) && Convert.ToDouble(gpaPointTextBox.Text) <= 4.00)
                {
                    AddStudent(idTextBox.Text, nameTextBox.Text, mobileTextBox.Text, Convert.ToInt32(ageTextBox.Text), addressTextBox.Text, Convert.ToDouble(gpaPointTextBox.Text));

                    Reset();
                    ShowStudent(names.Count - 1, names.Count);
                    MessageBox.Show("Student Added Successfully !");
                }
                else
                {
                    if (String.IsNullOrEmpty(idTextBox.Text) || idTextBox.TextLength != 4)
                        MessageBox.Show("Id is required and must be 4 charecter !");
                    else if (ids.Contains(idTextBox.Text))
                        MessageBox.Show("Id is alreay saved !");
                    else if (String.IsNullOrEmpty(nameTextBox.Text) || nameTextBox.TextLength > 30)
                        MessageBox.Show("Name is required and maximum 30 charecter !!");
                    else if (String.IsNullOrEmpty(mobileTextBox.Text) || mobileTextBox.TextLength != 11)
                        MessageBox.Show("Please enter a valid Mobile number !!");
                    else if (mobiles.Contains(mobileTextBox.Text))
                        MessageBox.Show("Mobile is alreay saved !");
                    else if (String.IsNullOrEmpty(gpaPointTextBox.Text) || Convert.ToDouble(gpaPointTextBox.Text) > 4)
                        MessageBox.Show("Please enter GPA Point in range (0 - 4)");

                    return;

                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            double maxGpa = gpaPoints[0], minGpa = gpaPoints[0], totalGpa = 0;
            int maxGpaIndex=0, minGpaIndex=0;

            try
            {
                for (int index = 1; index < ids.Count; index++)
                {
                    if (gpaPoints[index] > maxGpa)
                    {
                        maxGpa = gpaPoints[index];
                        maxGpaIndex = index;
                    }
                        
                    if(gpaPoints[index] < minGpa)
                    {
                        minGpa = gpaPoints[index];
                        minGpaIndex = index;
                    }
                        

                    totalGpa += gpaPoints[index];
                    
                }

                Reset();
                ShowStudent(0, names.Count);

                maxGpaTextBox.Text = String.Format("{0:0.00}", gpaPoints[maxGpaIndex]);
                maxGpaNameTextBox.Text = names[maxGpaIndex];
                minGpaTextBox.Text = String.Format("{0:0.00}", gpaPoints[minGpaIndex]);
                minGpaNameTextBox.Text = names[minGpaIndex];

                averageGpaTextBox.Text = String.Format("{0:0.00}", totalGpa/ids.Count);
                totalGpaTextBox.Text = String.Format("{0:0.00}", totalGpa);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string searchKeyword = searchTextBox.Text;
                int index;

                if (idRadioButton.Checked == true)
                    index = SearchById(searchKeyword);
                else if (nameRadioButton.Checked == true)
                    index = SearchByName(searchKeyword);
                else
                    index = SearchByMobile(searchKeyword);

                Reset();

                if (index != ids.Count)
                    ShowStudent(index, index + 1);
                else
                {
                    MessageBox.Show("Student not Found !!");
                    return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void AddStudent(string id, string name, string mobile, int age, string address, double gpa)
        {
            ids.Add(id);
            names.Add(name);
            mobiles.Add(mobile);
            ages.Add(age);
            addresses.Add(address);
            gpaPoints.Add(gpa);
        }

        private void ShowStudent(int startIndex, int endIndex)
        {
            string message = "ID\tName\t\t\tMobile\t\tAge\tAddress\t\tGPA Point\n";
            for(int index = startIndex; index < endIndex; index++)
            {
                message +=  ids[index]+ "\t" + names[index]+ "\t\t" + mobiles[index]+ "\t" + ages[index] + "\t" + addresses[index] + "\t\t" + gpaPoints[index] +"\n";
            }

            showRichTextBox.Text = message;
        }

        private void Reset()
        {
            idTextBox.Text = "";
            nameTextBox.Text = "";
            mobileTextBox.Text = "";
            ageTextBox.Text = "";
            addressTextBox.Text = "";
            gpaPointTextBox.Text = "";
            //searchTextBox.Text = "";
            maxGpaTextBox.Text = "";
            maxGpaNameTextBox.Text = "";
            minGpaTextBox.Text = "";
            minGpaNameTextBox.Text = "";
            averageGpaTextBox.Text = "";
            totalGpaTextBox.Text = "";
            showRichTextBox.Text = "";

        }

        private int SearchById(string searchKeyword)
        {
            int index;
            
            for(index = 0; index < ids.Count; index++)
            {
                if (ids[index] == searchKeyword)
                    break;
            }
            return index;
        }

        private int SearchByName(string searchKeyword)
        {
            int index;
            for (index = 0; index < ids.Count; index++)
            {
                if (names[index] == searchKeyword)
                    break;
            }

            return index;
        }

        private int SearchByMobile(string searchKeyword)
        {
            int index;
            for (index = 0; index < ids.Count; index++)
            {
                if (mobiles[index] == searchKeyword)
                    break;
            }
            return index;
        }

        
    }
}
