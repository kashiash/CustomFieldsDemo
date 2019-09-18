using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CustomFieldsDemo.Module.BusinessObjects
{
    [PropertyEditor(typeof(IList<DocumentItemBase>), "DynamicDocument", false)]
    public class WinDocumentItemsPropertyEditor : PropertyEditor, IComplexViewItem
    {
        public static int DefaultItemsPerRow = 3;
        public WinDocumentItemsPropertyEditor(Type type, IModelMemberViewItem model) : base(type, model) { }
        protected override object CreateControlCore()
        {
            return CreateDocumentItemsLayout();
        }
        private LayoutControl CreateDocumentItemsLayout()
        {
            LayoutControl layout = new XafLayoutControl();
            layout.Root.DefaultLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Vertical;
            layout.Name = "DocumentItemsLayout";
            CreateLayoutItems(layout);
            return layout;
        }
        private void CreateLayoutItems(LayoutControl layout)
        {
            layout.BeginUpdate();
            try
            {
                layout.Clear();
                IList<DocumentItemBase> documentItems = GetSortedDocumentItems();
                if (documentItems.Count != 0)
                {
                    int itemsPerRow = DefaultItemsPerRow;
                    ItemsPerRowAttribute attr = MemberInfo.FindAttribute<ItemsPerRowAttribute>();
                    if (attr != null)
                        itemsPerRow = attr.Value;



                    for (int rowNumber = 0; rowNumber < Math.Ceiling((decimal)documentItems.Count / (decimal)itemsPerRow); rowNumber++)
                    {
                        LayoutControlGroup row = layout.AddGroup();
                        row.Name = GetId("Row", rowNumber);
                        row.DefaultLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
                        row.GroupBordersVisible = false;

                        for (int cellNumber = 0; cellNumber < itemsPerRow; cellNumber++)
                            if (rowNumber * itemsPerRow + cellNumber < documentItems.Count)
                            {
                                DocumentItemBase item = documentItems[rowNumber * itemsPerRow + cellNumber];
                                LayoutControlItem cell = new LayoutControlItem(layout, CreateDocumentItemEditor(item));
                                cell.Name = GetId("Cell", item);
                                cell.Text = item.Caption;
                                if (application.Model.Options.LayoutManagerOptions.EnableCaptionColon)
                                    cell.Text += ":";
                                row.Add(cell);
                            }
                            else
                                row.Add(new EmptySpaceItem());
                    }
                }
            }
            finally
            {
                layout.EndUpdate();
                layout.BestFit();
            }
        }
        private IList<DocumentItemBase> GetSortedDocumentItems()
        {
            if (PropertyValue is XPBaseCollection)
            {
                XPCollection<DocumentItemBase> collection = new XPCollection<DocumentItemBase>((XPBaseCollection)PropertyValue);
                collection.Sorting.Clear();
                collection.Sorting.Add(new SortProperty("Order", DevExpress.Xpo.DB.SortingDirection.Ascending));
                return collection;
            }
            return (IList<DocumentItemBase>)PropertyValue;
        }

        private Control CreateDocumentItemEditor(DocumentItemBase item)
        {
            IModelMemberViewItem modelViewItem = (IModelMemberViewItem)application.FindModelClass(item.GetType()).DefaultDetailView.Items["Value"];
            if (modelViewItem != null)
            {
                WinPropertyEditor propertyEditor = (WinPropertyEditor)application.EditorFactory.CreatePropertyEditorByType(modelViewItem.PropertyEditorType, modelViewItem, item.GetType(), application, objectSpace);
                propertyEditor.CurrentObject = item;
                propertyEditor.CreateControl();
                propertyEditor.Control.Name = GetId("Editor", item);
                if (propertyEditor.Control is CheckEdit)
                    ((CheckEdit)propertyEditor.Control).Text = String.Empty;
                propertyEditor.ControlValueChanged += new EventHandler(propertyEditor_ControlValueChanged);
                return propertyEditor.Control;
            }
            return null;
        }

        void propertyEditor_ControlValueChanged(object sender, EventArgs e)
        {
            OnControlValueChanged();
        }
        private string GetId(string elementName, DocumentItemBase item)
        {
            return elementName + item.GetHashCode().ToString();
        }
        private string GetId(string elementName, int number)
        {
            return elementName + number.ToString();
        }
        protected override void ReadValueCore()
        {
            CreateLayoutItems((LayoutControl)Control);
        }
        protected override object GetControlValueCore()
        {
            return PropertyValue;
        }
        #region IComplexViewItem Members
        XafApplication application;
        IObjectSpace objectSpace;
        void IComplexViewItem.Setup(IObjectSpace objectSpace, XafApplication application)
        {
            this.objectSpace = objectSpace;
            this.application = application;
        }
        #endregion
    }
}
