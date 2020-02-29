using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToyDemoProj.Models;
using ToyDemoProj.Logic;


namespace ToyDemoProj.Logic
{
    public class ShoppingCartActions : IDisposable
    {
        public string ShoppingCatrId { get; set; }

        private ProductContext _db = new ProductContext();

        public const string CartSessionKey = "CartId";

        public void AddToCart (int id)
        {
            ShoppingCatrId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCatrId 
                && c.ProductId == id );

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCatrId,
                    Product = _db.Products.SingleOrDefault(
                        p => p.ProductID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _db.ShoppingCartItems.Add(cartItem);

            }

            else
            {
                cartItem.Quantity++;
            }
            _db.SaveChanges();

        }


        public void Dispose()
        {

            if ( _db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }


        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return HttpContext.Current.Session[CartSessionKey].ToString();

        }



       public List<CartItem> GetCartItems()
        {
            ShoppingCatrId = GetCartId();

            return _db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCatrId).ToList();
        }


        public decimal GetTotal()
        {

            ShoppingCatrId = GetCartId();

            decimal? total = decimal.Zero;

            total = (decimal?)(from cartItems in _db.ShoppingCartItems
                               where cartItems.CartId == ShoppingCatrId
                               select (int?)cartItems.Quantity * 
                               cartItems.Product.UnitPrice).Sum();

            return total ?? decimal.Zero;

        }


        public ShoppingCartActions GetCart(HttpContext context)
        {
            using (var cart = new ShoppingCartActions())
            {
                cart.ShoppingCatrId = cart.GetCartId();
                return cart;
            }
        }




        internal void UpdateShoppingCartDatabase(string cartId, object cartItemUpdates)
        {
            throw new NotImplementedException();
        }


        //

        public void UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[] CartItemUpdates)
        {
            using (var db = new ToyDemoProj.Models.ProductContext())
            {
                try
                {
                    int CartItemCount = CartItemUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach (var cartItem in myCart)
                    {
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (cartItem.Product.ProductID == CartItemUpdates[i].ProductId)
                            {
                                if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, cartItem.ProductId);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.ProductId, CartItemUpdates[i].PurchaseQuantity);
                                }
                            }

                        }
                    }

                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to update Database -  " 
                        + exp.Message.ToString(), exp);
                }
            }
        }


        public void RemoveItem(String removeCartID, int removeProductID)
        {
            using (var _db = new ToyDemoProj.Models.ProductContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems
                                  where c.CartId == removeCartID &&
  c.Product.ProductID == removeProductID
                                  select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        _db.ShoppingCartItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }

                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to remove cart item - "
                        + exp.Message.ToString(), exp);
                }
            }
        }




        public void UpdateItem(string updateCartID, int updateProductID, int quantity)
        {
            using (var _db = new ToyDemoProj.Models.ProductContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems
                                  where c.CartId == updateCartID &&
c.Product.ProductID == updateProductID
                                  select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void EmptyCart()
        {
         var  ShoppingCartId = GetCartId();
            var cartItems = _db.ShoppingCartItems.Where(
            c => c.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                _db.ShoppingCartItems.Remove(cartItem);
            }
           
            _db.SaveChanges();
        }
        public int GetCount()
        {
          var  ShoppingCartId = GetCartId();
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _db.ShoppingCartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public struct ShoppingCartUpdates
        {
            public int ProductId;
            public int PurchaseQuantity;
            public bool RemoveItem;
        }
    }
}