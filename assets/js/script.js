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
  const checkboxes = document.querySelectorAll(".invoice-checkbox");
  const totalAmountSpan = document.getElementById("totalAmount");
  const selectedCountSpan = document.getElementById("selectedCount");
  let totalAmount = 0;
  let selectedCount = 0;

  checkboxes.forEach((checkbox) => {
      checkbox.addEventListener("change", function () {
          const row = checkbox.closest("tr");
          const amountDue = parseFloat(row.cells[5].textContent.replace("$", ""));

          if (checkbox.checked) {
              totalAmount += amountDue;
              selectedCount++;
          } else {
              totalAmount -= amountDue;
              selectedCount--;
          }

          totalAmountSpan.textContent = `$${totalAmount.toFixed(2)}`;
          selectedCountSpan.textContent = selectedCount;
      });
  });
});
