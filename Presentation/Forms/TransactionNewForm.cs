﻿using Domain.Library.Enums;
using Infrastructure.Library.Models.Controls;
using Infrastructure.Library.Models.DTOs.BUS;
using Infrastructure.Library.Patterns;
using Presentation.Generator;
using System.Runtime.InteropServices;

namespace Presentation.Forms
{
    public partial class TransactionNewForm : Form
    {
        private IFacadPattern Pattern;
        private Guid TransactionID;

        #region Code
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public TransactionNewForm()
        {
            InitializeComponent();
            Pattern = new FacadPattern();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }
        private void timer_Tick(object sender, EventArgs e)
        {
        }

        #endregion
        private void TransactionNewForm_Load(object sender, EventArgs e)
        {
            UpdateComboBoxes();
        }
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            TransactionID = new Guid();
            var type = ((KeyValue<long>)TransactionTypeCombo.SelectedItem).Value;
            if (TransactionValidation(type))
            {
                //var fromCartId = ((KeyValue<long>)FromCustomerCombo.SelectedItem).Value;
                var fromAccountId = ((KeyValue<long>)FromAccountCombo.SelectedItem).Value;

                var cash = Convert.ToDouble(CashTxt.Text);

                switch (type)
                {

                    case 1: // خرید از کارت
                        {
                            //var CartData = unitOfWork.CartService.GetById(fromCartId);
                            Pattern.UnitOfWork.BeginTransaction();
                            try
                            {
                                //var CartChildData = Pattern.CartService.GetById(fromAccountId);
                                if (Pattern.CartService.ValidBankBlance(fromAccountId, cash))
                                {
                                    var lastBlance = Pattern.BlanceService.GetBlanceCartById(fromAccountId);
                                    var blanceDto = BlanceDTO(lastBlance,Convert.ToDouble(cash),fromAccountId,false);
                                    blanceDto.ID = Pattern.BlanceService.Insert(blanceDto);
                                    this.Close();

                                    Pattern.UnitOfWork.Commit();
                                }
                                else
                                {
                                    MSG.Text = $"موجودی کافی نیست و این تراکنش انجام نمیشود";
                                }

                            }
                            catch
                            {
                                Pattern.UnitOfWork.Rollback();
                                throw;
                            }
                            break;
                        }
                    case 2: //  کارت به کارت
                        {
                            var toCartId = ((KeyValue<long>)ToCustomerCombo.SelectedItem).Value;
                            var toCartAccountId = ((KeyValue<long>)ToAccountCombo.SelectedItem).Value;
                            if (Pattern.CartService.ValidBankBlance(fromAccountId, cash))
                            {
                                Pattern.UnitOfWork.BeginTransaction();
                                //  کسر از حساب مبداء
                                try
                                {
                                    var lastBlance = Pattern.BlanceService.GetBlanceCartById(fromAccountId);
                                    var fromAccountDto = BlanceDTO(lastBlance,Convert.ToDouble(cash),fromAccountId,false);
                                    fromAccountDto.ID = Pattern.BlanceService.Insert(fromAccountDto);
                                    //  اضافه به حساب مقصد
                                    var toCartlastBlance = Pattern.BlanceService.GetBlanceCartById(toCartAccountId);
                                    var toAccountDto = BlanceDTO(toCartlastBlance,Convert.ToDouble(cash),toCartAccountId,true);
                                    toAccountDto.ID = Pattern.BlanceService.Insert(toAccountDto);
                                    Pattern.UnitOfWork.Commit();
                                    this.Close();
                                }
                                catch (Exception ex)
                                {
                                    Pattern.UnitOfWork.Rollback();
                                    throw;
                                }
                            }
                            else
                            {
                                MSG.Text = $"موجودی کافی نیست و این تراکنش انجام نمیشود";
                            }

                            break;
                        }
                    case 3: //  واریز به کارت
                        {
                            var toCartId = ((KeyValue<long>)ToCustomerCombo.SelectedItem).Value;
                            var toCartChildId = ((KeyValue<long>)ToAccountCombo.SelectedItem).Value;
                            break;
                        }

                    default:
                        {
                            MSG.Text = "هنوز هیچ نوع تراکنشی تایید نشده است";
                            break;
                        }
                }
            }
            else
            {
                MSG.Text = $"موجودی کافی نیست و این تراکنش انجام نمیشود";
            }
        }
        private void TransactionTypeCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            var type = ((KeyValue<long>)TransactionTypeCombo.SelectedItem).Value;
            MSG.Text = ((KeyValue<long>)TransactionTypeCombo.SelectedItem).Key;
            switch (type)
            {
                case 1:
                    {
                        label5.Visible = false;
                        ToCustomerCombo.Visible = false;
                        label7.Visible = false;
                        ToAccountCombo.Visible = false;
                        NewDataBtn.Visible = false;
                        TransactionKindCombo.SelectedIndex = 2;
                        BlanceTypeCombo.SelectedIndex = 2;
                        NewDataPanel.Visible = false;
                        break;
                    }
                case 2:
                    {
                        label5.Visible = true;
                        ToCustomerCombo.Visible = true;
                        label7.Visible = true;
                        ToAccountCombo.Visible = true;
                        NewDataBtn.Visible = true;
                        TransactionKindCombo.SelectedIndex = 2;
                        NewDataPanel.Visible = false;
                        break;
                    }
                case 3:
                    {
                        label5.Visible = true;
                        ToCustomerCombo.Visible = true;
                        label7.Visible = true;
                        ToAccountCombo.Visible = true;
                        NewDataBtn.Visible = true;
                        TransactionKindCombo.SelectedIndex = 1;
                        BlanceTypeCombo.SelectedIndex = 2;
                        break;
                    }
                default:
                    {
                        MSG.Text = "هنوز هیچ نوع تراکنشی تایید نشده است";
                        label5.Visible = false;
                        ToCustomerCombo.Visible = false;
                        label7.Visible = false;
                        ToAccountCombo.Visible = false;
                        break;
                    }
            }
        }
        private void ToCustomerCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            var Id = ((KeyValue<long>)ToCustomerCombo.SelectedItem).Value;
            if (Id != 0)
            {
                ToAccountCombo = ComboBoxGenerator<long>.FillData(ToAccountCombo, Pattern.CartService.TitleValuesChild(Id), Convert.ToByte(ToAccountCombo.Tag));
            }
        }
        private void FromAccountCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            var Id = ((KeyValue<long>)FromAccountCombo.SelectedItem).Value;
            if (Id != 0)
            {
                FromAccountLBL.Text = Pattern.BlanceService.GetBlanceCartById(Id).ToString("N");
            }

        }
        private bool TransactionValidation(long type)
        {
            switch (type)
            {
                case 1:
                    {
                        if (FromAccountLBL.Text.Trim() != null && FromAccountLBL.Text.Trim() != "")
                        {
                            var TransactionCash = Convert.ToDouble(FromAccountLBL.Text);
                            var CurrentCash = Convert.ToDouble(CashTxt.Text);
                            if (CurrentCash > TransactionCash)
                            {
                                MSG.Text = "مبلغ تراکنش از مبلغ موجودی کارت بیشتراست";
                                return false;
                            }
                            //  تراکنش انجام شود
                            return true;
                        }
                        break;
                    }
                case 2:
                    {
                        if (FromAccountLBL.Text.Trim() != null && FromAccountLBL.Text.Trim() != "")
                        {
                            var TransactionCash = Convert.ToDouble(FromAccountLBL.Text);
                            var CurrentCash = Convert.ToDouble(CashTxt.Text);
                            if (CurrentCash > TransactionCash)
                            {
                                MSG.Text = "مبلغ تراکنش از مبلغ موجودی کارت بیشتراست";
                                return false;
                            }
                            //  تراکنش انجام شود
                            return true;
                        }
                        break;
                    }
                case 3:
                    {
                        if (FromAccountLBL.Text.Trim() != null && FromAccountLBL.Text.Trim() != "")
                        {
                            var TransactionCash = Convert.ToDouble(FromAccountLBL.Text);
                            var CurrentCash = Convert.ToDouble(CashTxt.Text);
                            if (CurrentCash > TransactionCash)
                            {
                                MSG.Text = "مبلغ تراکنش از مبلغ موجودی کارت بیشتراست";
                                return false;
                            }
                            //  تراکنش انجام شود
                            return true;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return true;
        }
        private void SaveNewDataBtn_Click(object sender, EventArgs e)
        {
            Pattern.UnitOfWork.BeginTransaction();
            try
            {
                var Custumer = CustomerDTO();
                var customeId = Pattern.CustomerService.AddOrUpdate(Custumer);
                var Bank = BankDTO();
                var bankId = Pattern.BankService.AddOrUpdate(Bank);
                var Cart = CartDTO(customeId,bankId);
                Pattern.CartService.AddOrUpdate(Cart);
                Pattern.UnitOfWork.Commit();
                UpdateComboBoxes();
            }
            catch
            {
                Pattern.UnitOfWork.Rollback();
                throw;
            }
        }
        private void NewDataBtn_Click(object sender, EventArgs e)
        {
            NewDataPanel.Visible = NewDataPanel.Visible ? false : true;
            if (NewDataPanel.Visible)
            {
                NewDataBtn.Text = "-";
            }
            else
            {
                NewDataBtn.Text = "+";
            }
        }
        private void UpdateComboBoxes()
        {
            TransactionTypeCombo = ComboBoxGenerator<long>.FillData(TransactionTypeCombo, Pattern.BlanceService.TitleValue(), Convert.ToByte(TransactionTypeCombo.Tag));
            TransactionKindCombo = ComboBoxGenerator<byte>.FillData(TransactionKindCombo, Pattern.BlanceService.TitleValueTransactionType(), Convert.ToByte(TransactionKindCombo.Tag));

            FromCustomerCombo = ComboBoxGenerator<long>.FillData(FromCustomerCombo, Pattern.CartService.TitleValuesParent(), Convert.ToByte(FromCustomerCombo.Tag));
            ToCustomerCombo = ComboBoxGenerator<long>.FillData(ToCustomerCombo, Pattern.CartService.TitleValuesAllParentCart(), Convert.ToByte(ToCustomerCombo.Tag));

            BlanceTypeCombo = ComboBoxGenerator<byte>.FillData(BlanceTypeCombo, Pattern.BlanceService.TitleValueBlanceType(), Convert.ToByte(BlanceTypeCombo.Tag));
        }
        private void ToCustomerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Id = ((KeyValue<long>)ToCustomerCombo.SelectedItem).Value;
            if (Id != 0)
            {
                ToCustomerCombo.Text = Pattern.BlanceService.GetBlanceCartById(Id).ToString("N");
                ToAccountCombo = ComboBoxGenerator<long>.FillData(ToAccountCombo, Pattern.CartService.TitleValuesChild(Id), Convert.ToByte(ToAccountCombo.Tag));
            }
        }
        private void FromCustomerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Id = ((KeyValue<long>)FromCustomerCombo.SelectedItem).Value;
            if (Id != 0)
            {
                FromCustomerLBL.Text = Pattern.BlanceService.GetBlanceCartById(Id).ToString("N");
                FromAccountCombo = ComboBoxGenerator<long>.FillData(FromAccountCombo, Pattern.CartService.TitleValuesChild(Id), Convert.ToByte(FromAccountCombo.Tag));
            }
        }
        private BankDTO BankDTO()
        {
            var entity = Pattern.BankService.GetBankByName(NewBankNameTxt.Text);
            if (entity != null)
            {
                return entity;
            }
            return new BankDTO
            {
                BankName = NewBankNameTxt.Text,

            };
        }
        private CartDTO CartDTO(long customeId, long bankId)
        {
            var entity = Pattern.CartService.GetCartByAccountNumber(NewCartNumberTxt.Text);
            if (entity != null)
            {
                return entity;
            }
            return new CartDTO
            {
                CartType = CartType.Main,
                AccountNumber = NewCartNumberTxt.Text,
                CustomerID = customeId,
                BankID = bankId,
                ExpireDate = DateTime.Now,
                Key = Guid.NewGuid(),
                ShabaAccountNumber = Guid.NewGuid().ToString(),
                ParentID = null
            };
        }
        private BlanceDTO BlanceDTO(double lastblance, double cash, long cartId, bool sum = true)
        {
            double blanceCash = lastblance;
            if (sum)
                blanceCash = lastblance + cash;
            else
                blanceCash = lastblance - cash;
            return new BlanceDTO
            {
                NewBlanceCash = blanceCash,
                OldBlanceCash = lastblance,
                BlanceType = BlanceType.Banking,
                CartID = cartId,
                TransactionType = sum ? TransactionType.Settlemant : TransactionType.Harvesting,
                TransactionCash = cash,
                TransactionID = TransactionID,
                Description = DescTxt.Text,

            };
        }
        private CustomerDTO CustomerDTO()
        {
            var entity = Pattern.CustomerService.GetCustomerByName(NewCustomerNameTxt.Text);
            if (entity != null)
            {
                return entity;
            }
            return new CustomerDTO
            {
                FullName = NewCustomerNameTxt.Text,
                Description = NewCustomerNameTxt.Text,
                Title = NewCustomerNameTxt.Text,
                Key = Guid.NewGuid()
            };
        }

    }
}
