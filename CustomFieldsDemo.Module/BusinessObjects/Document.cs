using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CustomFieldsDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Document : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Document(Session session)
            : base(session)
        {
        }

        private string _DocumentName;
        public string DocumentName
        {
            get { return _DocumentName; }
            set { SetPropertyValue("DocumentName", ref _DocumentName, value); }
        }
        [Association("Document-DocumentItems"), DevExpress.Xpo.Aggregated]
        public XPCollection<DocumentItemBase> DocumentItems
        {
            get { return GetCollection<DocumentItemBase>("DocumentItems"); }
        }
        [EditorAlias("DynamicDocument")]
        [ItemsPerRow(4)]
        public IList<DocumentItemBase> Items
        {
            get { return GetCollection<DocumentItemBase>("DocumentItems"); }
        }



        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public IObjectSpace ObjectSpace { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            AddBooleanFields();
            AddStringFields();
            AddDateTimeFields();
            AddIntFields();

        }

        private void AddIntFields()
        {
            string connectionString = "Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CustomFieldsDemo";
            string myCommandString = "SELECT Caption, MAX(DocumentItemBase.[Order]) AS 'Order' FROM DocumentItemBase, DocumentIntItem WHERE DocumentItemBase.Oid = DocumentIntItem.Oid AND GCRecord IS NULL GROUP BY Caption"; 
            SqlConnection SqlConnection = new SqlConnection(connectionString);
            SqlCommand SqlCommand = new SqlCommand(myCommandString, SqlConnection);



            List<DocumentIntItem> myList = new List<DocumentIntItem>();
            SqlConnection.Open();
            SqlDataReader SqlReader = SqlCommand.ExecuteReader();

            try
            {
                while (SqlReader.Read())
                {
                    DocumentIntItem dib = new DocumentIntItem(Session);
                    dib.Caption = CheckDbNull(SqlReader, 0);
                    dib.Order = SqlReader.GetInt32(1);
                    myList.Add(dib);
                }
            }
            finally
            {

                SqlConnection.Close();
            }

            //Populate ServiceInspection Table
            foreach (var detail in myList)
            {
                DocumentIntItem myDocument = new DocumentIntItem(Session);
                myDocument.Caption = detail.Caption;
                myDocument.Order = detail.Order;
                myDocument.Document = this;
                myDocument.Save();
            }
        }

        private void AddDateTimeFields()
        {
            string connectionString = "Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CustomFieldsDemo";
            string myCommandString = "SELECT Caption, MAX(DocumentItemBase.[Order]) AS 'Order' FROM DocumentItemBase, DocumentDateTimeItem WHERE DocumentItemBase.Oid = DocumentDateTimeItem.Oid AND GCRecord IS NULL GROUP BY Caption"; 
            SqlConnection SqlConnection = new SqlConnection(connectionString);
            SqlCommand SqlCommand = new SqlCommand(myCommandString, SqlConnection);



            List<DocumentDateTimeItem> myList = new List<DocumentDateTimeItem>();
            SqlConnection.Open();
            SqlDataReader SqlReader = SqlCommand.ExecuteReader();

            try
            {
                while (SqlReader.Read())
                {
                    DocumentDateTimeItem dib = new DocumentDateTimeItem(Session);
                    dib.Caption = CheckDbNull(SqlReader, 0);
                    dib.Order = SqlReader.GetInt32(1);
                    myList.Add(dib);
                }
            }
            finally
            {

                SqlConnection.Close();
            }

            //Populate ServiceInspection Table
            foreach (var detail in myList)
            {
                DocumentDateTimeItem myDocument = new DocumentDateTimeItem(Session);
                myDocument.Caption = detail.Caption;
                myDocument.Order = detail.Order;
                myDocument.Document = this;
                myDocument.Save();
            }
        }

        private void AddStringFields()
        {
            string connectionString = "Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CustomFieldsDemo";
            string myCommandString = "SELECT Caption, MAX(DocumentItemBase.[Order]) AS 'Order' FROM DocumentItemBase, DocumentStringItem WHERE DocumentItemBase.Oid = DocumentStringItem.Oid AND GCRecord IS NULL GROUP BY Caption"; 
            SqlConnection SqlConnection = new SqlConnection(connectionString);
            SqlCommand SqlCommand = new SqlCommand(myCommandString, SqlConnection);



            List<DocumentStringItem> myList = new List<DocumentStringItem>();
            SqlConnection.Open();
            SqlDataReader SqlReader = SqlCommand.ExecuteReader();

            try
            {
                while (SqlReader.Read())
                {
                    DocumentStringItem dib = new DocumentStringItem(Session);
                    dib.Caption = CheckDbNull(SqlReader, 0);
                    dib.Order = SqlReader.GetInt32(1);
                    myList.Add(dib);
                }
            }
            finally
            {

                SqlConnection.Close();
            }

            //Populate ServiceInspection Table
            foreach (var detail in myList)
            {
                DocumentStringItem myDocument = new DocumentStringItem(Session);
                myDocument.Caption = detail.Caption;
                myDocument.Order = detail.Order;
                myDocument.Document = this;
                myDocument.Save();
            }
        }

        private void AddBooleanFields()
        {
            string connectionString = "Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CustomFieldsDemo";
            string myCommandString = "SELECT Caption, MAX(DocumentItemBase.[Order]) AS 'Order' FROM DocumentItemBase, DocumentBoolItem WHERE DocumentItemBase.Oid = DocumentBoolItem.Oid AND GCRecord IS NULL GROUP BY Caption";
            SqlConnection SqlConnection = new SqlConnection(connectionString);
            SqlCommand SqlCommand = new SqlCommand(myCommandString, SqlConnection);



            List<DocumentBoolItem> myList = new List<DocumentBoolItem>();
            SqlConnection.Open();
            SqlDataReader SqlReader = SqlCommand.ExecuteReader();

            try
            {
                while (SqlReader.Read())
                {
                    DocumentBoolItem dib = new DocumentBoolItem(Session);
                    dib.Caption = CheckDbNull(SqlReader, 0);
                    dib.Order = SqlReader.GetInt32(1);
                    myList.Add(dib);
                }
            }
            finally
            {

                SqlConnection.Close();
            }

            //Populate ServiceInspection Table
            foreach (var detail in myList)
            {
                DocumentBoolItem myDocument = new DocumentBoolItem(Session);
                myDocument.Caption = detail.Caption;
                myDocument.Order = detail.Order;
                myDocument.Document = this;
                myDocument.Save();
            }
        }

        public static string CheckDbNull(SqlDataReader reader, int index)
        {
            return !reader.IsDBNull(index) ? reader.GetString(index) : String.Empty;
        }




    }
    [AttributeUsage(AttributeTargets.Property)]
    public class ItemsPerRowAttribute : Attribute
    {
        public ItemsPerRowAttribute(int value)
        {
            Value = value;
        }
        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}