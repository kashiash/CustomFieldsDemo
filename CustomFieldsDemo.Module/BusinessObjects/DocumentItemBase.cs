using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using CustomFieldsDemo.Module.Attributes;

namespace CustomFieldsDemo.Module.BusinessObjects
{
   // [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    
    public class DocumentItemBase : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        protected DocumentItemBase(Session session)
            : base(session)
        {
        }
        private Document _Document;
        [Association("Document-DocumentItems")]
        public Document Document
        {
            get { return _Document; }
            set { SetPropertyValue("Document", ref _Document, value); }
        }
        private string _Caption;
        public string Caption
        {
            get { return _Caption; }
            set { SetPropertyValue("Caption", ref _Caption, value); }
        }
        private int _Order;
        public int Order
        {
            get { return _Order; }
            set { SetPropertyValue("Order", ref _Order, value); }
        }

        //bool _alwaysVisible;
        //public bool AlwaysVisible
        //{
        //    get
        //    {
        //        return _alwaysVisible;
        //    }
        //    set
        //    {
        //        SetPropertyValue("AlwaysVisible", ref _alwaysVisible, value);
        //    }
        //}

        protected override void OnSaving()
        {
            base.OnSaving();
                      
        }
    }
}