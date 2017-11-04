$(document).ready(function () {

    InitializeSysModuleInquiryViewModel();
    SysModuleInquiry();

});

function InitializeSysModuleInquiryViewModel() {
    sysModuleInquiryViewModel = new SysModuleInquiryViewModel();
    ko.applyBindings(sysModuleInquiryViewModel, sysModuleInquiryDivID);
}


function SysModuleInquiryViewModel() {

    this.DisplayContent = ko.observable(false);
    this.SysModules = ko.observableArray("");
    this.MessageBox = ko.observable("");
}
 

function SysModuleInquiry() {

    JMWebApplication.DisplayAjax();

    //var sysModuleInquiry = new function () { };
    
    setTimeout(function () {

        var jqxhr = $.get("/api/sysModules/GetSysModule", function (response) {
            SysModuleInquiryCompleted(response);
        },
            "json")
            .fail(function (response) {
                RequestFailed(response);
            });
    }, 100);

    return false;

}


function SysModuleInquiryCompleted(response) {

     
    sysModuleInquiryViewModel.SysModules.removeAll();

    for (i = 0; i < response.SysModules.length; i++) {
        var sysModule = CreateSysModule(response.SysModules[i]);
        sysModuleInquiryViewModel.SysModules.push(sysModule);
    }
        
    sysModuleInquiryViewModel.DisplayContent(true);

    JMWebApplication.HideAjax();


}

//function SysModuleInquiryCompleted(response)
//{
 
//    //sysModuleInquiryViewModel.TotalPages(response.TotalPages);
//    //sysModuleInquiryViewModel.TotalSysModules(response.TotalRows);
//    //sysModuleInquiryViewModel.SysModules.removeAll();
//    for (i = 0; i < response.SysModules.length; i++) {
//        var sysModule = CreateSysModule(response.SysModules[i]);
//        sysModuleInquiryViewModel.SysModules.push(sysModule);
//    }

//    //EnablePager(sysModuleInquiryViewModel.CurrentPageNumber(), sysModuleInquiryViewModel.TotalPages());
//    //UpdateSortIndicator();
//    //sysModuleInquiryViewModel.DisplayContent(true);

//    //JMWebApplication.HideAjax();

//    //window.scrollTo(0, 0);
 
//}

function RequestFailed(response) {

    var jsonResponse = jsonParse(response.responseText);
    sysModuleInquiryViewModel.MessageBox("");
    sysModuleInquiryViewModel.MessageBox(JMWebApplication.RenderErrorMessage(jsonResponse.ReturnMessage));    
    sysModuleInquiryViewModel.DisplayContent(true);

    JMWebApplication.HideAjax();

}

  

function CreateSysModule(sysModule) {

    var displaySysModule = new function () { };
    displaySysModule.Id = sysModule.Id;

    //displaySysModule.Id = sysModule.SysModuleID;
    displaySysModule.Name = sysModule.Name;
    displaySysModule.EnglishName = sysModule.EnglishName;
    displaySysModule.ParentId = sysModule.ParentId;
    displaySysModule.Url = sysModule.Url;
    
    return displaySysModule;

}
 
