using PRDG11EOSV03WB.CommonFunction;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SGH_CCES
{
    public partial class GridViewPaging : System.Web.UI.UserControl
    {
        #region Delegate

        public event GridViewPagingDelegate.PageChangedEventHandler PageIndexChanging;

        void Pager_PageIndexChanging(object sender, GridViewPagingEventArgs e)
        {
            PageIndexChanging(this, e);
        }

        #endregion

        #region Properties

        private int _PageIndex;
        public int PageIndex
        {
            set { _PageIndex = value; }
            get
            {
                int intIndex = (Convert.ToInt32(ViewState["PageIndex"]) == 0) ? 1 : Convert.ToInt32(ViewState["PageIndex"]);

                if (Convert.ToInt32(txtPage.Text) > Convert.ToInt32(lblTotal.Text))
                {
                    intIndex = Convert.ToInt32(lblTotal.Text);
                }

                return intIndex;
            }
        }

        private int _TotalPages;
        public int TotalPages
        {
            set { _TotalPages = value; }
            get
            {
                return (TotalRecords / PageSize) + (TotalRecords % PageSize == 0 ? 0 : 1);
            }
        }

        private int _PageSize;
        public int PageSize
        {
            set { _PageSize = value; }
            get { return _PageSize == 0 ? 10 : _PageSize; }
        }

        public int RecordFirst
        {
            get
            {
                ViewState["PageIndex"] = txtPage.Text = "1";
                return 1;
            }
        }

        private int _RecordFrom;
        public int RecordFrom
        {
            set { _RecordFrom = value; }
            get
            {
                return ((PageIndex - 1) * PageSize) + 1;
            }
        }

        private int _RecordTo;
        public int RecordTo
        {
            set { _RecordTo = value; }
            get
            {
                return PageIndex * PageSize;
            }
        }

        private int _TotalRecords;
        public int TotalRecords
        {
            set { _TotalRecords = value; }
            get { return _TotalRecords; }
        }

        #endregion

        #region Page

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblTotal.Text = TotalPages.ToString();
                    ViewState["PageIndex"] = "1";
                    ResetButtons();
                }
            }
            catch
            {

            }
        }

        #endregion

        #region Button

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                txtPage.Text = "1";
                ChangePage();
            }
            catch
            {

            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                txtPage.Text = (Convert.ToInt32(txtPage.Text) - 1).ToString();
                ChangePage();
            }
            catch
            {

            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                txtPage.Text = (Convert.ToInt32(txtPage.Text) + 1).ToString();
                ChangePage();
            }
            catch
            {

            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                txtPage.Text = lblTotal.Text;
                ChangePage();
            }
            catch
            {

            }
        }

        #endregion

        #region LinkButton

        protected void lbtnPage_Click(object sender, EventArgs e)
        {
            ChangePage();
        }

        #endregion

        #region Methods / Functions

        public void BindTotalPages(int TotalItems)
        {
            try
            {
                TotalRecords = TotalItems;
                lblTotal.Text = TotalPages.ToString();
                ResetButtons();
            }
            catch
            {

            }
        }

        private void ChangePage()
        {
            try
            {
                hfPage.Value = txtPage.Text;
                GridViewPagingEventArgs args = new GridViewPagingEventArgs();
                args.NewPageIndex = Convert.ToInt32(txtPage.Text);
                args.TotalPages = Convert.ToInt32(lblTotal.Text);
                //PageIndex = args.NewPageIndex;
                ViewState["PageIndex"] = txtPage.Text;
                Pager_PageIndexChanging(this, args);

                BindTotalPages(TotalRecords);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
            }
        }

        private void ResetButtons()
        {
            try
            {
                divPage.Visible = TotalPages > 1 ? true : false;

                if (TotalPages > 0)
                {
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;

                    if (lblTotal.Text == "1")
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnFirst.Enabled = false;
                        btnPrevious.Enabled = false;
                    }
                    else
                    {
                        if ((txtPage.Text == lblTotal.Text) || (Convert.ToInt32(txtPage.Text) > Convert.ToInt32(lblTotal.Text)))
                        {
                            txtPage.Text = lblTotal.Text;
                            btnNext.Enabled = false;
                            btnLast.Enabled = false;
                        }
                        else
                        {
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;
                        }

                        if (txtPage.Text == "1")
                        {
                            btnFirst.Enabled = false;
                            btnPrevious.Enabled = false;
                        }
                        else
                        {
                            btnFirst.Enabled = true;
                            btnPrevious.Enabled = true;
                        }
                    }
                }
                else
                {
                    ViewState["PageIndex"] = hfPage.Value = txtPage.Text = lblTotal.Text = "1";
                }
            }
            catch
            {

            }
        }

        #endregion
    }
}