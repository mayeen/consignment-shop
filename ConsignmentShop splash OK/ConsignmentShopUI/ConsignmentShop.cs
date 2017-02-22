using consignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
     //    splash Start_lavel1 = new splash();
           // Start_lavel1.ShowDialog();
        
       
            
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorBinding = new BindingSource();
        private decimal storeProfit = 0;
        
        public ConsignmentShop()
        {
           
            
            InitializeComponent();
            SetupData();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            
            itemListBox.DataSource = itemsBinding;

            itemListBox.DisplayMember = "Display";
            itemListBox.ValueMember = "Display";

            cartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = cartBinding;

            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.DisplayMember = "Display";

            vendorBinding.DataSource = store.Vendors;
            vendorListbox.DataSource = vendorBinding;
            vendorListbox.DisplayMember = "Display";
            vendorListbox.ValueMember = "Display";
        }

        private void SetupData()
        {

            Vendor demoVendor = new Vendor();
            demoVendor.FirstName = "Kazi";
            demoVendor.LastName = "Shahriar";
            demoVendor.Commission = .5;
            store.Vendors.Add(demoVendor);

            demoVendor = new Vendor();
            demoVendor.FirstName = "Syed";
            demoVendor.LastName = "Ahmed";
            demoVendor.Commission = .8;
            store.Vendors.Add(demoVendor);
            store.Vendors.Add(new Vendor { FirstName = "Nuzhat", LastName = "gawhar" });
            store.Vendors.Add(new Vendor { FirstName = "Abdur", LastName = "Rahman" });
            store.Vendors.Add(new Vendor { FirstName = "Abdur", LastName = "rafi", Commission = .6 });

            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about whale",
                Price = 150,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "Harry Potter",
                Description = "A book about a magical world",
                Price = 210,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "Amar Bondhu Rashed",
                Description = "A book about liberation war",
                Price = 200,
                Owner = store.Vendors[2]
            });
            store.Name = "Easy to Buy";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            Item SelectedItem = (Item)itemListBox.SelectedItem;
           
            shoppingCartData.Add(SelectedItem);
            cartBinding.ResetBindings(false);

        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            //sold the item
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit += (1- (decimal)item.Owner.Commission) * item.Price;     
            }
            shoppingCartData.Clear();
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            storeProfitValue.Text = string.Format("{0}Tk", storeProfit);
            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorBinding.ResetBindings(false);
       
        }

        private void ConsignmentShop_Load(object sender, EventArgs e)
        {
            
        }

        private void vendorListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
