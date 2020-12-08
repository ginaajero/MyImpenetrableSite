using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyImpenetrableSite
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e) =>
            //  lblSearchTerms.Text = "You searched for: " + txtSearchTerms.Text;
            lblSearchTerms.Text = $"You searched for: {txtSearchTerms.Text}";
    }
}