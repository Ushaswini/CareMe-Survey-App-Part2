$(document).ready(function () {

    //to prevent back
   // window.onload  = window.history.forward();
    var app = new ViewModel();
    ko.applyBindings(app);

    var tokenKey = 'accessToken';

    console.log("document loaded");

    self.usersDataTable = $("#usersTable").DataTable(
        {
            select: true,
            data: self.users,
            columns: [{ data: "UserName" }]
    });

    LoadStudyGroups();

    LoadUsers();

    function LoadUsers() {
        var headers = {};
        var token = sessionStorage.getItem(tokenKey);
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        console.log(token);
        $.ajax({
            type: 'GET',
            url: '/api/Users',
            headers: headers,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(data);
            self.users = data;
            /*for (var i = 0; i < data.length; i++) {
                self.users.push(data[i]);
                console.log("users in table are" + data[i]);
            }*/
            
            BindUsersToDatatable(data);
        }).fail(showError);
    }

    function LoadStudyGroups() {
        var headers = {};
        var token = sessionStorage.getItem(tokenKey);
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        console.log(token);
        $.ajax({
            type: 'GET',
            url: '/api/StudyGroups',
            headers: headers,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                self.studyGroups.push(data[i]);
            }
        }).fail(showError);
    }
   
    function BindUsersToDatatable(data) {
        console.log(self.users);
        self.usersDataTable.clear();
        self.usersDataTable.destroy();
        self.usersDataTable = $("#usersTable").DataTable(
            {
                select:true,
                data: self.users,
                columns: [{ data: "UserName" }]
            });
        $('#usersTable tbody').on('click', 'tr', function () {
            var data = self.usersDataTable.row(this).data();
            //alert('You clicked on ' + data + '\'s row');
            console.log(data);
            sessionStorage.setItem('user', data);
            window.location.href = yourApp.Urls.userMessagesUrl;
        });
    }
    

    function ViewModel() {
        
        self.userName = ko.observable();
        self.userPassword = ko.observable();
        self.studyGroups = ko.observableArray([]);
        self.users = {}
        self.userEmail = ko.observable();
        self.selectedStudyGroup = ko.observable();
        self.selectedStudyGroupForSurvey = ko.observable();

        self.result = ko.observable();
        self.errors = ko.observableArray([]);

       

        self.AddUser = function () {

            self.result('');
            self.errors.removeAll();

            var data = {
                UserName: self.userName(),
                Password: self.userPassword(),
                Email: self.userEmail(),
                StudyGroupId: self.selectedStudyGroup()
            };
            var headers = {};
            var token = sessionStorage.getItem(tokenKey);
            if (token) {
                headers.Authorization = 'Bearer ' + token;
            }
            console.log("Data to add" + data);
            $.ajax({
                type: 'POST',
                url: '/api/Account/AddUser',
                headers: headers,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (data) {
                self.result("Done!");
                //Load users
                LoadUsers();
            }).fail(showError);
        }

      
    }

    $("#presentSurvey").click(function () {

        console.log("indied button click");
        var tokenKey = 'accessToken';
        var headers = {};
        var token = sessionStorage.getItem(tokenKey);
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        console.log(token);
        var surveyData = {
            StudyGroupId: self.selectedStudyGroupForSurvey()
        }
        $.ajax({
            type: 'POST',
            url: '/api/GenerateSurvey',
            headers: headers,
            data: surveyData,

        }).done(function (data) {
            console.log("data is received");

            }).fail(showError);
    })

    $("#logout").click(function () {
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'POST',
            url: '/api/Account/Logout',
            headers: headers
        }).done(function (data) {
            // Successfully logged out. Delete the token.
           // self.user('');
            window.location.href = yourApp.Urls.homeScreenUrl;
            sessionStorage.removeItem(tokenKey);
            
        }).fail(showError);
    })
    $('#navigateToSurveyManager').click(function () {
        // Response.Redirect("~/Views/Survey/Manage.cshtml");

        window.location.href = yourApp.Urls.surveyManageUrl;
            //replace("~/Views/Survey/Manage");

    })


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

    

})

