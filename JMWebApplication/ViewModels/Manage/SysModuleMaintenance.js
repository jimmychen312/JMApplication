$(document).ready(function () {

    InitializeSysModuleMaintenanceViewModel();
 
    GetSysModuleInformation();
});

function SysModuleMaintenanceViewModel() {

    this.DisplayContent = ko.observable(false);
    this.SysModules = ko.observableArray("");
    this.MessageBox = ko.observable("");

}

function InitializeSysModuleMaintenanceViewModel() {

    sysModuleMaintenanceViewModel = new SysModuleMaintenanceViewModel();
    ko.applyBindings(sysModuleMaintenanceViewModel);
    SubscribeToChanges();
   
    sysModuleMaintenanceViewModel.SysModuleID(sysModuleID);

}
 


$(function () {

    var o = {
        showcheck: false,
        url: GetSysModuleInformation(),
        onnodeclick: function (item) {
            var tabTitle = item.text;
            var url = "../../" + item.value;
            var icon = item.Icon;
            if (!item.hasChildren) {
                addTab(tabTitle, url, icon);
            } else {

                $(this).parent().find("img").trigger("click");
            }
        }
    }
    $.post(GetSysModuleInformation(), { "id": "0" },
        function (data) {
            if (data == "0") {
                window.location = "/Account";
            }
            o.data = data;
            $("#RightTree").treeview(o);
        }, "json");
});



function GetSysModuleInformation()
{

    JMWebApplication.DisplayAjax();

    var sysModule = new function () { };
    sysModule.SysModuleID = sysModuleMaintenanceViewModel.SysModuleID();

    setTimeout(function() {

        var jqxhr = $.get("/api/sysModules/GetSysModuleById", sysModule, function (response) {
            GetSysModuleCompleted(response);
        },
        "json")
         .fail(function (response) {
             RequestFailed(response);
         });
    }, 1000);

}
 

function GetSysModuleCompleted(response) {

    sysModuleMaintenanceViewModel.PaymentTypes(response.PaymentTypes);
  
    if (JMWebApplication.IsGuidEmpty(sysModuleMaintenanceViewModel.SysModuleID())==true)
    {
        sysModuleMaintenanceViewModel.MessageBox("");
        sysModuleMaintenanceViewModel.EnableCreateButton(true);
        sysModuleMaintenanceViewModel.EnableUpdateButton(false);
        sysModuleMaintenanceViewModel.EnableEditButton(false);
        sysModuleMaintenanceViewModel.EnableCreateAnotherSysModuleButton(false);
        sysModuleMaintenanceViewModel.EnableCancelChangesButton(false);

        sysModuleMaintenanceViewModel.DisplayMode(false);
        sysModuleMaintenanceViewModel.EditMode(true);

        JMWebApplication.HideAjax();

        return;
    }

    sysModuleMaintenanceViewModel.FirstName(response.SysModule.FirstName);
    sysModuleMaintenanceViewModel.LastName(response.SysModule.LastName);
    sysModuleMaintenanceViewModel.Address(response.SysModule.Address);
    sysModuleMaintenanceViewModel.City(response.SysModule.City);
    sysModuleMaintenanceViewModel.Region(response.SysModule.Region);
    sysModuleMaintenanceViewModel.PostalCode(response.SysModule.PostalCode);
    sysModuleMaintenanceViewModel.Country(response.SysModule.Country);
    sysModuleMaintenanceViewModel.PhoneNumber(response.SysModule.PhoneNumber);
    sysModuleMaintenanceViewModel.TelePhone(response.SysModule.TelePhone);
    sysModuleMaintenanceViewModel.EmailAddress(response.SysModule.EmailAddress);
    sysModuleMaintenanceViewModel.CreditCardSecurityCode(response.SysModule.CreditCardSecurityCode);
    sysModuleMaintenanceViewModel.CreditCardNumber(response.SysModule.CreditCardNumber);
    sysModuleMaintenanceViewModel.PaymentTypeID(response.SysModule.PaymentTypeID);
    sysModuleMaintenanceViewModel.CreditCardExpirationDate(JMWebApplication.FormatJsonDate(response.SysModule.CreditCardExpirationDate));
    sysModuleMaintenanceViewModel.CreditLimit(JMWebApplication.FormatCurrency(response.SysModule.CreditLimit));
    
    sysModuleMaintenanceViewModel.MessageBox("");
    sysModuleMaintenanceViewModel.EnableCreateButton(false);
    sysModuleMaintenanceViewModel.EnableUpdateButton(false);
    sysModuleMaintenanceViewModel.EnableEditButton(true);
    sysModuleMaintenanceViewModel.EnableCreateAnotherSysModuleButton(true);
    sysModuleMaintenanceViewModel.EnableCancelChangesButton(false);

    sysModuleMaintenanceViewModel.DisplayMode(true);
    sysModuleMaintenanceViewModel.EditMode(false);

    if (!JMWebApplication.IsGuidEmpty(sysModuleMaintenanceViewModel.PaymentTypeID()))
    {
        SetPaymentTypeDescription(sysModuleMaintenanceViewModel.PaymentTypeID())
    }

    UpdateOriginalValues();

    JMWebApplication.HideAjax();

}