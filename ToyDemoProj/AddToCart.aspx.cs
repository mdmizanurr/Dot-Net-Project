using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using ToyDemoProj.Logic;        

namespace ToyDemoProj
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request.QueryString["ProductID"];
            int productId;
            if(!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId))
            {
                using (ShoppingCartActions userShoppingCart = new ShoppingCartActions())
                {
                    userShoppingCart.AddToCart(Convert.ToInt16(rawId));
                }
                
            }

            else
            {
                Debug.Fail("ERROR: We shoud never get to AddToCart.aspx without a productId. ");
                throw new Exception("ERROR: It is illegal to load AddToCart.aspx without setting a productId.");
            }

            Response.Redirect("ShoppingCart.aspx");
        }
    }
}