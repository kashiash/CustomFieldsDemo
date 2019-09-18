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

namespace CustomFieldsDemo.Module.BusinessObjects
{
  //  [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DocumentStringItem : DocumentItemBase
    {
        public DocumentStringItem(Session session, string value) : base(session)
        {
            Value = value;
        }
        public DocumentStringItem(Session session) : base(session) { }
        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { SetPropertyValue("Value", ref _Value, value); }
        }
    }

}