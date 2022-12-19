using System;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using StoreManager.Views;
using StoreManager.Model;
using System.Drawing.Imaging;
using StoreManager.Model.Repositories;

namespace StoreManager.Presenters
{
    public sealed class EditCategoryPresenter : Presenter<EditCategoryView>
    {
        #region fields

        private readonly ApplicationPresenter m_AppPresenter;
        private readonly CategoriesRepository m_Repository;
        private readonly Category m_Category;

        #endregion

        #region ctr/dtr

        public EditCategoryPresenter(ApplicationPresenter appPresenter, EditCategoryView view,
            CategoriesRepository repository, Category category)
            : base(view, "Category.CategoryName")
        {
            m_AppPresenter = appPresenter;
            m_Repository = repository;
            m_Category = category;
        }

        #endregion

        #region properties

        public Category Category { get { return m_Category; } }

        #endregion

        #region methods

        public void ChangePicture()
        {
            var OpenDlg = new OpenFileDialog();
            OpenDlg.Filter = "Image files (*.jpg)|*.jpg|(*.png)|*.png|(*.gif)|*.gif";
            OpenDlg.RestoreDirectory = true;
            if (OpenDlg.ShowDialog() == true)
            {
                var PicPath = OpenDlg.FileName;
                if (!string.IsNullOrEmpty(PicPath))
                {
                    Category.Picture = File.ReadAllBytes(PicPath);
                }
            }
        }

        public void Save()
        {
            m_Repository.Save(Category);
            if (m_Repository.ExecutedGood)
            {
                m_AppPresenter.StatusText = "مقوله ی مورد نظر ذخیره شد";
            }
        }

        public void Delete()
        {
            m_Repository.Delete(Category);
            if (m_Repository.ExecutedGood)
            {
                m_AppPresenter.RemoveTab(this);
                m_AppPresenter.StatusText = "مقوله ی مورد نظر حذف شد";
            }
        }

        public void Close()
        {
            m_AppPresenter.RemoveTab(this);
        }

        #endregion
    }
}
