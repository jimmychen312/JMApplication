$(document).ready(function () {
    InitializeSeedDataViewModel();
});

function SeedDataViewModel() {

    this.MessageBox = ko.observable("");
    this.DisplayContent = ko.observable(true);

    this.SeedData = function () {
        SeedData();
    };
    
}

function InitializeSeedDataViewModel() {
    SeedDataViewModel = new SeedDataViewModel();
    ko.applyBindings(SeedDataViewModel);
}


function SeedData() {

    
    JMWebApplication.DisplayAjax();

    setTimeout(function () {

        var jqxhr = $.post("/api/seeddata/seed", "", function (response) {
            SeedDataCompleted(response);
        },
        "json")
         .fail(function (response) {
             RequestFailed(response);
         });
    }, 1000);

    alert("test2")
}


function SeedDataCompleted(response) {

    SeedDataViewModel.MessageBox("");
    SeedDataViewModel.MessageBox(JMWebApplication.RenderInformationalMessage(response.ReturnMessage));
    JMWebApplication.HideAjax();

}

function RequestFailed(response) {

    var jsonResponse = jsonParse(response.responseText);
    SeedDataViewModel.MessageBox("");
    SeedDataViewModel.MessageBox(JMWebApplication.RenderErrorMessage(jsonResponse.ReturnMessage));
    JMWebApplication.HideAjax();

}