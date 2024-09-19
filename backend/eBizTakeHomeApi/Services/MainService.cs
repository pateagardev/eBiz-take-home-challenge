
using System;
using System.Collections.Generic;

namespace eBizTakeHomeApiChallenge.Services
{
    public class MainService
    {
        public eBizData GeteBizData()
        {
            return new eBizData
            {
                UserProfile = GetUserProfile(),
                DashboardData = GetDashboardData(),
                SavedCards = GetSavedCards(),
                SummaryPayments = GetSummaryPayments(),
                InvoicedData = GetInvoicedData(),
                TabSectionModal = GetTabSectionModalData(),
                TabSectionPayment = GetTabSectionPaymentData()
            };
        }

        // Data for user profile
        public UserProfile GetUserProfile()
        {
            return new UserProfile
            {
                Name = "Rebecca Smith",
                Logged = "Log Out",
                Settings = new List<Setting>
                {
                    new Setting { Text = "Settings", Link = "#!" },
                    new Setting { Text = "Activity Log", Link = "#!" }
                }
            };
        }

        // Data for dashboard cards
        public DashboardData GetDashboardData()
        {
          return new DashboardData
          {
              WelcomeMessage = "Welcome back, Rebecca.",
              SummaryMessage = "Here is a summary of your account.",
              Cards = new List<DashboardCard>
              {
                  new DashboardCard
                  {
                      Title = "$4,120.23",
                      Subtitle = "Total amount due",
                      IconClass = "assets/images/icons/dashboard/AmountDue.png",
                      ImgAlt = "Total" 
                  },
                  new DashboardCard
                  {
                      Title = "13",
                      Subtitle = "Open invoices",
                      IconClass = "assets/images/icons/dashboard/Invoices.png",
                      ImgAlt = "Invoices" 
                  },
                  new DashboardCard
                  {
                      Title = "2",
                      Subtitle = "Open sales orders",
                      IconClass = "assets/images/icons/dashboard/SalesOrders.png",
                      ImgAlt = "Sales" 
                  },
                  new DashboardCard
                  {
                      Title = "2",
                      Subtitle = "Saved payment methods",
                      IconClass = "assets/images/icons/dashboard/Credits.png",
                      ImgAlt = "Saved" 
                  },
                  new DashboardCard
                  {
                      Title = "On",
                      Subtitle = "Auto pay",
                      IconClass = "assets/images/icons/dashboard/AutoPay.png",
                      ImgAlt = "AutoPay" 
                  }
              }
          };
        }

        // Data for saved cards
        public List<SavedCard> GetSavedCards()
        {
            return new List<SavedCard>
            {
                new SavedCard { CardType = "MasterCard", LastFourDigits = "2039", Nickname = "Main Card" },
                new SavedCard { CardType = "Visa", LastFourDigits = "1234", Nickname = "Backup Card" }
            };
        }

        public List<SummaryPayment> GetSummaryPayments()
        {
            return new List<SummaryPayment>
            {
                new SummaryPayment
                {
                    SelectedInvoices = 0,
                    InvoicePaymentAmount = 0.00m,
                    ConvenienceFee = 0.00m,
                    TotalPaymentAmount = 0.00m,
                    SelectedInvoicesTitle = "# of invoices selected:",
                    InvoicePaymentAmountTitle = "Invoice payment amount:",
                    ConvenienceFeeTitle = "3% card convenience fee:",
                    TotalPaymentAmountTitle = "Total payment amount:"
                }
            };
        }
        public TabSectionPayment GetTabSectionPaymentData()
        {
            return new TabSectionPayment
            {
                Tabs = new List<TabData>
                {
                    new TabData
                    {
                        Id = "credit-card-tab",
                        Title = "Pay by Credit Card",
                        Target = "#credit-card",
                        IsActive = true
                    },
                    new TabData
                    {
                        Id = "bank-account-tab",
                        Title = "Pay with Bank Account",
                        Target = "#bank-account",
                        IsActive = false
                    }
                    // Add more tabs as needed
                },
                TabContents = new List<TabContent>
                {
                    new TabContent
                    {
                        Id = "credit-card",
                        IsActive = true,
                        Content = getPaymentCards() 
                    },
                    new TabContent
                    {
                        Id = "bank-account",
                        IsActive = false,
                        Content = getBankAccounts()
                    }
                    // Add more tab contents as needed
                }
            };
        }


        private string getPaymentCards() 
        {
          return @"
            <div class='row mb-3'>
              <ul class='nav nav-tabs mb-3' id='savedCardTabs' role='tablist'>
                <li class='nav-item' role='presentation'>
                    <button class='nav-link active' id='saved-card-tab' data-bs-toggle='tab' data-bs-target='#saved-card' type='button' role='tab' aria-controls='saved-card' aria-selected='true'>Saved Card</button>
                </li>
                <li class='nav-item' role='presentation'>
                    <button class='nav-link' id='new-card-tab' data-bs-toggle='tab' data-bs-target='#new-card' type='button' role='tab' aria-controls='new-card' aria-selected='false'>New Card</button>
                </li>
              </ul>
            </div>
            <div class='tab-content' id='saveCardTabsContent'>
              <!-- Saved Card Tab -->
              <div class='tab-pane fade show active tabs-card' id='saved-card' role='tabpanel' aria-labelledby='saved-card-tab'>
                <div class='row mb-3'>
                  <div class='col-7 mb-4'>
                      <label for='savedCard' class='form-label'>Saved Card *</label>
                      <div class='dropdown-cards'>
                        <button class='btn btn-light dropdown-toggle' type='button' id='cardDropdown' data-bs-toggle='dropdown' aria-expanded='false'>
                            <img alt='mastercard' id='cardLogo' src='assets/images/icons/mastercard_icons.png' class='card-logo'> 
                            <span id='cardDetails'>MasterCard ending in 2039, 'main card'</span>
                        </button>
                        <ul class='dropdown-menu' id='cardDropdownMenu' aria-labelledby='cardDropdown'></ul>
                    </div>
                  </div>
                  <div class='col-5 d-flex align-items-center'>
                      <a type='button'  data-bs-toggle='modal' data-bs-target='#paymentModal' class='text-primary ms-auto'>Manage Payment Methods</a>
                  </div>

                  <!-- Security Code Input -->
                  <div class='row mb-3'>
                    <div class='col-md-6'>
                        <label for='securityCode' class='form-label'>Security Code (CVV2 / CVC)</label>
                        <input type='text' class='form-control' id='securityCode' placeholder=''>
                    </div>
                  </div>
                </div>
              </div>
      
              <!-- New Card Tab -->
              <div class='tab-pane fade tabs-card' id='new-card' role='tabpanel' aria-labelledby='new-card-tab'>
                  <!-- Content for New Card -->
                  <div class='mb-3'>
                      <label for='cardNumber' class='form-label'>Card Number</label>
                      <input type='text' class='form-control' id='cardNumber' placeholder='Enter card number'>
                  </div>
                  <div class='mb-3'>
                      <label for='expiryDate' class='form-label'>Expiry Date</label>
                      <input type='text' class='form-control' id='expiryDate' placeholder='MM/YY'>
                  </div>
                  <div class='mb-3'>
                      <label for='newCardSecurityCode' class='form-label'>Security Code (CVV2 / CVC)</label>
                      <input type='text' class='form-control' id='newCardSecurityCode' placeholder=''>
                  </div>
              </div>
            </div>
          ";
        }

        private string getBankAccounts() 
        {
          return @"
          <h3>Bank Payments</h3>
          <p>Content for Bank Payments goes here.</p>
          ";
        }


        public TabSectionModal GetTabSectionModalData()
        {
            return new TabSectionModal
            {
                Tabs = new List<TabData>
                {
                    new TabData
                    {
                        Id = "credit-cards-tab",
                        Title = "Credit Cards",
                        Target = "#credit-cards",
                        IsActive = true
                    },
                    new TabData
                    {
                        Id = "ach-accounts-tab",
                        Title = "ACH Accounts",
                        Target = "#ach-accounts",
                        IsActive = false
                    }
                    // Add more tabs as needed
                },
                TabContents = new List<TabContent>
                {
                    new TabContent
                    {
                        Id = "credit-cards",
                        IsActive = true,
                        Content = GetCreditCardContent()
                    },
                    new TabContent
                    {
                        Id = "ach-accounts",
                        IsActive = false,
                        Content = GetAchAccountsContent()
                    }
                    // Add more tab contents as needed
                }
            };
        }

        private string GetCreditCardContent()
        {
            return @"
            <div class='row'>
                <div class='col-md-2'>
                  <ul class='nav flex-column nav-tabs mb-3' id='cardTabs' role='tablist'>
                      <button type='button' class='btn btn-add mt-2'><span class='circle-icon'>+</span>Add New Card</button>
                      <li class='nav-item' role='presentation'>
                        <button class='nav-link default-link active' id='card-2244-tab' data-bs-toggle='tab' data-bs-target='#card-2244' type='button' role='tab' aria-controls='card-2244' aria-selected='true'>
                            <span class='default-text'>default</span>
                            <div class='default-flex'>
                              <img src='assets/images/icons/visa_icons.png' alt='Visa' class='me-2'>
                              <span> ending in 2244 <span class='fst-italic'>'Main debit'</span></span>
                            </div>
                        </button>
                      </li>
                      <li class='nav-item' role='presentation'>
                          <button class='nav-link' id='card-2997-tab' data-bs-toggle='tab' data-bs-target='#card-2997' type='button' role='tab' aria-controls='card-2997' aria-selected='false'>
                              <img src='assets/images/icons/amex_icons.png' alt='Amex' class='me-2'>
                              <span> ending in 2997 <span class='fst-italic'>'Main debit'</span></span>
                          </button>
                      </li>
                      <li class='nav-item' role='presentation'>
                          <button class='nav-link' id='card-1221-tab' data-bs-toggle='tab' data-bs-target='#card-1221' type='button' role='tab' aria-controls='card-1221' aria-selected='false'>
                              <img src='assets/images/icons/amex_icons.png' alt='Amex' class='me-2'>
                              <span> ending in 1221 <span class='fst-italic'>'Main debit'</span></span>
                          </button>
                      </li>
                      <li class='nav-item' role='presentation'>
                          <button class='nav-link' id='card-9672-tab' data-bs-toggle='tab' data-bs-target='#card-9672' type='button' role='tab' aria-controls='card-9672' aria-selected='false'>
                              <img src='assets/images/icons/mastercard_icons.png' alt='Mastercard' class='me-2'>
                              <span> ending in 9672 <span class='fst-italic'>'Main debit'</span></span>
                          </button>
                      </li>
                      <li class='nav-item' role='presentation'>
                          <button class='nav-link' id='card-5555-tab' data-bs-toggle='tab' data-bs-target='#card-5555' type='button' role='tab' aria-controls='card-5555' aria-selected='false'>
                              <img src='assets/images/icons/visa_icons.png' alt='Visa' class='me-2'> 
                              <span> ending in 5555 <span class='fst-italic'>'Main debit'</span></span>
                          </button>
                      </li>
                  </ul>
              </div>

              <!-- Card Details Content -->
              <div class='col-md-10'>
                <div class='tab-content' id='cardTabsContent'>
                  <!-- Card 2244 Details -->
                  <div class='tab-pane fade show active' id='card-2244' role='tabpanel' aria-labelledby='card-2244-tab'>
                    <form>
                        <div class='row mb-3'>
                            <div class='col'>
                                <label for='cardNumber-2244' class='form-label'>Card Number *</label>
                                <input type='text' class='form-control' id='cardNumber-2244' value='XXXXXXXXXXXX2244'>
                            </div>
                            <div class='col'>
                                <label for='billingAddress-2244' class='form-label'>Billing Address *</label>
                                <input type='text' class='form-control' id='billingAddress-2244' value='123 Sunshine Lane'>
                            </div>
                        </div>
                        <div class='row mb-3'>
                            <div class='col'>
                                <label for='nameOnCard-2244' class='form-label'>Name on Card *</label>
                                <input type='text' class='form-control' id='nameOnCard-2244' value='Ashley Johnson'>
                            </div>
                            <div class='col'>
                                <label for='zipCode-2244' class='form-label'>Zip Code / Postal Code *</label>
                                <input type='text' class='form-control' id='zipCode-2244' value='098345'>
                            </div>
                            <div class='col'>
                                <label for='cardNickname-2244' class='form-label'>Card Nickname</label>
                                <input type='text' class='form-control' id='cardNickname-2244' value='Primary card'>
                            </div>
                        </div>
                        <div class='row mb-3'>
                            <div class='col'>
                                <label for='expirationDate-2244' class='form-label'>Expiration Date *</label>
                                <div class='d-flex'>
                                    <select class='form-select me-2' id='expirationMonth-2244'>
                                      <option>01</option>
                                      <option>02</option>
                                      <option>93</option>
                                      <option>04</option>
                                      <option>05</option>
                                      <option>06</option>
                                      <option>07</option>
                                    </select>
                                    <select class='form-select' id='expirationYear-2244'>
                                      <option>23</option>
                                      <option>24</option>
                                      <option>25</option>
                                      <option>26</option>
                                      <option>27</option>
                                      <option>28</option>
                                      <option>29</option>
                                    </select>
                                </div>
                            </div>
                            <div class='col d-flex align-items-center'>
                                <input type='checkbox' class='form-check-input me-2' id='defaultPayment-2244' checked>
                                <label class='form-check-label' for='defaultPayment-2244'>Make default payment method</label>
                            </div>
                        </div>
                        <div class='col data-button'>
                          <button type='button' class='btn btn-outline-danger'>Delete Card</button>
                          <button type='submit' class='btn btn-primary btn-save float-end'>Save Changes</button>
                        </div>
                    </form>
                  </div>
                  <div class='tab-pane fade show' id='card-2997' role='tabpanel' aria-labelledby='card-2997-tab'>
                    <h3>Card 2997 </h3>
                    <p>Content for Card 2997 goes here.</p>
                  </div>
                  <div class='tab-pane fade show' id='card-1221' role='tabpanel' aria-labelledby='card-1221-tab'>
                    <h3>Card 1221 </h3>
                    <p>Content for Card 1221 goes here.</p>
                  </div>
                  <div class='tab-pane fade show' id='card-9672' role='tabpanel' aria-labelledby='card-9672-tab'>
                    <h3>Card 9672 </h3>
                    <p>Content for Card 9672 goes here.</p>
                  </div>
                  <div class='tab-pane fade show' id='card-5555' role='tabpanel' aria-labelledby='card-5555-tab'>
                    <h3>Card 5555 </h3>
                    <p>Content for Card 5555 goes here.</p>
                  </div>
                </div>
              </div>
            </div>
            ";
        }

        private string GetAchAccountsContent()
        {
            return @"
            <h3>ACH Accounts</h3>
            <p>ACH accounts content goes here.</p>
            ";
        }
        // Data for invoices
        public InvoicedData GetInvoicedData()
        {
            return new InvoicedData
            {
                InvoiceSelected = "Invoices selected:",
                InvoiceSelectedNum = "0",
                AmountOwed = "Total payment amount:.",
                AmountOwedNum = "$0.00",
                Invoices = new List<Invoice> 
                {
                  new Invoice
                  {
                      InvoiceNumber = 234239,
                      Date = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      DueDate = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      InvoiceTotal = 100.00m,
                      AmountDue = 100.00m,
                      Terms = "Discount of $100.00 if paid by 5/2/2020",
                      Description = "Lorem ipsum dlse esliop ehasersv",
                      PONumber = 239482
                  },
                  new Invoice
                  {
                      InvoiceNumber = 567567,
                      Date = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      DueDate = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      InvoiceTotal = 500.00m,
                      AmountDue = 500.00m,
                      Terms = "Discount of $100.00 if paid by 5/2/2020",
                      Description = "Lorem ipsum dlse esliop ehasersv",
                      PONumber = 239482
                  },
                  new Invoice
                  {
                      InvoiceNumber = 345454,
                      Date = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      DueDate = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      InvoiceTotal = 200.00m,
                      AmountDue = 200.00m,
                      Terms = "Discount of $100.00 if paid by 5/2/2020",
                      Description = "Lorem ipsum dlse esliop ehasersv",
                      PONumber = 239482
                  },
                  new Invoice
                  {
                      InvoiceNumber = 345454, // This one has a duplicate Invoice Number
                      Date = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      DueDate = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      InvoiceTotal = 300.00m,
                      AmountDue = 300.00m,
                      Terms = "Discount of $100.00 if paid by 5/2/2020",
                      Description = "Lorem ipsum dlse esliop ehasersv",
                      PONumber = 239482
                  },
                  new Invoice
                  {
                      InvoiceNumber = 345345,
                      Date = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      DueDate = new DateTime(2018, 10, 30).ToString("MM/dd/yy"),
                      InvoiceTotal = 20.00m,
                      AmountDue = 20.00m,
                      Terms = "Discount of $100.00 if paid by 5/2/2020",
                      Description = "Lorem ipsum dlse esliop ehasersv Test",
                      PONumber = 239482
                  }
                }
            };
        }

    }

    // Unified class for all eBizData
    public class eBizData
    {
        public UserProfile UserProfile { get; set; }
        public DashboardData DashboardData { get; set; }
        public List<SavedCard> SavedCards { get; set; }
        public List<SummaryPayment> SummaryPayments { get; set; }
        public InvoicedData InvoicedData { get; set; }
        public TabSectionModal TabSectionModal { get; set; }
        public TabSectionPayment TabSectionPayment { get; set; }
    }

    public class DashboardData
    {
        public string WelcomeMessage { get; set; }
        public string SummaryMessage { get; set; }
        public List<DashboardCard> Cards { get; set; }
    }

    public class DashboardCard
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string IconClass { get; set; }
        public string ImgAlt { get; set; }
    }

    public class UserProfile
    {
        public string Name { get; set; }
        public string Logged { get; set; }
        public List<Setting> Settings { get; set; }
    }

    public class Setting
    {
        public string Text { get; set; }
        public string Link { get; set; }
    }

    public class SavedCard
    {
        public string CardType { get; set; }
        public string LastFourDigits { get; set; }
        public string Nickname { get; set; }
    }

    public class SummaryPayment
    {
        public int SelectedInvoices { get; set; }
        public decimal InvoicePaymentAmount { get; set; }
        public decimal ConvenienceFee { get; set; }
        public decimal TotalPaymentAmount { get; set; }
        public string SelectedInvoicesTitle { get; set; }
        public string InvoicePaymentAmountTitle { get; set; }
        public string ConvenienceFeeTitle { get; set; }
        public string TotalPaymentAmountTitle { get; set; }
    }

    public class InvoicedData
    {
        public string InvoiceSelected { get; set; }
        public string InvoiceSelectedNum { get; set; }
        public string AmountOwed { get; set; }
        public string AmountOwedNum { get; set; }
        public List<Invoice> Invoices { get; set; }
    }

    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal AmountDue { get; set; }
        public string Terms { get; set; }
        public string Description { get; set; }
        public int PONumber { get; set; }
    }
    public class TabData
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Target { get; set; } 
        public bool IsActive { get; set; }
    }

    public class TabContent
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public string Content { get; set; }
    }

    public class TabSectionModal
    {
        public List<TabData> Tabs { get; set; } = new List<TabData>();
        public List<TabContent> TabContents { get; set; } = new List<TabContent>();
    }
    public class TabSectionPayment
    {
        public List<TabData> Tabs { get; set; } = new List<TabData>();
        public List<TabContent> TabContents { get; set; } = new List<TabContent>();
    }
}
