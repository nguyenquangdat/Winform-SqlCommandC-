using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Demo2DB
{
    public partial class frmManageProduct : Form
    {
        ProductBusiness productBusiness = new ProductBusiness();
        private int productID;
        public frmManageProduct()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void LoadForm()
        {
            CategoryBusiness categoryBusiness = new CategoryBusiness();
            List<Product> listProduct = productBusiness.GetProduct();
            List<Category> listCategory = categoryBusiness.GetCategory();
            txtProductName.DataBindings.Clear();
            nupUnitPrice.DataBindings.Clear();
            nupUnitStock.DataBindings.Clear();
            txtProductID.DataBindings.Clear();
            txtProductID.DataBindings.Add("Text", listProduct, "ProductID");
            txtProductName.DataBindings.Add("Text", listProduct, "ProductName");
            nupUnitPrice.DataBindings.Add("Value", listProduct, "UnitPrice");
            nupUnitStock.DataBindings.Add("Value", listProduct, "UnitInstock");
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "CategoryID";
            dgvProduct.DataSource = listProduct;
            /*txtProductName.Text = "";
            txtProductID.Text = "";
            nupUnitPrice.Value = 0;
            nupUnitStock.Value = 0;*/
        }
        private void frmManageProduct_Load(object sender, EventArgs e)
        {
            LoadForm();
            cbCategory.SelectedValue = dgvProduct.Rows[0].Cells[4].Value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.UnitPrice = float.Parse(nupUnitPrice.Value.ToString());
                product.UnitInstock = int.Parse(nupUnitStock.Value.ToString());
                product.CategoryId = int.Parse(cbCategory.SelectedValue.ToString());
                int rs = productBusiness.InsertProudct(product);
                if (rs > 0)
                {
                    MessageBox.Show("Insert Succesfull");
                    LoadForm();
                }
                else{
                    MessageBox.Show("Insert fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

          

        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            productID = int.Parse(dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtProductID.Text = productID.ToString();
            txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            nupUnitPrice.Value = decimal.Parse(dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString());
            nupUnitStock.Value = int.Parse(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
            cbCategory.SelectedValue = int.Parse(dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString());

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(productID != 0)
            {
                try
                {
                    Product product = productBusiness.GetProductByID(productID);
                    product.ProductName = txtProductName.Text;
                    product.UnitPrice = float.Parse(nupUnitPrice.Value.ToString());
                    product.UnitInstock = int.Parse(nupUnitStock.Value.ToString());
                    product.CategoryId = int.Parse(cbCategory.SelectedValue.ToString());
                    var rs = productBusiness.UpdateProduct(product);
                    if (rs > 0)
                    {
                        MessageBox.Show("Update Successful");
                        LoadForm();
                    }
                    else
                    {
                        MessageBox.Show("Update fail");
                    }
                         
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                
            }
            else
            {
                MessageBox.Show("can't Update");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(productID != 0)
            {
                try
                {
                    int rs = productBusiness.DeleteProduct(productID);
                    if (rs > 0)
                    {
                        MessageBox.Show("delete sucess");
                        LoadForm();
                    }
                    else
                    {
                        MessageBox.Show("delete fail");
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
    }
}
