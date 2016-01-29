using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using System.Web.Security;

namespace BugTrackingSystem.Administrator
{
    public partial class Tester : System.Web.UI.Page
    {
		private Data.Tester _OldTester;
		private Data.Tester _NewTester;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                header.InnerText = "Нов тестер";
                if (OldTester != null) 
                {
                    header.InnerText = "Редакция на тестер";
                    LoadContent();
                }
            }
        }

		protected Data.Tester OldTester 
        {
            get 
            {
                if (_OldTester == null) 
                {
                    if (Page.Request.QueryString["testerId"] != null)
                    {
                        _OldTester = TestersDBManager.GetTesterByTesterId(int.Parse(Page.Request.QueryString["testerId"]));
                    }
                }

                return _OldTester;
            }
        }

		protected Data.Tester NewTester 
        {
            get 
            {
                if (_NewTester == null) 
                {
					_NewTester = new Data.Tester();
                    _NewTester.Username = txtUsername.Text;
                    _NewTester.Password = txtPassword.Text;
                    _NewTester.Name = txtName.Text;
                    _NewTester.Surname = txtSurname.Text;
                    _NewTester.Email = txtEmail.Text;
                    _NewTester.Phone = txtPhone.Text;
                    if (OldTester != null)
                    {
                        _NewTester.TesterId = OldTester.TesterId;
                        _NewTester.Password = OldTester.Password;
                    }
                }

                return _NewTester;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) 
        {
            Response.Redirect("Testers.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e) 
        {
            if (Page.Request.QueryString["testerId"] != null) 
            {
                cuvUsername.Enabled = false;
            }
            Page.Validate();
            if (Page.IsValid)
            {
                // insert new tesnter in database
                if (OldTester == null)
                {
					Data.Tester tester = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName);
                    tester.LastAction = "Създаване на тестер №" + TestersDBManager.Insert(NewTester);
                    TestersDBManager.Update(tester);
                    Membership.CreateUser(NewTester.Username, NewTester.Password, NewTester.Email);
                    Roles.AddUserToRole(NewTester.Username, "Tester");
                }
                // update existing one
                else
                {
                    TestersDBManager.Update(NewTester);
					Data.Tester tester = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName);
                    tester.LastAction = "Редакция на тестер №" + NewTester.TesterId;
                    TestersDBManager.Update(tester);
                    // update data in membership provider
                    MembershipUser user = Membership.GetUser(OldTester.Username);
                    user.Email = NewTester.Email;
                    Membership.UpdateUser(user);
                }

                Response.Redirect("Testers.aspx");
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tester/ChangePassword.aspx?testerId=" + OldTester.TesterId);
        }

        private void LoadContent()
        {
            txtUsername.Visible = false;
            reqUsername.Enabled = false;
            lblUsernameEditMode.Text = OldTester.Username;
            lblUsernameEditMode.Visible = true;
            rowPassword.Visible = false;
            rowRepeatPassword.Visible = false;
            txtName.Text = OldTester.Name;
            txtSurname.Text = OldTester.Surname;
            txtEmail.Text = OldTester.Email;
            txtPhone.Text = OldTester.Phone;
            btnChangePassword.Visible = true;
        }

        protected void cuvUsername_ServerValidate(object sender, ServerValidateEventArgs e) 
        {
            if (!Char.IsDigit(txtUsername.Text.Trim()[0]))
            {
                if (TestersDBManager.GetTesterByUsername(txtUsername.Text) == null)
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    CustomValidator cusValidator = (CustomValidator)sender;
                    cusValidator.ErrorMessage = "Съществува тестер с въведеното име";
                }
            }
            else 
            {
                e.IsValid = false;
                CustomValidator cusValidator = (CustomValidator)sender;
                cusValidator.ErrorMessage = "Потребителското име трябва да започва с буква";
            }
        }
    }
}