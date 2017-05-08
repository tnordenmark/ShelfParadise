using System.Collections.Generic;
using System.Linq;

namespace ShelfParadise
{
    class ShopLogic
    {
        #region Attributes
        ShopStorage shopInventory = new ShopStorage();
        ShoppingCart cart = new ShoppingCart();
        #endregion

        #region Properties
        #endregion

        #region Methods
        // Add item to the ShopStorage list of Item.
        public void AddItem(int partikelnr, string pnamn, string pkategori, int ppris)
        {
            Item item = new Item();

            item.ArtikelNummer = partikelnr;
            item.Namn = pnamn;
            item.Kategori = pkategori;
            item.Pris = ppris;

            shopInventory.Additem(item);
        }

        // Remove an item
        public void RemoveItem(int partikelnr)
        {
            // TODO: code for removal of specified item
        }

        // Get unsorted list of all items
        public List<Item> GetStoreInventory()
        {
            List<Item> itemList = shopInventory.ItemList.ToList();

            return itemList;
        }

        // Get list of all items sorted ascending by price.
        public List<Item> GetItemListByPrice()
        {
            List<Item> itemListByPrice = shopInventory.ItemList.OrderBy(i => i.Pris).ToList();

            return itemListByPrice;
        }

        // Get list of all items sorted ascending by name.
        public List<Item> GetItemListByName()
        {
            List<Item> itemListByName = shopInventory.ItemList.OrderBy(i => i.Namn).ToList();

            return itemListByName;
        }

        // Find product with name matching the supplied string
        public Item FindProductByName(string pnamn)
        {
            List<Item> itemList = shopInventory.ItemList;

            return itemList.Find(i => i.Namn == pnamn);
        }

        // Find product with part number matching the supplied int
        public Item FindProductByPartNo(int partikelnr)
        {
            List<Item> itemList = shopInventory.ItemList;

            return itemList.Find(i => i.ArtikelNummer == partikelnr);
        }

        // Add Item to Shopping Cart by part number
        public void AddItemToCart(int partikelnr)
        {
            Item tempItem = FindProductByPartNo(partikelnr);

            cart.Additem(tempItem);
        }

        // Get contents of Shopping cart
        public List<Item> GetShoppingCartContents()
        {
            List<Item> cartList = cart.ItemList;
   
            return cartList;
        }

        // Clear Shopping Cart
        public void EmptyShoppingCart()
        {
            cart.ItemList.Clear();
        }
        #endregion
    } // End of ShopLogic
}
