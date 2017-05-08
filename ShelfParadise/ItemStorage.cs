using System.Collections.Generic;

namespace ShelfParadise
{
    class ItemStorage
    {
        #region Attributes
        protected List<Item> itemList = new List<Item>();
        #endregion

        #region Properties
        public List<Item> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
        #endregion

        #region Methods
        // Add item to list of objects of type Item
        public void Additem(Item pItem)
        {
            itemList.Add(pItem);
        }

        // Remove item from list of objects of type Item
        public void RemoveItem(Item pItem)
        {
            // TODO: Code for removal of specified element
            //itemList.Remove(pItem);
        }
        #endregion
    } // End of ItemStorage()
}
