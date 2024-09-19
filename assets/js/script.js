// Toggle the side navigation
window.addEventListener('DOMContentLoaded', event => {
  const sidebarToggle = document.body.querySelector('#sidebarToggle');
  if (sidebarToggle) {
      sidebarToggle.addEventListener('click', event => {
          event.preventDefault();
          document.body.classList.toggle('sb-sidenav-toggled');
          localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
      });
  }
});

document.addEventListener("DOMContentLoaded", function () {
  fetchAndPopulateAllData();
  setTimeout(function() {
    getTableInputData();
  }, 1000);
});
function fetchAndPopulateAllData() {
  fetch('https://pateagardev.github.io/eBiz-take-home-challenge/data.json')
    .then(response => response.json())
    .then(data => {
      // Show User Data
      document.getElementById('userName').innerText = data.userProfile.name;
      document.getElementById('logOut').innerText = data.userProfile.logged;
      const dropdownMenu = document.querySelector('.dropdown-menu');
      dropdownMenu.innerHTML = '';
      data.userProfile.settings.forEach(setting => {
          const listItem = document.createElement('li');
          listItem.innerHTML = `<a class="dropdown-item" href="${setting.link}">${setting.text}</a>`;
          dropdownMenu.appendChild(listItem);
      });

      document.getElementById('welcomeMessage').innerHTML = data.dashboardData.welcomeMessage;
      document.getElementById('summaryMessage').innerHTML = data.dashboardData.summaryMessage;
      // Show Dashboard Data
      const dashboardContainer = document.getElementById("dashboardContainer");
      dashboardContainer.innerHTML = ''; // Clear existing data
      data.dashboardData.cards.forEach(card => {
        const cardElement = `
          <div class="col-xl-3 card-box">
              <div class="card">
                  <div class="card-body">
                      <img alt=${card.imgAlt} src=${card.iconClass}>
                      <p class="card-body-title">${card.title}</p>
                      <p class="card-body-sub">${card.subtitle}</p>
                  </div>
              </div>
          </div>
        `;
        dashboardContainer.innerHTML += cardElement;
      });
      // Show Saved Cards
      setTimeout(function() {
        const savedCardsContainer = document.getElementById("cardDropdownMenu");
        savedCardsContainer.innerHTML = '';
        data.savedCards.forEach(card => {
          const cardElement = `
            <li><a class="dropdown-item" href="#">
              <img src="assets/images/icons/${card.cardType.toLowerCase()}_icons.png" alt="${card.cardType}" class="card-logo"> 
              ${card.cardType} ending in ${card.lastFourDigits}, "${card.nickname}"
            </a></li>
          `;
          savedCardsContainer.innerHTML += cardElement;
        });
      }, 1000);
      // Show Invoices Data
      document.getElementById('selectedCount').innerText = data.invoicedData.invoiceSelectedNum;
      document.getElementById('selectedAmount').innerText = data.invoicedData.invoiceSelected;
      document.getElementById('totalAmountTitle').innerHTML = data.invoicedData.amountOwed;
      document.getElementById('totalAmount').innerHTML = data.invoicedData.amountOwedNum;
      const invoiceTableBody = document.getElementById("invoiceTableBody");
      invoiceTableBody.innerHTML = '';
      data.invoicedData.invoices.forEach(invoice => {
        const rowElement = `
          <tr>
              <td><input type="checkbox" class="invoice-checkbox"></td>
              <td><a href="#">${invoice.invoiceNumber}</a></td>
              <td>${invoice.date}</td>
              <td>${invoice.dueDate}</td>
              <td>$${invoice.invoiceTotal.toFixed(2)}</td>
              <td>$${invoice.amountDue.toFixed(2)}</td>
              <td>${invoice.terms}</td>
              <td>${invoice.description}</td>
              <td>${invoice.poNumber}</td>
          </tr>
        `;
        invoiceTableBody.innerHTML += rowElement;
      });
      // Show PaymentSummary Data
      if (data.summaryPayments && data.summaryPayments.length > 0) {
        const summary = data.summaryPayments[0];
        document.getElementById("selectedCountIn").innerText = summary.selectedInvoices;
        document.getElementById("totalAmountIn").innerText = `$${summary.invoicePaymentAmount.toFixed(2)}`;
        document.getElementById("convenienceFee").innerText = `$${summary.convenienceFee.toFixed(2)}`;
        document.getElementById("totalAmountFinal").innerText = `$${summary.totalPaymentAmount.toFixed(2)}`;
        document.getElementById("selectedCountTitle").innerText = summary.selectedInvoicesTitle;
        document.getElementById("totalAmountInTitle").innerText = summary.invoicePaymentAmountTitle;
        document.getElementById("convenienceFeeTitle").innerText = summary.convenienceFeeTitle;
        document.getElementById("totalPaymentAmountTitle").innerText = summary.totalPaymentAmountTitle;
      }

      const tabsContainerModal = document.getElementById("paymentMethodsTabs");
      const tabContentsContainerModal = document.getElementById("paymentMethodsTabsContent");
      // Show Modal Tabs
      data.tabSectionModal.tabs.forEach(tab => {
          const tabElement = `
              <li class="nav-item" role="presentation">
                  <button class="nav-link ${tab.isActive ? 'active' : ''}" id="${tab.id}" data-bs-toggle="tab" data-bs-target="${tab.target}" type="button" role="tab" aria-controls="${tab.target.slice(1)}" aria-selected="${tab.isActive}">
                      ${tab.title}
                  </button>
              </li>
          `;
          tabsContainerModal.innerHTML += tabElement;
      });
      // Show Modal  Contents
      data.tabSectionModal.tabContents.forEach(tabContent => {
          const tabContentElement = `
              <div class="tab-pane fade ${tabContent.isActive ? 'show active' : ''}" id="${tabContent.id}" role="tabpanel">
                  ${tabContent.content}
              </div>
          `;
          tabContentsContainerModal.innerHTML += tabContentElement;
      });

      const tabsContainerPayments = document.getElementById("paymentTab");
      const tabContentsContainerPaymemt = document.getElementById("paymentTabContent");
      // Show Modal Tabs
      data.tabSectionPayment.tabs.forEach(tab => {
          const tabElement = `
              <li class="nav-item" role="presentation">
                  <button class="nav-link ${tab.isActive ? 'active' : ''}" id="${tab.id}" data-bs-toggle="tab" data-bs-target="${tab.target}" type="button" role="tab" aria-controls="${tab.target.slice(1)}" aria-selected="${tab.isActive}">
                      ${tab.title}
                  </button>
              </li>
          `;
          tabsContainerPayments.innerHTML += tabElement;
      });
      // Show Modal  Contents
      data.tabSectionPayment.tabContents.forEach(tabContent => {
          const tabContentElement = `
              <div class="tab-pane fade ${tabContent.isActive ? 'show active' : ''}" id="${tabContent.id}" role="tabpanel">
                  ${tabContent.content}
              </div>
          `;
          tabContentsContainerPaymemt.innerHTML += tabContentElement;
      });
      
    })
    .catch(error => {
      console.error('Error fetching eBiz data:', error);
    });
}

function getTableInputData() {
  // Select all checkboxes within the table
  const checkboxes = document.querySelectorAll("table tr input[type='checkbox']");
  // Elements where we'll display the counts and amounts
  const totalAmountSpan = document.getElementById("totalAmount");
  const selectedCountSpan = document.getElementById("selectedCount");
  const convenienceFeeSpan = document.getElementById("convenienceFee");
  const totalAmountFinalSpan = document.getElementById("totalAmountFinal");
  const totalAmountSpanIn = document.getElementById("totalAmountIn");
  const selectedCountSpanIn = document.getElementById("selectedCountIn");

  let totalAmount = 0;
  let selectedCount = 0;

  checkboxes.forEach((checkbox) => {
      checkbox.addEventListener("change", function () {
          const row = checkbox.closest("tr");
          const amountCell = row.querySelector("td:nth-child(6)");
          
          if (amountCell && amountCell.textContent) {
              const amountDue = parseFloat(amountCell.textContent.replace(/[$,]/g, ""));

              if (!isNaN(amountDue)) {
                  if (checkbox.checked) {
                      totalAmount += amountDue;
                      selectedCount++;
                  } else {
                      totalAmount -= amountDue;
                      selectedCount--;
                  }
                  // Calculate the 3% convenience fee
                  const convenienceFee = totalAmount * 0.03;
                  const finalTotal = totalAmount + convenienceFee;
                  // Update the display values
                  totalAmountSpan.textContent = `$${totalAmount.toFixed(2)}`;
                  selectedCountSpan.textContent = selectedCount;
                  convenienceFeeSpan.textContent = `$${convenienceFee.toFixed(2)}`;
                  totalAmountFinalSpan.textContent = `$${finalTotal.toFixed(2)}`;
                  totalAmountSpanIn.textContent = `$${totalAmount.toFixed(2)}`;
                  selectedCountSpanIn.textContent = selectedCount;
              }
          }
      });
  });
}