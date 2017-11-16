function ViewModel() {

    var self = this;
    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.user = ko.observable(); 
    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();
    self.errors = ko.observableArray([]);

    function showError(jqXHR) {
        console.log(jqXHR);
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
            if (response.error_description) self.errors.push(response.error_description);
        }
    }
    
    self.login = function () {
        self.result('');
        self.errors.removeAll();
        console.log("login clicked");
        $.ajax({
            type: 'GET',
            url: 'api/Account/UserIsAdim?roleName=Admin&userName=' + self.loginEmail()
        }).done(function (data) {

            console.log(data);
            var loginData = {
                grant_type: 'password',
                username: self.loginEmail(),
                password: self.loginPassword()
            };

            $.ajax({
                type: 'POST',
                url: '/oauth2/token',
                data: loginData
            }).done(function (data) {
                self.user(data.userName);
                console.log(data.access_token);
                // Cache the access token in session storage.
                sessionStorage.setItem(tokenKey, data.access_token);
                window.location.href = 'Admin/Dashboard';
                }).fail(showError);
            }).fail(function (error) {
                //self.result('Only admin can login');
                self.errors.push('Only Admin  can login!!');
                console.log("user login");
            });



       /* var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: '/oauth2/token',
            data: loginData
        }).done(function (data) {
            self.user(data.userName);
            console.log(data.access_token);
            // Cache the access token in session storage.
            sessionStorage.setItem(tokenKey, data.access_token);            
            window.location.href = 'Admin/Dashboard';
        }).fail(showError);*/
    }
}

var app = new ViewModel();
ko.applyBindings(app);

