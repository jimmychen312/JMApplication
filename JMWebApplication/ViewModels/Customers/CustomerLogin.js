$(document).ready(function () {
  
    InitializeCustomerLoginViewModel();
});

function CustomerLoginViewModel() {
  
    this.LoginName = ko.observable("BGates");
    this.MessageBox = ko.observable("");


    this.Login = function () {
       
        LoginCustomer();
    };

}

function InitializeCustomerLoginViewModel() {
    CustomerLoginViewModel = new CustomerLoginViewModel();
    ko.applyBindings(CustomerLoginViewModel); 
}


function LoginCustomer() {
         
    JMWebApplication.DisplayAjax();
     
    ////setTimeout() 方法用于在指定的毫秒数后调用函数或计算表达式。
    ////var t = setTimeout("alert('1 seconds!')", 1000);

    //alert(CustomerLoginViewModel.LoginName())

    setTimeout(function () {    

        var customer = new function () { };

        customer.LoginID =  CustomerLoginViewModel.LoginName();
      
        var jqxhr = $.post("/api/customers/LoginCustomer", customer, function (response) {
           LoginCustomerCompleted(response);
        },
        "json")
         .fail(function (response) {
             RequestFailed(response);
         });
    }, 1000);

    JMWebApplication.HideAjax();
}

function LoginCustomerCompleted(response) {
    
    window.location = "/home/index";

}

function RequestFailed(response) {

    if (response.status == "404")
    {
        CustomerLoginViewModel.MessageBox("");
        CustomerLoginViewModel.MessageBox(response.responseText);

        JMWebApplication.HideAjax();
        return;
    }

    JMWebApplication.ClearValidationErrors();
    var jsonResponse = jsonParse(response.responseText);

    CustomerLoginViewModel.MessageBox("");
    CustomerLoginViewModel.MessageBox(JMWebApplication.RenderErrorMessage(jsonResponse.ReturnMessage));

    JMWebApplication.HideAjax();

}