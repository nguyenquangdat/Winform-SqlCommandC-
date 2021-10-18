using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo2DB
{
    public partial class FrmManageCategories : Form
    {
        public FrmManageCategories()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        CategoryBusiness catList = new CategoryBusiness();
        private int CatID =0;
        void LoadCategories()
        {
            List<Category> categories = catList.GetCategory();
            txtCategoryID.DataBindings.Clear();
            txtCategoryName.DataBindings.Clear();
            txtCategoryID.DataBindings.Add("Text", categories, "CategoryID");
            txtCategoryName.DataBindings.Add("Text", categories, "CategoryName");
            dgvCategory.DataSource = categories;
        }
        private void FrmManageCategories_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategories();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                String cateName = txtCategoryName.Text;
                Category category = new Category();
                category.CategoryName = cateName;
                var rs = catList.InsertCategory(category);
                if(rs > 0)
                {
                    MessageBox.Show("Insert success.");
                    LoadCategories();
                }
                else
                {
                    MessageBox.Show("Insert failse.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(CatID != 0)
            {
                try
                {
                    String name = txtCategoryName.Text;
                    int rs = catList.UpdateCategory(CatID, name);
                    if(rs > 0)
                    {
                        MessageBox.Show("update success.");
                        LoadCategories();
                    }
                    else
                    {
                        MessageBox.Show("update success.");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CatID =  int.Parse(dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString());
            String CatName = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCategoryName.Text = CatName;
            txtCategoryID.Text = CatID.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatID != 0)
                {
                    var rs = catList.DeleteCategory(CatID);
                    if (rs > 0)
                    {
                        MessageBox.Show("delete successful");
                        LoadCategories();
                    }
                    else
                    {
                        MessageBox.Show("delete fail");
                    }
                }
                else
                {
                    MessageBox.Show("Can't delete");
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
