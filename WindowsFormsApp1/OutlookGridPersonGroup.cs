﻿using System;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using KryptonOutlookGrid.Classes;
using KryptonOutlookGrid.Interfaces;

namespace WindowsFormsApp1
{

    public class OutlookGridPersonGroup : OutlookGridDefaultGroup
    {

        private int _priceCode;
        private string _currency;

        private const int noPrice = 999999;
        public OutlookGridPersonGroup() : base()
        {
            AllowHiddenWhenGrouped = false;
            _currency = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentGroup">The parentGroup if any.</param>
        public OutlookGridPersonGroup(IOutlookGridGroup parentGroup) : base(parentGroup)
        {
            AllowHiddenWhenGrouped = false;
            _currency = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
        }

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        public override string Text
        {
            get { return string.Format("{0}", GetPriceString(_priceCode)); }
        }

        private int GetPriceCode(decimal price)
        {
            if ((price == 1))
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private string GetPriceString(int priceCode)
        {
            switch (priceCode)
            {
                case 1:
                    return "РУП ПО \"Белоруснефть\"";
                case 2:
                    return "Дочерние предприятия";
                default:
                    return "";
            }
        }


        /// <summary>
        /// Gets or sets the Alphabetic value
        /// </summary>
        public override object Value
        {
            get { return val; }
            set
            {
                if (object.ReferenceEquals(value, DBNull.Value) || value == null)
                {
                    _priceCode = noPrice;
                    val = _priceCode;
                }
                else
                {
                    _priceCode = GetPriceCode(decimal.Parse(value.ToString()));
                    val = _priceCode;
                }
            }
        }

        #region "ICloneable Members"

        /// <summary>
        /// Overrides the Clone() function
        /// </summary>
        /// <returns>OutlookGridAlphabeticGroup</returns>
        public override object Clone()
        {
            OutlookGridPersonGroup gr = new OutlookGridPersonGroup(this.ParentGroup);

            gr.Column = this.Column;
            gr.Value = this.val;
            gr.Collapsed = this.Collapsed;
            gr.Height = this.Height;
            gr.GroupImage = this.GroupImage;
            gr.FormatStyle = this.FormatStyle;
            gr.XXXItemsText = this.XXXItemsText;
            gr.OneItemText = this.OneItemText;
            gr.AllowHiddenWhenGrouped = this.AllowHiddenWhenGrouped;
            gr.SortBySummaryCount = this.SortBySummaryCount;
            gr._currency = _currency;
            gr._priceCode = _priceCode;
            return gr;
        }

        #endregion

        #region "IComparable Members"
        /// <summary>
        /// overide the CompareTo, so only the first character is compared, instead of the whole string
        /// this will result in classifying each item into a letter of the Alphabet.
        /// for instance, this is usefull when grouping names, they will be categorized under the letters A, B, C etc..
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            int orderModifier = (Column.SortDirection == SortOrder.Ascending ? 1 : -1);
            int priceOther = 0;

            if (obj is OutlookGridPersonGroup)
            {
                priceOther = ((OutlookGridPersonGroup)obj)._priceCode;
            }
            else
            {
                priceOther = noPrice;
            }
            return _priceCode.CompareTo(priceOther) * orderModifier;
        }
        #endregion
    }

}
