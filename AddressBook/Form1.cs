using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AddressBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Thread t = new Thread(new ThreadStart(Program.ShowSplashScreen)); //new instance of thread that executes ShowSplashScreen
            t.Start(); //Starts the Splash screen
            Thread.Sleep(1000);

            InitializeComponent();


            
            t.Abort();
            

        }

        List<Program.Contact> contactList = new List<Program.Contact>(); //creates new list "contactList" from "Contact" class


        string filePath = ""; //global string variable "filePath"


        //ADD BUTTON EVENT
        private void btnAdd_Click(object sender, EventArgs e)
        {

            Program.Contact contact = new Program.Contact(); //new instance of Contact is created called contact


            contact.FirstName = txtFirstName.Text; //FirstName is set to text box
            contact.LastName = txtLastName.Text; //LastName is set to text box
            contact.Phone = txtPhone.Text; //Phone is set to text box
            contact.Email = txtEmail.Text; //Email is set to text box
            contact.Business = cboxBusiness.Text; //Business is set to combo box
            contact.Notes = txtNotes.Text; //Notes is set to text box
            string text = contact.FirstName + " | " + contact.LastName + " | " + contact.Phone + " | " + contact.Email + " | " + contact.Business + " | " + contact.Notes; //text eqauls everthing in the instance
            contactList.Add(contact); //add everything in the instance to "contactList"
            dataGridView1.Rows.Add(txtFirstName.Text, txtLastName.Text, txtPhone.Text, txtEmail.Text, cboxBusiness.Text, txtNotes.Text); //add everything to datagrid 
            txtFirstName.Clear(); //clear text box
            txtLastName.Clear(); //clear text box
            txtPhone.Clear(); //clear text box
            txtEmail.Clear(); //clear text box
            cboxBusiness.SelectedIndex = -1; //clears combo box
            txtNotes.Clear(); //clear text box
            txtFirstName.Focus(); //focus text box
            
        }

        
        //REMOVE BUTTON EVENT
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1) //if something in the datagrid is selected execute:
            {
                int index = dataGridView1.CurrentRow.Index; //int "index" is set to whats selected in list box
                dataGridView1.Rows.RemoveAt(index); //remove row at index of datagrid
                contactList.RemoveAt(index); //removes item in "contactList" at int "index"
                txtFirstName.Clear(); //clear text box
                txtLastName.Clear(); //clear text box
                txtPhone.Clear(); //clear text box
                txtEmail.Clear(); //clear text box
                txtNotes.Clear(); //clear text box
                cboxBusiness.SelectedIndex = -1; //clears combo box
                txtFirstName.Focus(); //focus text box
            }
        }


        //SAVE AS MENU BUTTON EVENT
        private void btnSave_Click(object sender, EventArgs e)
        {
            try //try this:
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) //if the user clickes the "Save"/"Ok" button in dialog box execute:
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName)) //creates stream writer instance called "sw" that writes to the file the user chooses
                    {
                        foreach (Program.Contact line in contactList) //for each item in list:
                        {
                            sw.WriteLine(line.FirstName + " | " + line.LastName + " | " + line.Phone + " | " + line.Email + " | " + line.Business + " | " + line.Notes); //writes each item of list to file
                        }
                        filePath = saveFileDialog1.FileName; //file path is set to the file the user chose
                        txtFirstName.Focus(); //focus firstname text box
                    }
                }
            }
            catch(Exception ex) //if try doesnt work do this
            {
                MessageBox.Show("Error: " + ex.Message); //error message
            }
        }

        //OPEN MENU BUTTON EVENT
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try// try this
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK) //if the user clickes the "Save"/"Ok" button in dialog box execute:
                {
                    contactList.Clear(); //clear contactList 
                    dataGridView1.Rows.Clear(); //clear datagrid
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName)) //creates stream reader instance called "sr" that writes to the file the user chooses
                    {
                        while (!sr.EndOfStream) //while stream reader is not at end of file execute:
                        {
                            string line = sr.ReadLine(); //line equals the line stream reader just read

                            Program.Contact c = new Program.Contact(); //new instance of contact is created called "c"
                            string[] data = line.Split('|'); //array "data" is created and each index is split by "|"
                            if (data.Length == 6) //if data array has 6 items execute:
                            {
                                c.FirstName = data[0]; //FirstName is set to item 1 in array
                                c.LastName = data[1]; //LastName is set to item 2 in array
                                c.Phone = data[2]; //Phone is set to item 3 in array
                                c.Email = data[3]; //Email is set to item 4 in array
                                c.Business = data[4]; //Business is set to item 5 in array
                                c.Notes = data[5]; //Notes is set to item 6 in array
                                dataGridView1.Rows.Add(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString()); //ass each item to datagrid
                                contactList.Add(c); //add all the items to contactList
                            }
                        }
                        filePath = openFileDialog1.FileName; //filePath equals to the file the user chose
                        txtFirstName.Focus(); //focus first name text box
                    }
                }
            }
            catch (Exception ex) //if try doesnt work do this
            {
                MessageBox.Show("Error: " + ex.Message); //error message
            }
        }

        //EXIT MENU BUTTON EVENT
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Application.Exit(); //exit application 
        }


        //SAVE MENU BUTTON EVENT
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try //try this:
            {
                if (filePath.Length != 0) //if the text length of file path (aka there is a file path) execute:
                {
                    using (StreamWriter sw = new StreamWriter(filePath)) //creates stream writer instance called "sw" that writes to the file the user chooses
                    {
                        foreach (Program.Contact line in contactList) //for each item in list:
                        {
                            sw.WriteLine(line.FirstName + " | " + line.LastName + " | " + line.Phone + " | " + line.Email + " | " + line.Business + " | " + line.Notes); //writes each item of list to file
                        }
                        txtFirstName.Focus(); //focus firstname text box
                    }
                }
                //using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName)) //creates stream writer instance called "sw" that writes to the file the user chooses
                //{
                //    foreach (Program.Contact line in contactList) //for each item in list:
                //    {
                //        sw.WriteLine(line.FirstName + " | " + line.LastName + " | " + line.Phone + " | " + line.Email + " | " + line.Business + " | " + line.Notes); //writes each item of list to file
                //    }
                //    filePath = saveFileDialog1.FileName; //file path is set to the file the user chose
                //    txtFirstName.Focus(); //focus firstname text box
                //}
                else
                {
                    try //try this:
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK) //if the user clickes the "Save"/"Ok" button in dialog box execute:
                        {
                            using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName)) //creates stream writer instance called "sw" that writes to the file the user chooses
                            {
                                foreach (Program.Contact line in contactList) //for each item in list:
                                {
                                    sw.WriteLine(line.FirstName + " | " + line.LastName + " | " + line.Phone + " | " + line.Email + " | " + line.Business + " | " + line.Notes); //writes each item of list to file
                                }
                                filePath = saveFileDialog1.FileName; //file path is set to the file the user chose
                                txtFirstName.Focus(); //focus firstname text box
                            }
                        }
                    }
                    catch (Exception ex) //if try doesnt work do this
                    {
                        MessageBox.Show("Error: " + ex.Message); //error message
                    }

                }
            }
            catch (Exception ex) //if try doesnt work do this
            {
                MessageBox.Show("Error: " + ex.Message); //error message
            }
        }

        //CELL CLICK EVENT
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == 0) //if there is data in datagrid execute:
            {
                //fill text boxes with the rows contents
                txtFirstName.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtLastName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtPhone.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                cboxBusiness.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtNotes.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            
        }


        //CELL DOUBLE CLICK EVENT
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show(dataGridView1.CurrentRow.Cells[5].Value.ToString()); //show notes inside message box
        }

        //TEXT CHANGE IN SEARCH BOX EVENT
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++) //for every row execute:
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++) //for every column execute:
                {
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().ToUpper().Contains(txtSearch.Text.ToUpper())) //if the text in the cell on the row contains the text in the search text box execute:
                    {
                        dataGridView1.Rows[i].Visible = true; //row is visible
                        break; //break from loop
                    }
                    else //if not
                    {
                        dataGridView1.Rows[i].Visible = false; //the row is invisible
                    }
                }
            }
        }
    }
}
