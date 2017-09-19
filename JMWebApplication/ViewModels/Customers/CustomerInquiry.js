$(document).ready(function () {

    InitializeCustomerInquiryViewModel();
    executingSearch = false;
    CustomerInquiry();

});

function InitializeCustomerInquiryViewModel() {
    customerInquiryViewModel = new CustomerInquiryViewModel();
    ko.applyBindings(customerInquiryViewModel, customerInquiryDivID);
}


function CustomerInquiryViewModel() {

    this.DisplayContent = ko.observable(false);
    this.FirstName = ko.observable("");
    this.LastName = ko.observable("");

    this.Customers = ko.observableArray("");
    this.MessageBox = ko.observable("");

    this.FirstNameDesc = ko.observable(false);
    this.FirstNameAsc = ko.observable(false);
    this.LastNameDesc = ko.observable(false);
    this.LastNameAsc = ko.observable(true);
 
    this.EmailAddressDesc = ko.observable(false);
    this.EmailAddressAsc = ko.observable(false);

    this.CityDesc = ko.observable(false);
    this.CityAsc = ko.observable(false);

    this.CountryDesc = ko.observable(false);
    this.CountryAsc = ko.observable(false);

    this.TotalCustomers = ko.observable();
    this.TotalPages = ko.observable();
    this.PageSize = ko.observable(pageSize);

    this.CurrentPageNumber = ko.observable(1);
    this.SortExpression = ko.observable("");
    this.CurrentSortExpression = ko.observable("");
    this.SortDirection = ko.observable("");

    this.EnableSearchButton = ko.observable(true);
    this.EnableResetButton = ko.observable(true);
    this.EnableFirstPageButton = ko.observable(false);
    this.DisableFirstPageButton = ko.observable(true);
    this.EnableLastPageButton = ko.observable(false);
    this.DisableLastPageButton = ko.observable(true);
    this.EnablePrevPageButton = ko.observable(false);
    this.DisablePrevPageButton = ko.observable(true);
    this.EnableNextPageButton = ko.observable(false);
    this.DisableNextPageButton = ko.observable(true);

    this.SetBackgroundColor = function (currentCustomer) {
        var rowIndex = this.Customers.indexOf(currentCustomer);
        var colorCode = rowIndex % 2 == 0 ? "White" : "WhiteSmoke";
        return colorCode;
    }

    this.EditCustomer = function (currentCustomer) {

        var selectedCustomer = this.Customers.indexOf(currentCustomer);
        var customerID = this.Customers()[selectedCustomer].CustomerID;
       
        ExecuteEditCustomer(customerID);

    }

    this.FirstPage = function () {
        ExecuteFirstPage();
    }
   
    this.NextPage = function () {    
        ExecuteNextPage();
    }

    this.PrevPage = function () {
        ExecutePrevPage();
    }

    this.LastPage = function () {
        ExecuteLastPage();
    }

    this.SortGrid = function (sortExpression) {
        ExecuteSortGrid(sortExpression);
    }

    this.SeachCustomers = function() {
        SearchCustomers();
    }

    this.ResetSearchValues = function () {
        ResetSearchValues();
    }
   
}

//function CustomerInquiry(){
//    setTimeout(function () {

//        var customerInquiry = new function () { };

//        customerInquiry.FirstName = customerInquiryViewModel.FirstName();

//        var jqxhr = $.get("/api/customers/GetCustomers", customerInquiry, function (response) {
//            alert(response);
//        },
//            "json")
//            .fail(function (response) {
//                RequestFailed(response);
//            });
//    }, 1000);
//}

function CustomerInquiry() {

    if (executingSearch == true) return false;

    JMWebApplication.DisplayAjax();
    
    var customerInquiry = new function () { };

    customerInquiry.FirstName = customerInquiryViewModel.FirstName();
    customerInquiry.LastName = customerInquiryViewModel.LastName();  
    customerInquiry.CurrentPageNumber = customerInquiryViewModel.CurrentPageNumber();
    customerInquiry.SortExpression = customerInquiryViewModel.SortExpression();
    customerInquiry.SortDirection = customerInquiryViewModel.SortDirection();
    customerInquiry.PageSize = customerInquiryViewModel.PageSize();

    setTimeout(function () {

        var jqxhr = $.get("/api/customers/GetCustomers", customerInquiry, function (response) {
            CustomerInquiryCompleted(response);
        },
            "json")
            .fail(function (response) {
                RequestFailed(response);
            });
    }, 1000);

    return false;

}

function CustomerInquiryCompleted(response)
{
 
    customerInquiryViewModel.TotalPages(response.TotalPages);
    customerInquiryViewModel.TotalCustomers(response.TotalRows);
    customerInquiryViewModel.Customers.removeAll();
    for (i = 0; i < response.Customers.length; i++) {
        var customer = CreateCustomer(response.Customers[i]);
        customerInquiryViewModel.Customers.push(customer);
    }

    EnablePager(customerInquiryViewModel.CurrentPageNumber(), customerInquiryViewModel.TotalPages());
    UpdateSortIndicator();
    customerInquiryViewModel.DisplayContent(true);

    JMWebApplication.HideAjax();

    window.scrollTo(0, 0);
 
}
function RequestFailed(response) {

    var jsonResponse = jsonParse(response.responseText);
    customerInquiryViewModel.MessageBox("");
    customerInquiryViewModel.MessageBox(JMWebApplication.RenderErrorMessage(jsonResponse.ReturnMessage));    
    customerInquiryViewModel.DisplayContent(true);

    JMWebApplication.HideAjax();

}


function UpdateSortIndicator() {

    customerInquiryViewModel.FirstNameDesc(false);
    customerInquiryViewModel.FirstNameAsc(false);
    customerInquiryViewModel.LastNameDesc(false);
    customerInquiryViewModel.LastNameAsc(false);
    customerInquiryViewModel.EmailAddressDesc(false);
    customerInquiryViewModel.EmailAddressAsc(false);
    customerInquiryViewModel.CityDesc(false);
    customerInquiryViewModel.CityAsc(false);
    customerInquiryViewModel.CountryDesc(false);
    customerInquiryViewModel.CountryAsc(false);
    

    if (customerInquiryViewModel.SortExpression() == "FirstName" && customerInquiryViewModel.SortDirection() == "ASC") {
        customerInquiryViewModel.FirstNameAsc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "FirstName" && customerInquiryViewModel.SortDirection() == "DESC") {
        customerInquiryViewModel.FirstNameDesc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "LastName" && customerInquiryViewModel.SortDirection() == "ASC") {
        customerInquiryViewModel.LastNameAsc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "LastName" && customerInquiryViewModel.SortDirection() == "DESC") {
        customerInquiryViewModel.LastNameDesc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "EmailAddress" && customerInquiryViewModel.SortDirection() == "ASC") {
        customerInquiryViewModel.EmailAddressAsc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "EmailAddress" && customerInquiryViewModel.SortDirection() == "DESC") {
        customerInquiryViewModel.EmailAddressDesc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "City" && customerInquiryViewModel.SortDirection() == "ASC") {
        customerInquiryViewModel.CityAsc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "City" && customerInquiryViewModel.SortDirection() == "DESC") {
        customerInquiryViewModel.CityDesc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "Country" && customerInquiryViewModel.SortDirection() == "ASC") {
        customerInquiryViewModel.CountryAsc(true); return;
    }

    if (customerInquiryViewModel.SortExpression() == "Country" && customerInquiryViewModel.SortDirection() == "DESC") {
        customerInquiryViewModel.CountryDesc(true); return;
    }

    customerInquiryViewModel.LastNameAsc(true);


}

function EnablePager(currentPage, totalPages) {

    customerInquiryViewModel.EnableNextPageButton(false);
    customerInquiryViewModel.DisableNextPageButton(false);
    customerInquiryViewModel.EnableLastPageButton(false);
    customerInquiryViewModel.DisableLastPageButton(false);

    if (currentPage < totalPages && totalPages > 0) {

        customerInquiryViewModel.EnableNextPageButton(true);
        customerInquiryViewModel.DisableNextPageButton(false);
        customerInquiryViewModel.EnableLastPageButton(true);
        customerInquiryViewModel.DisableLastPageButton(false);

    }

    if (currentPage > 1) {

        customerInquiryViewModel.EnablePrevPageButton(true);
        customerInquiryViewModel.DisablePrevPageButton(false);
        customerInquiryViewModel.EnableFirstPageButton(true);
        customerInquiryViewModel.DisableFirstPageButton(false);

    }

    if (totalPages == 1 || totalPages == currentPage)
    {
        customerInquiryViewModel.EnableNextPageButton(false);
        customerInquiryViewModel.DisableNextPageButton(true);
        customerInquiryViewModel.EnableLastPageButton(false);
        customerInquiryViewModel.DisableLastPageButton(true);
        customerInquiryViewModel.EnableLastPageButton(false);
        customerInquiryViewModel.DisableLastPageButton(true);

    }

    customerInquiryViewModel.EnableSearchButton(true);
    customerInquiryViewModel.EnableResetButton(true);

    executingSearch = false;

}

function CreateCustomer(customer) {

    var displayCustomer = new function () { };

    displayCustomer.CustomerID = customer.CustomerID;
    displayCustomer.FirstName = customer.FirstName;
    displayCustomer.LastName = customer.LastName;
    displayCustomer.EmailAddress = customer.EmailAddress;
    displayCustomer.City = customer.City;
    displayCustomer.Country = customer.Country;
    displayCustomer.PaymentTypeDescription = customer.PaymentTypeDescription;
  
    return displayCustomer;

}

function ExecuteSortGrid(sortExpression) {

    if (customerInquiryViewModel.SortExpression() == sortExpression) {
        if (customerInquiryViewModel.SortDirection() == "ASC")
            customerInquiryViewModel.SortDirection("DESC");
        else
            customerInquiryViewModel.SortDirection("ASC");
    }
    else {
        customerInquiryViewModel.SortDirection("ASC");
    }

    customerInquiryViewModel.CurrentPageNumber(1);
    customerInquiryViewModel.SortExpression(sortExpression);
    CustomerInquiry();

}

function ExecuteNextPage() {
    var currentPageNumber = customerInquiryViewModel.CurrentPageNumber();
    currentPageNumber++;
    customerInquiryViewModel.CurrentPageNumber(currentPageNumber);
    CustomerInquiry();
}

function ExecutePrevPage() {
    var currentPageNumber = customerInquiryViewModel.CurrentPageNumber();
    currentPageNumber = currentPageNumber - 1;
    customerInquiryViewModel.CurrentPageNumber(currentPageNumber);
    CustomerInquiry();
}

function ExecuteFirstPage() {
    currentPageNumber = 1;
    customerInquiryViewModel.CurrentPageNumber(1);
    CustomerInquiry();
}

function ExecuteLastPage() {
    var currentPageNumber = customerInquiryViewModel.TotalPages();
    customerInquiryViewModel.CurrentPageNumber(currentPageNumber);
    CustomerInquiry();
}

function SearchCustomers() {
    customerInquiryViewModel.CurrentPageNumber(1);
    customerInquiryViewModel.SortExpression("LastName");
    customerInquiryViewModel.SortDirection("ASC");
    CustomerInquiry();
    
}

function ResetSearchValues() {
    customerInquiryViewModel.FirstName("");
    customerInquiryViewModel.LastName("");
    customerInquiryViewModel.SortExpression("LastName");
    customerInquiryViewModel.SortDirection("ASC");
    CustomerInquiry();
}

function ExecuteEditCustomer(customerID) {

    JMWebApplication.DisplayAjax();

    $("#EditCustomer #CustomerID").val(customerID);
    $("#EditCustomer").submit();

    return false;

}

