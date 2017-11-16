$(document).ready(function () {

    var app = new ViewModel();
    ko.applyBindings(app);

    var tokenKey = 'accessToken';

    console.log("document loaded");

    self.surveysDataTable = $("#responsesTable").DataTable(
        {
            data: self.responses,
            columns: [{ data: "StudyGroupName" }, { data: "SurveyId" }, { data: "UserName" }, { data: "SurveyComments" }, { data: "SurveyResponseReceivedTime" }]
        });
    LoadSurveys();

    function LoadSurveys() {
        var headers = {};
        var token = sessionStorage.getItem(tokenKey);
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        //string id = Application["groupId"].ToString();

        var id = sessionStorage.getItem('groupId');
        console.log(id);
        console.log(token);
        $.ajax({
            type: 'GET',
            url: '/api/SurveyResponses?studyGroupId=' + id,
            headers: headers,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(data);
            self.responses = data;
            BindSurveysToDatatable(data);
        }).fail(showError);
    }

    function BindSurveysToDatatable(data) {
        console.log(self.surveys);
        self.surveysDataTable.clear();
        self.surveysDataTable.destroy();
        self.surveysDataTable = $("#responsesTable").DataTable(
            {
                data: self.responses,
                columns: [{ data: "StudyGroupName" }, { data: "SurveyId" }, { data: "UserName" }, { data: "SurveyComments" }, { data: "SurveyResponseReceivedTime" }]
            });
    }
    function ViewModel() {

        self.userName = ko.observable();
        self.userPassword = ko.observable();
        self.studyGroups = ko.observableArray([]);
        self.responses = {}
        self.userEmail = ko.observable();
        self.selectedStudyGroup = ko.observable();
        self.selectedStudyGroupForSurvey = ko.observable();

        self.result = ko.observable();
        self.errors = ko.observableArray([]);
    }

    function showError(jqXHR) {
        //console.log(jqXHR);
        self.result(jqXHR.status + ': ' + jqXHR.statusText);

        var response = jqXHR.responseJSON;
        if (response) {
            if (response.Message) self.errors.push(response.Message);
            if (response.ModelState) {
                var modelState = response.ModelState;
                for (var prop in modelState) {
                    if (modelState.hasOwnProperty(prop)) {
                        var msgArr = modelState[prop]; // expect array here
                        if (msgArr.length) {
                            for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
                        }
                    }
                }
            }
            if (response.error) self.errors.push(response.error);
            if (response.error_description) {
                self.errors.push(response.error_description);
                console.log(response.error_description);
            }
        }
    }



});