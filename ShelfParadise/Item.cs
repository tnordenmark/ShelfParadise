using System;

namespace ShelfParadise
{
    public class Item
    {
        #region Attributes
        private int artikelNummer;
        private string namn;
        private string kategori;
        private int pris;
        #endregion

        #region Properties
        public int ArtikelNummer
        {
            get { return artikelNummer; }
            set { artikelNummer = value; }
        }

        public string Namn
        {
            get { return namn; }
            set { namn = value; }
        }

        public string Kategori
        {
            get { return kategori; }
            set { kategori = value; }
        }

        public int Pris
        {
            get { return pris; }
            set { pris = value; }
        }
        #endregion

        #region Overrides
        
        // Override ToString() method to return a nicely formatted string
        public override string ToString()
        {
            return String.Format("{0,-20}{1,-20}{2,-20}{3,-20}", ArtikelNummer, Namn, Kategori, Pris);
        }
        #endregion
    } // End of Item
}
