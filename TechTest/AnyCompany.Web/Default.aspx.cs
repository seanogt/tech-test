using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AnyCompany;

namespace AnyCompany.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Random r = new Random();
                lblorderid.Text = r.Next(100, 9999).ToString();
            }
           
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

               
                Order objorder = new Order();
                objorder.CustomerId = Convert.ToInt32(ddlcustomer.SelectedValue);

                objorder.OrderId = Convert.ToInt32(lblorderid.Text);
                objorder.Amount = Convert.ToDouble(txtorderamt.Text);
                //objorder.VAT = Convert.ToDouble(txtVat.Text);

                OrderService obj = new OrderService();
                obj.PlaceOrder(objorder, Convert.ToInt32(ddlcustomer.SelectedValue));
                string message = "Order Placed Successfully.";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                Reset();
                GridView1.DataBind();
                }
                catch 
                {

                   
                }

            }

        }
        private void Reset()
        {
            Random r = new Random();
            lblorderid.Text = r.Next(100, 9999).ToString();
            txtorderamt.Text = "";
            ddlcustomer.SelectedValue="0";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}